using System.Runtime.InteropServices;

namespace Meta
{
    /// <summary>
    /// Data structure to marshall point data from Win API calls.
    /// </summary>
    public struct Win32Point
    {
        public int X;
        public int Y;

        public Win32Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}