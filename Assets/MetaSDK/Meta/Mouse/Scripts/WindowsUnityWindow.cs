using System;

namespace Meta.Mouse
{
    public class WindowsUnityWindow
    {
        private IntPtr _unityHWnd;

        public WindowsUnityWindow()
        {
            _unityHWnd = User32interop.GetForegroundWindow();
        }

        public IntPtr GetUnityWindowHandle()
        {
            return _unityHWnd;
        }

        public Win32Point GetUnityWindowCenter()
        {
            Win32Rect rect;
            User32interop.GetWindowRect(_unityHWnd, out rect);
            return new Win32Point((rect.left + rect.right) / 2, (rect.bottom + rect.top) / 2);
        }

        public void SetUnityWindowForeground()
        {
            User32interop.SetActiveWindow(_unityHWnd);
        }
    }
}