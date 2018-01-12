using System;
using System.Collections.Generic;
using System.Linq;
using Meta.HandInput;
using UnityEngine;
using UnityEngine.Assertions;
#pragma warning disable 0414

namespace Meta
{
    /// <summary>
    /// This class holds all information regarding the hands (including variables, thresholds, statistics)
    /// as well as being the main application's entry point for hand references.
    /// </summary>
    public class HandsProvider : MetaBehaviour
    {
        #region Member variables

        private const string RightHandPath = "Prefabs/HandTemplate (Right)";
        private const string LeftHandPath = "Prefabs/HandTemplate (Left)";

        [Readonly]
        [SerializeField]
        private GameObject _rightTemplate;

        [Readonly]
        [SerializeField]
        private GameObject _leftTemplate;

        [SerializeField]
        private Settings _settings = new Settings();
        [SerializeField]
        private Events _events = new Events();
        [SerializeField]
        private Stats _statistics = new Stats();

        private readonly List<Hand> _activeHands = new List<Hand>();

        /// <summary>
        /// Class containing all settings for the hand.
        /// </summary>
        public Settings settings
        {
            get { return _settings; }
        }

        /// <summary>
        /// Class containing events related to the hand.
        /// </summary>
        [SerializeField]
        public Events events
        {
            get { return _events; }
        }

        /// <summary>
        /// Class containing all settings for the hand.
        /// </summary>
        public Stats Statistics
        {
            get
            {
                return _statistics;
            }
        }

        #endregion Member variables

        #region Monobehaviour methods

        private void Awake()
        {
            InitializeTemplateHands();
        }

        private void Start()
        {
            Initialize();
        }

        private void Reset()
        {
            InitializeTemplateHands();
        }

        [RuntimeInitializeOnLoadMethod]
        private void OnValidate()
        {
            InitializeTemplateHands();
        }

        #endregion Monobehaviour methods

        #region Member methods

        private void InitializeTemplateHands()
        {
            if (_rightTemplate == null)
            {
                var rightTemplate = Resources.Load<GameObject>(RightHandPath);
                if (rightTemplate == null)
                {
                    UnityEngine.Debug.LogWarning(RightHandPath);
                }
                else
                {
                    _rightTemplate = rightTemplate;
                }
            }


            if (_leftTemplate == null)
            {
                var leftTemplate = Resources.Load<GameObject>(LeftHandPath);
                if (leftTemplate == null)
                {
                    UnityEngine.Debug.LogWarning("No HandTemplate (Left).");
                }
                else
                {
                    _leftTemplate = leftTemplate;
                }

            }
        }

        private void Initialize()
        {
            HandsModule handManager = GameObject.FindObjectOfType<MetaContextBridge>().CurrentContext.Get<HandsModule>();

            handManager.OnHandEnterFrame += OnHandDataAppear;
            handManager.OnHandExitFrame += OnHandDataDisappear;
        }

        private void OnHandDataAppear(HandData handData)
        {
            var handProxy = HandUtil.CreateNewHand(handData);
            handProxy.transform.SetParent(transform);
            _activeHands.Add(handProxy);

            // -- Invoke on hand appear event
            events.OnHandEnter.Invoke(handProxy);
        }

        private void OnHandDataDisappear(HandData handData)
        {
            var handProxyForHandData = _activeHands.First(handProxy => handProxy.HandId == handData.HandId);


            int nullPoxies = 0;
            foreach (var activeHand in _activeHands)
            {
                if (activeHand == null)
                {
                    nullPoxies++;
                }
            }
            if (nullPoxies > 0)
            {
                UnityEngine.Debug.Log("Null proxy count: " + nullPoxies);
            }


            if (handProxyForHandData == null)
            {
                Assert.IsTrue(handData != null);
                throw new ArgumentNullException("Outgoing HandData does not exist in active Hand list");
            }

            // -- Invoke on hand disappear event
            events.OnHandExit.Invoke(handProxyForHandData);

            // -- Notify Hand & MotionHandFeatures
            handProxyForHandData.MarkInvalid();

            // -- Remove Hand from active Hand list
            _activeHands.Remove(handProxyForHandData);

            // -- Destroy Hand
            Destroy(handProxyForHandData.gameObject);
        }

        /// <summary>
        /// Not currently used. Should be used in case HandTemplate (Right) or HandTemplate (Left) Have been corupted / destroyed.
        /// </summary>
        private void BuildNewTemplates()
        {
            if (_rightTemplate == null)
            {
                HandUtil.InitializeTemplateHand(HandType.Right);
            }

            if (_leftTemplate == null)
            {
                HandUtil.InitializeTemplateHand(HandType.Left);
            }
        }

        #endregion Member methods

        #region Helper classes

        /// <summary>
        /// Class containing events related to the hand.
        /// </summary>
        [Serializable]
        public class Events
        {
            /// <summary> Event fired on the first frame the hand is visible. </summary>
            public OnNewHandData OnHandEnter;
            /// <summary> Event fired on the last frame the hand is visible, before the hand GameObject is destoryed. </summary>
            public OnNewHandData OnHandExit;


            /// <summary> Event fired on the first frame a hand goes from open to closed. </summary>
            public OnNewHandData OnGrab;
            /// <summary> Event fired on the first frame a hand goes from closed to open. </summary>
            public OnNewHandData OnRelease;
        }

        /// <summary>
        /// Class containing all settings for the hand.
        /// </summary>
        [Serializable]
        public class Settings
        {
            private const int IgnoreRaycast = 2;
            private const int Everything = -1;

            [Header("CenterHandFeature Search Settings")]
            [Range(0, 0.15f)]
            [SerializeField]
            private float _palmRadiusNear = 0.04f;

            [Range(0, 0.15f)]
            [SerializeField]
            private float _palmRadiusFar = 0.065f;

            [Range(0, 0.025f)]
            [SerializeField]
            private float _closestObjectDebounce = 0.01f;

            [SerializeField]
            private QueryTriggerInteraction _queryTriggers = QueryTriggerInteraction.Collide;

            [SerializeField]
            private LayerMask _queryLayerMask = (1 << -Everything) | ~(1 << IgnoreRaycast);

            [SerializeField]
            private int _handFeatureLayer;

            public float PalmRadiusNear
            {
                get { return _palmRadiusNear; }
            }

            public float PalmRadiusFar
            {
                get { return _palmRadiusFar; }
            }

            public QueryTriggerInteraction QueryTriggers
            {
                get { return _queryTriggers; }
            }

            public LayerMask QueryLayerMask
            {
                get { return _queryLayerMask; }
            }

            public float ClosestObjectDebounce
            {
                get { return _closestObjectDebounce; }
            }

            public int HandFeatureLayer
            {
                get { return _handFeatureLayer; }
            }
        }


        /// <summary>
        /// Class containing statics for hands operations.
        /// </summary>
        [Serializable]
        public class Stats
        {
            [Header("Read only")]

            [Readonly]
            [SerializeField]
            private bool _initilized = false;

            [Readonly]
            [SerializeField]
            private int _handsFrameID;
            [Readonly]
            [SerializeField]
            private int _handsInScene;

            public void MarkInitialized()
            {
                _initilized = true;
            }

            public void UpdateFrameData(int frameId, int handsInScene)
            {
                _handsFrameID = frameId;
                _handsInScene = handsInScene;
            }
        }

        #endregion Helper classes
    }
}