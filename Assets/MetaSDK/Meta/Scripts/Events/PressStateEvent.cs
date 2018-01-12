using System;
using UnityEngine.Events;

namespace Meta.UI
{
    /// <summary>
    /// Unity event that passes information about the state of a UIEventTrigger
    /// </summary>
    [Serializable]
    public class PressStateEvent : UnityEvent<PressState> { }
}