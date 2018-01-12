using System.Runtime.InteropServices;

namespace Meta.Internal
{
    //todo: a better system to get this data
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    internal struct SensorMetaData
    {
        /// <summary>   The height. </summary>
        public int height;
        /// <summary>   The width. </summary>
        public int width;
        /// <summary>   The focal length x coordinate. </summary>
        public float focalLengthX;
        /// <summary>   The focal length y coordinate. </summary>
        public float focalLengthY;
        /// <summary>   The principal point x coordinate. </summary>
        public float principalPointX;
        /// <summary>   The principal point y coordinate. </summary>
        public float principalPointY;
    }
}