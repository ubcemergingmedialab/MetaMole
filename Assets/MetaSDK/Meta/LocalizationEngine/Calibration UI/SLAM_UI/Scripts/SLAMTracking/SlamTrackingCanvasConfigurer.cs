using System.Collections;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Configures a canvas object so it is attached in front of the stereo cameras
    /// </summary>
    internal class SlamTrackingCanvasConfigurer : BaseSlamTrackingCanvasConfigurer
    {
        [SerializeField]
        private Canvas _targetCanvas;
        [SerializeField]
        private EventCamera _eventCamera;
        private Camera _targetCamera;

        /// <summary>
        /// Gets or sets the target canvas to configure
        /// </summary>
        public Canvas TargetCanvas
        {
            get { return _targetCanvas; }
            set { _targetCanvas = value; }
        }

        /// <summary>
        /// Gets or sets the Event Camera
        /// </summary>
        public EventCamera EventCamera
        {
            get { return _eventCamera; }
            set { _eventCamera = value; }
        }

        /// <summary>
        /// Automatically configure the Canvas attached to this GameObject.
        /// </summary>
        /// <returns>True if configuration was successful, false otherwise</returns>
        public override bool AutoConfigure()
        {
            if (_targetCanvas == null)
            {
                _targetCanvas = gameObject.GetComponent<Canvas>();
            }
            if (_eventCamera == null)
            {
                _eventCamera = GameObject.FindObjectOfType<EventCamera>();
            }
            return Configure();
        }

        /// <summary>
        /// Configures the canvas to render in from of the stereo cameras.
        /// This will attach the canvas to the Event Camera, adjust it's size and relative position.
        /// </summary>
        /// <returns>True if configuration was successful, false otherwise</returns>
        public override bool Configure()
        {
            if (_eventCamera == null)
            {
                Debug.LogError("Missing Event Camera");
                return false;
            }
            if (_targetCanvas == null)
            {
                Debug.LogError("Missing Target Canvas");
                return false;
            }

            _targetCamera = _eventCamera.GetComponent<Camera>();
            if (_targetCamera == null)
            {
                Debug.LogError("Event Camera does not have a Camera");
                return false;
            }
            _targetCanvas.transform.SetParent(_eventCamera.transform);
            StartCoroutine(ConfigureCanvas());

            return true;
        }

        private IEnumerator ConfigureCanvas()
        {
            _targetCanvas.worldCamera = _targetCamera;

            _eventCamera.enabled = true;
            _targetCanvas.planeDistance = 0.45f;
            // save target display
            var targetDisplayNumber = _targetCamera.targetDisplay;
            // Set display target to 0... Because of Unity...
            _targetCamera.targetDisplay = 0;
            // Set the render mode
            _targetCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            // Wait for the changes to be applied.
            yield return null;

            // Set the render mode to World space so the stereo cameras can see it.
            _targetCanvas.renderMode = RenderMode.WorldSpace;
            _eventCamera.enabled = false;

            // Restore target display number
            _targetCamera.targetDisplay = targetDisplayNumber;
        }
    }
}
