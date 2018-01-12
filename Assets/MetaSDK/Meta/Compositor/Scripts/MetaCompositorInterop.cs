using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Meta
{
    public class MetaCompositorInterop
    {
        /// <summary>
        /// Enable/disable last minute warp in the compositor.  This is for debugging latewarp.
        /// </summary>
        /// <param name="enableLateWarp">bool for enable/disable</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "EnableLateWarp")]
        public static extern void EnableLateWarp(bool enableLateWarp);

        /// <summary>
        /// Late warp threshold within the compositor.  This is for debugging the latewarp threshold.
        /// </summary>
        /// <param name="lateWarpThreshold">float for threshold value</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "SetThresholdForLateWarp")]
        public static extern void SetThresholdForLateWarp(float lateWarpThreshold);

        /// <summary>
        /// Initialize the compositor
        /// The compositor will determine whether direct mode is enabled or not.
        /// Enabling the AsyncLateWarp creates a separate thread for post processing (Unwarping)
        /// meanwhile allowing the Unity render thread to continue Rendering asynchronously.
        /// </summary>
        /// <param name="enableAsyncLateWarp">bool for enable/disable</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "InitCompositor")]
        public static extern void InitCompositor(bool enableAsyncLateWarp);

        /// <summary>
        /// Shutdown compositor
        /// The plugin handles this itself, not sure if this is needed
        /// </summary>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "ShutdownCompositor")]
        public static extern void ShutdownCompositor();

        /// <summary>
        /// Enable/disable timewarp within the compositor
        /// </summary>
        /// <param name="enabletimewarp">bool for enable/disable</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "EnableTimewarp")]
        public static extern void EnableTimewarp(int enabletimewarp);


        /// <summary>
        /// Enable/disable hand occlusion in compositor
        /// </summary>
        /// <param name="enablehandocclusion">bool for enable/disable</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "EnableHandOcclusion")]
        public static extern void EnableHandOcclusion(bool state);


        /// <summary>
        /// Sets temporal filtering momentum of hand occlusion
        /// </summary>
        /// <param name="enablehandocclusion">float for setting temporal filtering momentum of hand occlusion</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "SetHandOcclusionTemporalMomentum")]
        public static extern void SetHandOcclusionTemporalMomentum(float momentum);


        /// <summary>
        /// Sets temporal filtering momentum of hand occlusion
        /// </summary>
        /// <param name="enablehandocclusion">float for setting temporal filtering momentum of hand occlusion</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "SetHandOcclusionFeatherSize")]
        public static extern void SetHandOcclusionFeatherSize(int size);


        /// <summary>
        /// Sets how fast the opacity of the hand occlusion falls off
        /// </summary>
        /// <param name="enablehandocclusion">float; sets how fast the opacity of the hand occlusion falls off</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "SetHandOcclusionFeatherOpacityFalloff")]
        public static extern void SetHandOcclusionFeatherOpacityFalloff(float exponent);

        /// <summary>
        /// Sets a opacity threshold of handocclusion below which pixels would be thrown out.
        /// </summary>
        /// <param name="sethandocclusionfeatheropacitycutoff">float; sets a opacity threshold of handocclusion below which pixels would be thrown out.</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "SetHandOcclusionFeatherOpacityCutoff")]
        public static extern void SetHandOcclusionFeatherOpacityCutoff(float exponent);


        /// <summary>
        /// Set the amount of prediction used for timewarp correction at end of frame.
        /// </summary>
        /// <param name="dt">time in seconds</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "SetTimewarpPrediction")]
        public static extern void SetTimewarpPrediction(float dt);

        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "GetEyeRenderTargetWidth")]
        public static extern int GetEyeRenderTargetWidth();

        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "GetEyeRenderTargetHeight")]
        public static extern int GetEyeRenderTargetHeight();


        /// <summary>
        /// Set the render targets and depth buffers from an external engine
        /// </summary>
        /// <param name="leftRT"></param>
        /// <param name="rightRT"></param>
        /// <param name="leftEyeDepth"></param>
        /// <param name="rightEyeDepth"></param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "SetEyeRenderTargets")]
        public static extern void SetEyeRenderTargets(
            IntPtr leftRT, IntPtr rightRT, IntPtr leftEyeDepth, IntPtr rightEyeDepth);

        /// <summary>
        /// Call this prior to rendering a frame
        /// This sets up the pose for timewarp to calculate delta
        /// </summary>
        /// <returns></returns>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "BeginFrame")]
        public static extern void BeginFrame();

        /// <summary>
        /// Render function
        /// Calls EndFrame within the compositor, renders whatever is in the target texture
        /// </summary>
        /// <returns>Render function</returns>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "GetRenderEventFunc")]
        public static extern IntPtr GetRenderEventFunc();

        /// <summary>
        /// Get view matrix for camera
        /// </summary>
        /// <returns>Render function</returns>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "GetViewMatrix")]
        public static extern void getViewMatrix(int eye, ref IntPtr ptrResultViewMatrix);

        /// <summary>
        /// Get SLAM pose used for this frames rendering
        /// This will return the pose set in begin frame
        /// (Used in MetaSlamInterop)
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="rotation"></param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "GetRenderPoseToWorld")]
        public static extern void GetRenderPoseToWorld([MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] double[] translation,
                                                 [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] double[] rotation);

        /// <summary>
        /// Get projection matrix for camera
        /// </summary>
        /// <returns>Render function</returns>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "GetProjectionMatrix")]
        public static extern void getProjectionMatrix(int eye, ref IntPtr ptrResultProjectionMatrix);

        /// <summary>
        /// Enable/disable latency addition within the compositor.  This is for debugging timewarp.
        /// </summary>
        /// <param name="enabletimewarp">bool for enable/disable</param>
        [DllImport(Meta.DllReferences.MetaVisionDLLName, EntryPoint = "SetAddLatency")]
        public static extern void SetAddLatency(bool enableLatency);

        private static void MarshalAndCopy(IntPtr matPtr, ref Matrix4x4 mat)
        {
            float[] matMarshalled = new float[16];
            Marshal.Copy(matPtr, matMarshalled, 0, 16);

            // fill matrix
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                    mat[j, i] = matMarshalled[i * 4 + j];
        }


        public static void GetViewMatrix(int eye, ref Matrix4x4 viewMatrix)
        {
            IntPtr viewMatrixPtr = IntPtr.Zero;

            getViewMatrix(eye, ref viewMatrixPtr);

            MarshalAndCopy(viewMatrixPtr, ref viewMatrix);
        }

        public static void GetProjectionMatrix(int eye, ref Matrix4x4 projectionMatrix)
        {
            IntPtr projectionMatrixPtr = IntPtr.Zero;

            getProjectionMatrix(eye, ref projectionMatrixPtr);

            MarshalAndCopy(projectionMatrixPtr, ref projectionMatrix);
        }
    }
}
