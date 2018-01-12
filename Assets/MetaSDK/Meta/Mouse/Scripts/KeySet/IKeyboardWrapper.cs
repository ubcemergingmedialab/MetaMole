using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Provides keyboard functionality
    /// </summary>
    public interface IKeyboardWrapper
    {
        /// <summary>
        /// Is a key pressed?
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        bool GetKey(KeyCode keyCode);
        /// <summary>
        /// Has a key just been released?
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        bool GetKeyUp(KeyCode keyCode);
        /// <summary>
        /// Has a key just been pressed?
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        bool GetKeyDown(KeyCode keyCode);
    }
}