using UnityEngine;
using UnityEngine.EventSystems;

namespace Meta
{
    /// <summary>
    ///     Sends touch events to a touchable object.
    /// </summary>
    public interface ITouchEventHandler : IEventSystemHandler
    {
        /// <summary>
        ///     Called on a touchable physics object when touched by the hand cloud.
        /// </summary>
        /// <param name="pos"></param>
        void OnTouchEnter(Vector3 pos);

        /// <summary>
        ///     Called on a touchable physics object when no longer touched by the hand cloud.
        /// </summary>
        /// <param name="pos"></param>
        void OnTouchExit(Vector3 pos);

        /// <summary>
        ///     Called on a touch dwellable physics object when continuously touched for a specified time.
        /// </summary>
        void OnTouchDwell();
    }
}