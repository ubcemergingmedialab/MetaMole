using UnityEngine;
using UnityEngine.EventSystems;

namespace Meta.Events
{
    public static class EventSystemUtility
    {
        /// <summary>
        /// Get the correct camera for a pointer
        /// </summary>
        /// <param name="pointerEventData"></param>
        /// <returns></returns>
        public static Camera GetMetaHandEventDataPressEventCamera(PointerEventData pointerEventData)
        {
            MetaHandEventData data = pointerEventData as MetaHandEventData;
            return data == null ? pointerEventData.pressEventCamera : data.pressEventCamera; ;
        }
    }
}