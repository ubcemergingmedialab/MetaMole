using System;

namespace Meta.Buttons
{
    /// <summary>
    /// Interface to connect to the button events
    /// </summary>
    public interface IMetaButtonEventProvider
    {
        /// <summary>
        /// Subscribe to the button events
        /// </summary>
        /// <param name="action">Action to register</param>
        void Subscribe(Action<IMetaButton> action);

        /// <summary>
        /// Unsubscribe to the button events
        /// </summary>
        /// <param name="action">Action to unregister</param>
        void Unsubscribe(Action<IMetaButton> action);
    }
}
