using UnityEngine;
using System.Collections;

namespace Meta
{
    /// <summary>
    /// Controls the slam initialization UI process.
    /// </summary>
    public class SLAMInitializationProcess : MetaBehaviour
    {
        public enum SlamInitializationType
        {
            NewMap,
            LoadingMap
        }

        private Coroutine _calibration;

        /// <summary>
        /// Begins the calibration process.
        /// </summary>
        /// <param name="initializationType"></param>
        public void Begin(SlamInitializationType initializationType)
        {
            //prevent runaway calibration process
            if (_calibration != null)
            {
                StopCoroutine(_calibration);
            }

            _calibration = StartCoroutine(FullCalibration(initializationType));
        }

        /// <summary>
        /// Cleanly destroy the UI so that it does not leave unused game objects behind.
        /// </summary>
        public void Cleanup()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Run UI
        /// </summary>
        /// <param name="initializationType"></param>
        private IEnumerator FullCalibration(SlamInitializationType initializationType)
        {
            yield return SensorInitialization();

            SLAMUIManager slamInitManager = GetComponentInChildren<SLAMUIManager>();
            yield return slamInitManager.RunFullCalibration(initializationType);
            // yield return StartCoroutine(canvas.FindCanvasOrigin());

            yield return null;

            Cleanup();
        }

        private IEnumerator SensorInitialization()
        {
            // wait for SLAM cameras to be initialized
            var localizer = metaContext.Get<MetaLocalization>().GetLocalizer() as SlamLocalizer;

            if (localizer != null && localizer.SlamFeedback != null)
            {
                while (!(localizer.SlamFeedback.CameraReady))
                {
                    yield return null;
                }
            }
        }
    }
}