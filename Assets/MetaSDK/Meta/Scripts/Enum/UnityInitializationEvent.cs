namespace Meta.GeneralEnum
{
    /// <summary>
    /// Represents a Unity Initialization Event.
    /// Ref: https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    enum UnityInitializationEvent
    {
        /// <summary>
        /// Represents the Awake event
        /// </summary>
        Awake,

        /// <summary>
        /// Represents the OnEnable event
        /// </summary>
        OnEnable,

        /// <summary>
        /// Represents the Start event
        /// </summary>
        Start
    }
}
