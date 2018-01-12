using System;
using System.Text;

namespace Meta.Utility
{

    /// <summary>
    /// Utilities for the Unity Game Window.
    /// </summary>
    public class UnityWindowHandleUtility
    {

        /// <summary>
        /// Gets the hwnd of the Unity Game Window.
        /// </summary>
        /// <returns>The Unity Game Window hwnd</returns>
        public static IntPtr GetUnityWindowHandle()
        {
            IntPtr windowHandle = new IntPtr(0);
            do
            {
                windowHandle = User32interop.FindWindowEx(new IntPtr(0), windowHandle, null, null);
                string name = GetClassNameOfWindow(windowHandle);
                if (name.Equals("UnityWndClass"))
                {
                    break;
                }
            } while (windowHandle != IntPtr.Zero);
            return windowHandle;
        }

        private static string GetClassNameOfWindow(IntPtr hwnd)
        {
            int clsMaxLength = 1000;
            StringBuilder classText = new StringBuilder("", clsMaxLength + 5);
            User32interop.GetClassName(hwnd, classText, clsMaxLength + 2);

            if (!String.IsNullOrEmpty(classText.ToString()) && !String.IsNullOrEmpty(classText.ToString()))
            {
                return classText.ToString();
            }

            return string.Empty;
        }
    }
}
