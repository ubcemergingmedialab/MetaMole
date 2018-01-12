using System.Runtime.InteropServices;

namespace Meta
{
    /// <summary>
    /// Data structure to marshall data from Win API calls.
    /// </summary>
    public struct Win32Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}