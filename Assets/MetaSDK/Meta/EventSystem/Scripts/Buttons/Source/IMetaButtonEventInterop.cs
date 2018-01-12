namespace Meta.Buttons
{
    /// <summary>
    /// Interface to get Button events from Interop
    /// </summary>
    public interface IMetaButtonEventInterop
    {
        /// <summary>
        /// Get a button event if available
        /// </summary>
        /// <returns>Button Event, null if there is no event</returns>
        IMetaButton GetButtonEvent();
    }
}
