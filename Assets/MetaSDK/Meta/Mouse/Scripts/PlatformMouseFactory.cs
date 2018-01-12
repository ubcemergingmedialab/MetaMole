using UnityEngine;
using System;

namespace Meta.Mouse
{
    internal static class PlatformMouseFactory
    {
        public static IPlatformMouse GetPlatformMouse()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return new WindowsPlatformMouse();
            }
            throw new PlatformNotSupportedException("Unsupported platform for MetaMouse");
        }
    }
}