using UnityEngine;

namespace Meta.Mouse
{
    internal interface IPlatformMouse
    {
        void SetGlobalCursorPos(Vector2 pos);
        Vector2 GetGlobalCursorPos();
    }
}