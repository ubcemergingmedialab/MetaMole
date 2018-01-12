using System.Runtime.InteropServices;

//todo better way of interop
namespace Meta
{
    /// <summary>   A point cloud interop meta data. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct PointCloudInteropMetaData
    {
        /// <summary>   true to valid. </summary>
        public bool valid;

        /// <summary>   Maximum size of the point cloud. </summary>
        public int maxSize;

        /// <summary>   Length of the field. </summary>
        public int fieldLength; 

        /// <summary>   SIZE 4 4 4 4. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public int[] fieldSize;

        /// <summary>   TYPE F F F F. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public char[] fieldType;

        /// <summary>   COUNT 1 1 1 1. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public int[] fieldCount;

        /// <summary>   x y z rgb. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public char[] fieldName;

    }
}