using UnityEngine;

namespace Meta.Buttons
{
    /// <summary>
    /// Broadcast the button events from the headset via events
    /// </summary>
    public class MetaButtonGeneralEventBroadcaster : BaseMetaButtonEventBroadcaster
    {
        [SerializeField]
        private MetaButtonUnityEvent _mainEvent;

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
    }
}
