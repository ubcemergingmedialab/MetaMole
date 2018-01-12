
namespace Meta.Buttons
{
    /// <summary>
    /// Interface to process the Meta Button Events
    /// </summary>
    public interface IOnMetaButtonEvent
    {
        /// <summary>
        /// Process the Meta Button Event
        /// </summary>
        /// <param name="button">Button Message</param>
        void OnMetaButtonEvent(IMetaButton button);
    }
}
