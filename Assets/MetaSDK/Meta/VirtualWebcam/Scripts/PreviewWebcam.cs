using UnityEngine;
using System.Collections.Generic;

namespace Meta
{
    /// <summary>
    /// Allows the user to preview the Virtual Webcam in one of the Unity Displays. PreviewWebcam also defines behaviour for when the Virtual Webcam is not being streamed.
    /// </summary>
    public class PreviewWebcam : MonoBehaviour
    {

        private List<IWebcamStateChangeListener> _listeners = new List<IWebcamStateChangeListener>();

        [SerializeField]
        private WebcamMirrorModes _targetDisplay;

        /// <summary>
        /// Denotes the display onto which the Virtual Webcam should be streamed.
        /// </summary>
        public WebcamMirrorModes TargetDisplay
        {
            get
            {
                return _targetDisplay;
            }

            set
            {
                _targetDisplay = value;
                OnWebcamStateChanged(value);
            }
        }

        private void Start()
        {
            if (_listeners != null && _listeners.Capacity == 0)
            {
                AddGameObjectHieracicalListeners();
                AddDefaultListeners();
                OnWebcamStateChanged(TargetDisplay);
            }
        }

        private void AddDefaultListeners()
        {
            _listeners.Add(new WebcamOffCanvasHandler());
            _listeners.Add(new WebcamUnityWindowHandler());
        }

        private void AddGameObjectHieracicalListeners()
        {
            var structuralListeners = GetComponentsInChildren<IWebcamStateChangeListener>();
            if (structuralListeners != null)
            {
                foreach (var listener in structuralListeners)
                {
                    _listeners.Add(listener);
                }
            }
        }


        private void OnWebcamStateChanged(WebcamMirrorModes modeChangedTo)
        {
            foreach (var listener in _listeners)
            {
                listener.OnStateChanged(modeChangedTo);
            }
        }

    }
}
