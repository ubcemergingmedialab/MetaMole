
namespace Meta.Buttons
{
    /// <summary>
    /// Enumerate the types of broadcasting when using Meta Button GameObject Event Broadcaster
    /// </summary>
    public enum ButtonBroadcastType
    {
        /// <summary>
        /// Do not broadcast
        /// </summary>
        None,

        /// <summary>
        /// Broadcast to the scene
        /// </summary>
        Scene,

        /// <summary>
        /// Broadcast to the children of the current GameObject
        /// </summary>
        Children,

        /// <summary>
        /// Broadcast to the parents of the current GameObject
        /// </summary>
        Parents
    }
}
