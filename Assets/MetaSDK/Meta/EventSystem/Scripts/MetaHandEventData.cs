using UnityEngine;
using Meta.HandInput;
using UnityEngine.EventSystems;

namespace Meta
{
    /// <summary>
    /// Adds hand info to the Unity pointer data
    /// </summary>
    public class MetaHandEventData : PointerEventData
    {
        public bool IsCanceled { get; set; }

        /// <summary>
        /// HandFeature which spawned this event.
        /// </summary>
        public HandFeature HandFeature { get; set; }

        /// <summary>
        /// Distance to down press.
        /// </summary>
        public float DownDistance { get; set; }

        /// <summary>
        /// Distance to front of UIEventTrigger.
        /// </summary>
        public float FrontDistance { get; set; }

        /// <summary>
        /// Position of HandFeature projected onot down press plane.
        /// </summary>
        public Vector3 ProjectedPanelPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventSystem"></param>
        public MetaHandEventData(EventSystem eventSystem) : base(eventSystem)
        {
            eligibleForClick = false;

            pointerId = -1;
            position = Vector2.zero; // Current position of the mouse or touch event
            delta = Vector2.zero; // Delta since last update
            pressPosition = Vector2.zero; // Delta since the event started being tracked
            clickTime = 0.0f; // The last time a click event was sent out (used for double-clicks)
            clickCount = 0; // Number of clicks in a row. 2 for a double-click for example.

            scrollDelta = Vector2.zero;
            useDragThreshold = true;
            dragging = false;
            button = InputButton.Left;
            IsCanceled = false;
        }

        public override void Reset()
        {
            base.Reset();
            HandFeature = null;
            IsCanceled = false;
        }
    }
}