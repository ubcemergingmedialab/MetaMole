using UnityEngine;

namespace Meta
{
    internal class InteractionObjectOutlineSettings : MonoBehaviour
    {
#pragma warning disable 0649 

        /// <summary>
        /// The color to be used for outlining the object when it is grabbed.
        /// </summary>
        public Color ObjectGrabbedColor = new Color(0.6274509803921569f, 0.8901960784313725f, 0.9568627450980393f);

        /// <summary>
        /// The color to be used for outlining the object when the hand is hovering over it.
        /// </summary>
        public Color ObjectHoverColor = new Color(0.19607843137254902f, 0.8392156862745098f, 0.9921568627450981f);

        /// <summary>
        /// The color to be used for outlining the object it is not interacting with a hand.
        /// </summary>
        public Color ObjectIdleColor;

#pragma warning restore 0649
    }
}
