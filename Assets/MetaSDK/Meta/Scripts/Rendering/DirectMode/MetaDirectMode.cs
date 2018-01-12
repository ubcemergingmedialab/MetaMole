using UnityEngine;

namespace Meta.DisplayMode.DirectMode
{
    /// <summary>
    /// Put this on the UnwarpingCamera to enable DirectMode.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class MetaDirectMode : MonoBehaviour
    {
        // Camera to render to device
        private Camera _mainCamera;
        private bool _isDirectModeInitialized = false;

        /// <summary>
        /// Sent to all game objects before the application is quit.
        /// Ref: https://docs.unity3d.com/Manual/ExecutionOrder.html
        /// </summary>
        private void OnApplicationQuit()
        {
#if UNITY_EDITOR
            MetaDirectModeInterop.RegisterDebugCallback(false);
#endif
            if (enabled && _isDirectModeInitialized)
            {
                //Debug.Log("Destroying the direct mode session");
                MetaDirectModeInterop.DestroyDirectMode();
                _isDirectModeInitialized = false;
            }
        }

        /// <summary>
        /// Used in the Editor when not using PlayMode.
        /// OnPostRender is called after a camera finished rendering the scene.
        /// Ref: https://docs.unity3d.com/Manual/ExecutionOrder.html
        /// </summary>
        private void OnPostRender()
        {
            if (Application.isPlaying && _isDirectModeInitialized)
            {
                GL.IssuePluginEvent(MetaDirectModeInterop.GetRenderEventFunc(), 1);
            }
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled () or inactive.
        /// Ref: https://docs.unity3d.com/Manual/ExecutionOrder.html
        /// </summary>
        private void OnDisable()
        {
            if (enabled && _isDirectModeInitialized)
            {
                //Debug.Log("Disabling direct mode session");
                var rT = _mainCamera.targetTexture;
                if (rT)
                {
                    rT.Release();
                    rT = null;
                }
                _mainCamera.targetTexture = null;
            }
        }

        private void SetCameraTexture()
        {
            _mainCamera = GetComponent<Camera>();
            var rT = new RenderTexture(2560, 1440, 24, RenderTextureFormat.ARGB32);
            rT.autoGenerateMips = false;
            rT.filterMode = FilterMode.Point;
            rT.Create();
            _mainCamera.targetTexture = rT;
            MetaDirectModeInterop.SetTextureFromUnity(rT.GetNativeTexturePtr());
        }

        /// <summary>
        /// Start direct mode
        /// </summary>
        public void StartDirectMode()
        {
            //Avoid starting direct mode twice.
            if (!enabled || _isDirectModeInitialized)
            {
                return;
            }

            _isDirectModeInitialized = false;

            //Adding this check to avoid crashing the application out of Unity editor. We are receiving a callback after the application is closed.
#if UNITY_EDITOR
            MetaDirectModeInterop.RegisterDebugCallback(true);
#endif
            if (Application.isPlaying && enabled)
            {
                if (MetaDirectModeInterop.InitDirectMode())
                {
                    //Debug.Log("Initialized direct mode session!!");
                    SetCameraTexture();
                    _isDirectModeInitialized = true;
                }
                else
                {
                    enabled = false;
                }
            }
        }
    }
}