using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Wraps the Unity Input system's keyboard functions
    /// </summary>
    public class UnityKeyboardWrapper : IKeyboardWrapper
    {
        public bool GetKey(KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }

        public bool GetKeyUp(KeyCode keyCode)
        {
            return Input.GetKeyUp(keyCode);
        }

        public bool GetKeyDown(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }
    }
}