using System;
using Meta.Utility;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Modifies the Unity window properties if the Webcam Mirror Mode is changed.
    /// </summary>
    public class WebcamUnityWindowHandler : IWebcamStateChangeListener
    {

        private const int MinimizeWindow = 2;

        /// <summary>
        /// When the Mirror Mode is changed the window will be minimized
        /// </summary>
        /// <param name="changedToMode"></param>
        public void OnStateChanged(WebcamMirrorModes changedToMode)
        {
            Screen.SetResolution(1280, 720, false);
            if (changedToMode == WebcamMirrorModes.None && !Application.isEditor)
            {
                IntPtr unityWindowHandle = UnityWindowHandleUtility.GetUnityWindowHandle();
                if (unityWindowHandle != IntPtr.Zero)
                {
                    User32interop.ShowWindowAsync(unityWindowHandle, MinimizeWindow);
                }
                
            }
        }
    }
}
