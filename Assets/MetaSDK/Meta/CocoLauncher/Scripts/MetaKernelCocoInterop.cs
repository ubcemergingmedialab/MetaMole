using System.Runtime.InteropServices;

namespace Meta
{
    /// <summary>
    /// Class containing kernel-coco interop related datastructures and methods.
    /// </summary>
    public static class MetaKernelCocoInterop
    {

        #region C API Methods

        /// <summary>
        /// Instructs Kernel to initialize and connect coco to process.
        /// </summary>
        [DllImport("MetaVisionDLL", EntryPoint = "ConnectCoco")]
        internal static extern int ConnectCoco();

        /// <summary>
        /// Instructs Kernel to disconnect coco.
        /// </summary>
        [DllImport("MetaVisionDLL", EntryPoint = "DisconnectCoco")]
        internal static extern int DisconnectCoco();

        /// <summary>
        /// Instructs Kernel to resume coco.
        /// </summary>
        [DllImport("MetaVisionDLL", EntryPoint = "ResumeCoco")]
        internal static extern int ResumeCoco();


        /// <summary>
        /// Instructs Kernel to suspond coco.
        /// </summary>
        [DllImport("MetaVisionDLL", EntryPoint = "SupendCoco")]
        internal static extern int SupendCoco();

        #endregion C API Methods

        #region C API Method Wrappers

        /// <summary>
        /// Initializes coco launcher from MetaKernel.
        /// </summary>
        public static void Start()
        {
            MetaCocoInterop.EnsureMaskExists();
            MetaKernelCocoInterop.ConnectCoco();

            UnityEngine.Application.runInBackground = true;
        }

        /// <summary>
        /// Stops currently running coco instance.
        /// </summary>
        public static void Stop()
        {
            MetaKernelCocoInterop.DisconnectCoco();
        }

        #endregion C API Methods
    }
}