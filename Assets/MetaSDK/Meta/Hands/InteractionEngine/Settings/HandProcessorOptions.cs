using System.Runtime.InteropServices;
namespace Meta
{
    /// <summary>  Encapsulates options for hand processor. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct HandProcessorOptions
    {
        /// <summary>   The grab threshold. </summary>
        /// todo: maybe deprecated
        public float grabThresh;
        /// <summary>   debug display of hand processing. </summary>
        public bool debugDisplay;
        /// <summary>   enable kalman filtering of hand data. </summary>
        public bool enableKalman;
        /// <summary>   true to camera rotated 180. </summary>
        //todo: maybe deprecated
        public bool camera_rotated_180;
    }
}