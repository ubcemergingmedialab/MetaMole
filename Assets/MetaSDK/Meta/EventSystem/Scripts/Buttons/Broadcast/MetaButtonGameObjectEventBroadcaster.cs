using UnityEngine;

namespace Meta.Buttons
{
    /// <summary>
    /// Broadcast the button events from the headset via GameObject.FindObjectsOfType.
    /// This class is not performant but is the easiest way to interact with buttons.
    /// For more performant implementations please see MetaButtonIndividualEventBroadcaster, MetaButtonGeneralEventBroadcaster or MetaButtonEventBroadcaster.
    /// </summary>
    public class MetaButtonGameObjectEventBroadcaster : BaseMetaButtonEventBroadcaster
    {
        [SerializeField]
        private ButtonBroadcastType _broadcastType;

        /// <summary>
        /// Process the button events
        /// </summary>
        /// <param name="button">Button event</param>
        protected override void ProcessButtonEvents(IMetaButton button)
        {
            switch (_broadcastType)
            {
                case ButtonBroadcastType.None:
                    break;
                case ButtonBroadcastType.Scene:
                    BroadcastToAll(button);
                    break;
                case ButtonBroadcastType.Children:
                    BroadcastChildren(button);
                    break;
                case ButtonBroadcastType.Parents:
                    BroadcastParent(button);
                    break;
            }
        }

        /// <summary>
        /// Broadcast to all GameObjects in the scene
        /// </summary>
        /// <param name="button">Button event</param>
        private void BroadcastToAll(IMetaButton button)
        {
            var objects = GameObject.FindObjectsOfType<BaseMetaButtonInteractionObject>();
            for (int i = 0; i < objects.Length; ++i)
            {
                objects[i].OnMetaButtonEvent(button);
            }
        }

        /// <summary>
        /// Broadcast to the children of the current GameObject
        /// </summary>
        /// <param name="button">Button event</param>
        private void BroadcastChildren(IMetaButton button)
        {
            var objects = GetComponentsInChildren<IOnMetaButtonEvent>();
            for (int i = 0; i < objects.Length; ++i)
            {
                objects[i].OnMetaButtonEvent(button);
            }
        }

        /// <summary>
        /// Broadcast to the parents of the current GameObject
        /// </summary>
        /// <param name="button">Button event</param>
        private void BroadcastParent(IMetaButton button)
        {
            var objects = GetComponentsInParent<IOnMetaButtonEvent>();
            for (int i = 0; i < objects.Length; ++i)
            {
                objects[i].OnMetaButtonEvent(button);
            }
        }

        /// <summary>
        /// Gets or sets the Broadcast type of this broadcaster
        /// </summary>
        public ButtonBroadcastType BroadcastType
        {
            get { return _broadcastType; }
            set { _broadcastType = value; }
        }
    }
}
