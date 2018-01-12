 using System.Collections;
using System.Collections.Generic;
 using Meta;
 using UnityEngine;
 using UnityEngine.Serialization;

namespace Meta
{
    /// <summary>
    /// Class containing all grab events
    /// </summary>
    [System.Serializable]
    public class InteractionEvents
    {
        /// <summary>
        /// Unity event for when a grab occurs.
        /// </summary>
        [FormerlySerializedAs("OnEngagedEvent")]
        public MetaInteractionDataEvent _engaged = new MetaInteractionDataEvent();

        /// <summary>
        /// Unity event for when a release occurs.
        /// </summary>
        [FormerlySerializedAs("OnDisengagedEvent")]
        public MetaInteractionDataEvent _disengaged = new MetaInteractionDataEvent();

        /// <summary>
        /// Unity event for when a hover starts.
        /// </summary>
        [FormerlySerializedAs("OnHoverStartEvent")]
        public MetaInteractionDataEvent _hoverStart = new MetaInteractionDataEvent();

        /// <summary>
        /// Unity event for when a hover ends.
        /// </summary>
        [FormerlySerializedAs("OnHoverEndEvent")]
        public MetaInteractionDataEvent _hoverEnd = new MetaInteractionDataEvent();

        /// <summary>
        /// Unity event for when a grab occurs.
        /// </summary>
        public MetaInteractionDataEvent Engaged
        {
            get { return _engaged; }
        }

        /// <summary>
        /// Unity event for when a release occurs.
        /// </summary>
        public MetaInteractionDataEvent Disengaged
        {
            get { return _disengaged; }
        }

        /// <summary>
        /// Unity event for when a hover starts.
        /// </summary>
        public MetaInteractionDataEvent HoverStart
        {
            get { return _hoverStart; }
        }

        /// <summary>
        /// Unity event for when a hover ends.
        /// </summary>
        public MetaInteractionDataEvent HoverEnd
        {
            get { return _hoverEnd; }
        }
    }
}