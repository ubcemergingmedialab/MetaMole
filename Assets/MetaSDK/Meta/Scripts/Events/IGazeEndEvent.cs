using UnityEngine.EventSystems;

namespace Meta
{
    /// <summary>
    /// Allows OnGazeEnd() event to be send to a GameObject
    /// </summary>
    public interface IGazeEndEvent : IEventSystemHandler
    {
        void OnGazeEnd();
    }
}