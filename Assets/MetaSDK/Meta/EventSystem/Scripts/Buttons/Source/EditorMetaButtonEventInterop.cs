#if UNITY_EDITOR
using System.Collections.Generic;

namespace Meta.Buttons
{
    /// <summary>
    /// Editor Only class that helps emulate the device button events
    /// </summary>
    public class EditorMetaButtonEventInterop : IMetaButtonEventInterop
    {
        /// <summary>
        /// Static queue for Button Events.
        /// This is EDITOR ONLY to emulate button events
        /// </summary>
        public static Queue<IMetaButton> ButtonEvents = new Queue<IMetaButton>();

        /// <summary>
        /// Original interp implementation
        /// </summary>
        private IMetaButtonEventInterop _interop;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public EditorMetaButtonEventInterop()
        {
            _interop = new MetaButtonEventInterop();
        }

        /// <summary>
        /// Get a button event if available
        /// </summary>
        /// <returns>Button Event, null if there is no event</returns>
        public IMetaButton GetButtonEvent()
        {
            var originalResult = _interop.GetButtonEvent();
            if (originalResult != null)
            {
                return originalResult;
            }

            if (ButtonEvents.Count <= 0)
            {
                return null;
            }

            return ButtonEvents.Dequeue();
        }
    }
}
#endif