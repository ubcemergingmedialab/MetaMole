using UnityEngine;

namespace Meta
{
    internal class WebcamOffCanvasHandler : IWebcamStateChangeListener
    {
        private GameObject _webcamUi;

        /// <summary>
        /// Handles assignment/removal of the canvas shown in place of the Webcam.
        /// </summary>
        /// <param name="changedToMode"></param>
        public void OnStateChanged(WebcamMirrorModes changedToMode)
        {
            if (changedToMode == WebcamMirrorModes.None)
            {
                if (!_webcamUi)
                {
                    GameObject UiResourceRef = (GameObject)Resources.Load("Prefabs/MetaLogoCanvas");
                    _webcamUi = GameObject.Instantiate(UiResourceRef);
                    _webcamUi.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
                    foreach (Transform t in _webcamUi.transform)
                    {
                        t.gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;

                    }
                }
            }
            else if(_webcamUi != null)
            {
                GameObject.Destroy(_webcamUi);
                _webcamUi = null;
            }
        }

    }
}
