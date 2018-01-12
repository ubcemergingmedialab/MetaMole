using System;

namespace Meta.Buttons
{
    /// <summary>
    /// Handles setup and execution of localization for the MetaWorld prefab.
    /// </summary>
    internal class MetaButtonEventProvider : IEventReceiver, IMetaButtonEventProvider
    {
        private IMetaButtonEventInterop _interop;
        private event Action<IMetaButton> _mainEvent;
        private ButtonType _lastType;
        private ButtonState _lastState;

        public MetaButtonEventProvider()
        {
#if UNITY_EDITOR
            _interop = new EditorMetaButtonEventInterop();
#else
            _interop = new MetaButtonEventInterop();
#endif
        }

        /// <summary>
        /// Initalises the events for the module.
        /// </summary>
        /// <param name="eventHandlers"></param>
        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnUpdate(Update);
        }

        /// <summary>
        /// Calls the update loop to get new values from the localizer.
        /// </summary>
        private void Update()
        {
            IMetaButton buttonEvent = null;

            // Pull buttons until button queue is exhausted for this update.
            while ((buttonEvent = _interop.GetButtonEvent()) != null)
            {
                // Make sure we send a Release event
                if (buttonEvent.Type != _lastType && _lastState != ButtonState.ButtonRelease)
                {
                    var forceRelease = new MetaButton(_lastType, ButtonState.ButtonRelease, buttonEvent.Timestamp);
                    SendEvent(forceRelease);
                }
                SendEvent(buttonEvent);

                _lastType = buttonEvent.Type;
                _lastState = buttonEvent.State;
            }
        }

        /// <summary>
        /// Send the event
        /// </summary>
        /// <param name="button">Button event</param>
        private void SendEvent(IMetaButton button)
        {
            if (_mainEvent != null)
            {
                _mainEvent.Invoke(button);
            }
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