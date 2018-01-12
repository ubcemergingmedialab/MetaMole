using UnityEngine;

namespace Meta.Buttons
{
    /// <summary>
    /// Base class to broadcast Button Events
    /// </summary>
    public abstract class BaseMetaButtonEventBroadcaster : MetaBehaviour
    {
        private IMetaButtonEventProvider _provider;
        private bool _registered = false;

        /// <summary>
        /// Process the button events
        /// </summary>
        /// <param name="button">Button event</param>
        protected abstract void ProcessButtonEvents(IMetaButton button);

        /// <summary>
        /// Register to the button events
        /// </summary>
        private void OnEnable()
        {
            if (_registered)
                return;
            if (_provider == null)
            {
                var context = metaContext;
                if (context == null)
                {
                    Debug.LogWarning("Could not get Meta Context. Button events will not be provided");
                    return;
                }

                if (!context.ContainsModule<IMetaButtonEventProvider>())
                {
                    Debug.LogWarning("Could not get Meta Button Event Provider. Button events will not be broadcasted");
                    return;
                }
                _provider = context.Get<IMetaButtonEventProvider>();
            }

            _provider.Subscribe(ProcessButtonEvents);
            _registered = true;
        }

        /// <summary>
        /// Unregister to the button events
        /// </summary>
        private void OnDisable()
        {
            if (!_registered)
            {
                return;
            }
            if (_provider == null)
            {
                return;
            }

            _provider.Unsubscribe(ProcessButtonEvents);
            _registered = false;
        }
    }
}
