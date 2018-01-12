using Meta.HandInput;
using UnityEngine.EventSystems;

namespace Meta
{
    /// <summary>
    /// Encapsulates either mouse or hand input. All input related events should pass this around
    /// rather than PointerEventData or HandFeature directly so that mouse and hand input can share
    /// the same events.
    /// </summary>
    public class MetaInteractionData
    {
        public PointerEventData PointerEventData { get; private set; }

        public HandFeature MotionHandFeature { get; private set; }

        public MetaInteractionData(PointerEventData pointerEventData, HandFeature motionHandFeature)
        {
            PointerEventData = pointerEventData;
            MotionHandFeature = motionHandFeature;
        }
    }
}
