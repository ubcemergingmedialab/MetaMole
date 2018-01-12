using System.Runtime.InteropServices;

namespace Meta
{
    /// <summary>
    ///     Options for cloud generation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct CloudGeneratorOptions
    {
        /// <summary>
        ///     The radius to cluster data in.
        /// </summary>
        public float clusterRadius;

        /// <summary>   turn on for debug display from kernel. </summary>
        public bool debugDisplay;
    }
}