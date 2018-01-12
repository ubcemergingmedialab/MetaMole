using System;

namespace Meta.Buttons
{
    /// <summary>
    /// Retransmit the source button events
    /// </summary>
    public class MetaButtonEventBroadcaster : BaseMetaButtonEventBroadcaster, IMetaButtonEventProvider
    {
        private event Action<IMetaButton> _mainEvent;

        /// <summary>
        /// Process the button events
        /// </summary>
        /// <param name="button">Button event</param>
        protected override void ProcessButtonEvents(IMetaButton button)
        {
            if (_mainEvent == null)
            {
                return;
            }
            _mainEvent.Invoke(button);
        }

        /// <summary>
        /// Subscribe to the button events
        /// </summary>
        /// <param name="action">Action to register</param>
        public void Subscribe(Action<IMetaButton> action)
        {
            _mainEvent += action;
        }

        /// <summary>
        /// Unsubscribe to the button events
        /// </summary>
        /// <param name="action">Action to unregister</param>
        public void Unsubscribe(Action<IMetaButton> action)
        {
            _mainEvent -= action;
        }
    }
}
