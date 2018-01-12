using UnityEngine;

namespace Meta
{
    public interface IEventCamera
    {
        /// <summary>
        /// The EventCamera's Camera component
        /// </summary>
        Camera EventCameraRef { get; }

        Vector3 Position { get; }
    }
}