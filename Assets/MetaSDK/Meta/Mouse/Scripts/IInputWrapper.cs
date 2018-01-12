using UnityEngine;

namespace Meta.Mouse
{
    /// <summary>
    /// Interface for wrapper around input
    /// </summary>
    public interface IInputWrapper
    {
        CursorLockMode LockState { get; set; }

        /// <summary>
        /// Set visibility on cursor.
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Gets the local mouse position relative to the Unity game window
        /// </summary>
        Vector3 GetMousePosition();

        /// <summary>
        /// Gets scrolling of mouse wheel
        /// </summary>
        Vector2 GetMouseScrollDelta();

        /// <summary>
        /// Gets the movement of a mouse axis
        /// </summary>
        float GetAxis(string axisName);

        /// <summary>
        /// Gets whether the mouse button is currently pressed
        /// </summary>
        bool GetMouseButton(int button);

        /// <summary>
        /// Gets whether a mouse button was unclicked that frame
        /// </summary>
        bool GetMouseButtonUp(int button);

        /// <summary>
        /// Gets whether a mouse button was clicked that frame
        /// </summary>
        bool GetMouseButtonDown(int button);

        /// <summary>
        /// Gets the rect of the Unity game window in pixels
        /// </summary>
        Rect GetScreenRect();
    }
}