using UnityEngine;
using Meta.Buttons;

namespace Meta
{
    /// <summary>
    /// Base abstract class for implementing custom button interactions.
    /// This is just one way of using the button events.
    /// </summary>
    public abstract class BaseMetaButtonInteractionObject : MonoBehaviour, IOnMetaButtonEvent
    {
        /// <summary>
        /// Process the Meta Button Event
        /// </summary>
        /// <param name="button">Button Message</param>
        public abstract void OnMetaButtonEvent(IMetaButton button);
    }
}
