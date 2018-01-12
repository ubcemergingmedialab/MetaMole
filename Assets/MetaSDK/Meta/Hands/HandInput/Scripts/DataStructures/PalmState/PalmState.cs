namespace Meta.HandInput
{
    /// <summary>
    /// Enumorations of hand's palm state.
    /// </summary>
    public enum PalmState
    {
        /// <summary>
        /// Palm is not near any IInteractible Objects
        /// </summary>
        Idle,
        /// <summary>
        /// Palm is near at least one IInteractible Objects, but not grabbing any.
        /// </summary>
        Hovering,
        /// <summary>
        /// Palm is grabbing an IInteractible Object.
        /// </summary>
        Grabbing,
    }
}