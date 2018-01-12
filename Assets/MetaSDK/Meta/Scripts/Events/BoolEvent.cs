using System;
using UnityEngine.Events;

namespace Meta
{
    /// <summary>
    /// Unity event that passes a Boolean value
    /// </summary>
    [Serializable]
    public class BoolEvent : UnityEvent<bool> {}
}