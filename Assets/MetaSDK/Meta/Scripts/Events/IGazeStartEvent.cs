using UnityEngine.EventSystems;

namespace Meta
{
    /// <summary>
    /// Allows OnGazeStart() event to be send to a GameObject
    /// </summary>
    public interface IGazeStartEvent : IEventSystemHandler
    {
        void OnGazeStart();
    }
}