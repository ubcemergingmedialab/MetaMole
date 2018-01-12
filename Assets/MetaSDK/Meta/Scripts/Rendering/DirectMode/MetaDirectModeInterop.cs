using System;
using System.Runtime.InteropServices;

namespace Meta.DisplayMode.DirectMode
{
    /// <summary>
    /// Wrapper class to interact with Direct more via C++
    /// </summary>
    internal static class MetaDirectModeInterop
    {
        private const string DLLName = "MetaDirectMode";
        private delegate void DebugCallback(string message);

        #region Internal
        // Native plugin rendering events are only called if a plugin is used
        // by some script. This means we have to DllImport at least
        // one function in some active script.
        // For this example, we'll call into plugin's SetTimeFromUnity
        // function and pass the current time so the plugin can animate.
        [DllImport(DLLName, EntryPoint = "SetTimeFromUnity")]
        private static extern void DLL_SetTimeFromUnity(float t);

        // We'll also pass native pointer to a texture in Unity.
        // The plugin will fill texture data from native code.
        [DllImport(DLLName, EntryPoint = "SetTextureFromUnity")]
        private static extern void DLL_SetTextureFromUnity(IntPtr texture);

        [DllImport(DLLName, EntryPoint = "SetUnityStreamingAssetsPath")]
        private static extern void DLL_SetUnityStreamingAssetsPath([MarshalAs(UnmanagedType.LPStr)] string path);

        [DllImport(DLLName, EntryPoint = "GetRenderEventFunc")]
        private static extern IntPtr DLL_GetRenderEventFunc();

        [DllImport(DLLName, EntryPoint = "InitDirectMode")]
        private static extern bool DLL_InitDirectMode();

        [DllImport(DLLName, EntryPoint = "DestroyDirectMode")]
        private static extern void DLL_DestroyDirectMode();

        [DllImport(DLLName, EntryPoint = "RegisterDebugCallback")]
        private static extern void DLL_RegisterDebugCallback(DebugCallback callback);
        #endregion

        /// <summary>
        /// Register a DebugCallback to expose messages from DLL
        /// </summary>
        /// <param name="register">Register Debug Callback or Unregister</param>
        public static void RegisterDebugCallback(bool register)
        {
            try
            {
                if (register)
                    DLL_RegisterDebugCallback(new DebugCallback(DebugMethod));
                else
                    DLL_RegisterDebugCallback(null);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogErrorFormat("Exception on RegisterDebugCallback: {0}", exception.Message);
            }
        }

        /// <summary>
        /// Initialize Direct Mode
        /// </summary>
        public static bool InitDirectMode()
        {
            bool result = false;
            try
            {
                result = DLL_InitDirectMode();
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogErrorFormat("Exception on Initializing Direct Mode: {0}", exception.Message);
            }
            return result;
        }

        /// <summary>
        /// Destroy Direct Mode Session
        /// </summary>
        public static void DestroyDirectMode()
        {
            try
            {
                DLL_DestroyDirectMode();
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogErrorFormat("Exception on Destroying Direct Mode Session: {0}", exception.Message);
            }
        }

        /// <summary>
        /// Set a unity texture pointer where to render to Direct Mode
        /// </summary>
        /// <param name="texture">Texture Pointer</param>
        public static void SetTextureFromUnity(IntPtr texture)
        {
            try
            {
                DLL_SetTextureFromUnity(texture);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogErrorFormat("Exception when setting texture pointer: {0}", exception.Message);
            }
        }

        /// <summary>
        /// Get the render event function pointer.
        /// Return default pointer if there is an error.
        /// </summary>
        /// <returns>Render event function pointer.</returns>
        public static IntPtr GetRenderEventFunc()
        {
            IntPtr pointer = default(IntPtr);
            try
            {
                pointer = DLL_GetRenderEventFunc();
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogErrorFormat("Exception when getting render event function: {0}", exception.Message);
            }

            return pointer;
        }

        /// <summary>
        /// Debug Method for exposing messages from C++
        /// </summary>
        /// <param name="message">Debug message</param>
        private static void DebugMethod(string message)
        {
            //UnityEngine.Debug.Log("Meta Direct Mode: " + message);
        }
    }
}
