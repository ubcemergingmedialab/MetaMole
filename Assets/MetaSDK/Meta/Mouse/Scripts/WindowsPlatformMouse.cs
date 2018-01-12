using UnityEngine;

namespace Meta.Mouse
{
    internal class WindowsPlatformMouse : IPlatformMouse
    {
        public void SetGlobalCursorPos(Vector2 pos)
        {
            User32interop.SetCursorPos((int)pos.x, (int)pos.y);
        }

        public Vector2 GetGlobalCursorPos()
        {
            Win32Point pos;
            User32interop.GetCursorPos(out pos);
            return new Vector2(pos.X, pos.Y);
        }
    }
}