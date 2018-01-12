using System;
using Meta.HandInput;
using UnityEngine.Events;

namespace Meta
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SwipeEvent : UnityEvent<HandFeature, SwipeDirections>
    {
    }
}