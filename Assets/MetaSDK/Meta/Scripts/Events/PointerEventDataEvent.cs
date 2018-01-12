using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Meta
{
    /// <summary>
    /// Unity event that passes information about the pointer
    /// </summary>
    [Serializable]
    public class PointerEventDataEvent : UnityEvent<PointerEventData> { }
}