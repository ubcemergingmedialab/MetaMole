using System.Collections.Generic;
using System.Threading;
using Meta.MetaAnalytics;
using Meta.Mouse;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meta
{
    /// <summary>
    /// Reports analytics events for the SDK.
    /// </summary>
    internal class MetaSdkAnalytics : IEventReceiver
    {
        private int _numberOfSuccessfulSlamInitializations = 0;
        private int _numberOfFailedSlamInitializations = 0;

        private float _slamInitBeginTime = 0;

        /// <summary>
        /// Whether SLAM had begun initializing. This is used to conditionally record 
        /// events when SLAM ends initialization. This is required because SLAM may end
        /// initialization without beginning initialization by loading a map.
        /// </summary>
        private bool _slamBeganInitialization;

        private List<float> _slamInitTimes = new List<float>();

#if !NET_2_0_SUBSET

        private bool? _webcamEnabled = null;
        private IMetaAnalytics _metaAnalytics;

        public MetaSdkAnalytics()
        {
            _metaAnalytics = new MetaAnalytics.MetaAnalytics();
        }
#endif

        public void Init(IEventHandlers eventHandlers)
        {
#if !NET_2_0_SUBSET
            eventHandlers.SubscribeOnAwake(SceneStartAnalytics);
            eventHandlers.SubscribeOnApplicationQuit(SceneStopAnalytics);
            eventHandlers.SubscribeOnUpdate(OnUpdate);
            eventHandlers.SubscribeOnStart(InitSlamLocalizerAnalytics);

#endif
        }


        private void InitSlamLocalizerAnalytics()
        {
            SlamLocalizer slamLocalizer = GameObject.FindObjectOfType<SlamLocalizer>();

            if (slamLocalizer == null)
            {
                Debug.LogError(GetType() + ": Could not retrieve localizer.");
                return;
            }

            slamLocalizer.onSlamSensorsReady.AddListener(BeginLocalizationEvent);
            slamLocalizer.onSlamLocalizerResetEvent.AddListener(BeginLocalizationEvent);

            slamLocalizer.onSlamMappingComplete.AddListener(() => { EndLocalizationEvent(true); });
            slamLocalizer.onSlamInitializationFailed.AddListener(() => { EndLocalizationEvent(false); });

        }

        private void BeginLocalizationEvent()
        {
            _slamBeganInitialization = true;
            _slamInitBeginTime = Time.fixedUnscaledTime;
        }

        private void EndLocalizationEvent(bool success)
        {
            if (!success)
            {
                _numberOfFailedSlamInitializations++;
            }
            else if (success && _slamBeganInitialization)
            {
                _numberOfSuccessfulSlamInitializations++;
            }

            if (_slamBeganInitialization)
            {
                _slamInitTimes.Add(Time.fixedUnscaledTime - _slamInitBeginTime);
                _slamBeganInitialization = false;
            }
            
        } 


#if !NET_2_0_SUBSET
        private void OnUpdate()
        {
            //Prevents generating analytics when the scene is started
            if (_webcamEnabled == null)
            {
                _webcamEnabled = Meta.Plugin.Webcam.IsWebcamOn();
                return;
            }

            WebcamToggleAnalytics();
        }

        private void WebcamToggleAnalytics()
        {
            bool webcamEnabled = Meta.Plugin.Webcam.IsWebcamOn();
            if (webcamEnabled != _webcamEnabled)
            {
                _webcamEnabled = webcamEnabled;
                JObject o = new JObject();
                o["webcam_enabled"] = webcamEnabled;

                SendAsyncAnalytics("unity_webcamToggle", o);
            }
        }

        private void SceneStartAnalytics()
        {
            Scene s = SceneManager.GetActiveScene();
            string sceneName = s.name;

            bool handsInScene = GameObject.FindObjectOfType(typeof(HandsProvider)) != null;
            bool mouseInScene = GameObject.FindObjectOfType(typeof(MetaInputModule)) != null;

            JObject o = new JObject();
            o["scene_identifier"] = sceneName;
            o["hands_present"] = handsInScene;
            o["mouse_present"] = mouseInScene;
            SendAsyncAnalytics("unity_sceneStarted", o);
        }

        private void SendAsyncAnalytics(string eventName, JObject o)
        {
            Thread t = new Thread(() => { _metaAnalytics.SendAnalytics(eventName, o.ToString()); });
            t.Start();
        }

        private void SceneStopAnalytics()
        {
            Scene s = SceneManager.GetActiveScene();
            string sceneName = s.name;
            JObject o = new JObject();
            o["scene_identifier"] = sceneName;
            AddSlamAnalytics(o);
            _metaAnalytics.SendAnalytics("unity_sceneEnded", o.ToString());
        }

        private void AddSlamAnalytics(JObject o)
        {
            o["slam_successful"] = _numberOfSuccessfulSlamInitializations;
            o["slam_fail"] = _numberOfFailedSlamInitializations;

            float min, avg, max;
            min = avg = max = float.PositiveInfinity;

            if (_slamInitTimes.Count > 0)
            {
                max = float.NegativeInfinity;
                float sum = 0f;
                foreach (float initTime in _slamInitTimes)
                {
                    if (initTime > max)
                    {
                        max = initTime;
                    }

                    if (initTime < min)
                    {
                        min = initTime;
                    }

                    sum += initTime;
                }
                avg = sum / (float) _slamInitTimes.Count;

            }

            o["slam_min_time"] = min;
            o["slam_avg_time"] = avg;
            o["slam_max_time"] = max;
        }
#endif
    }
}
