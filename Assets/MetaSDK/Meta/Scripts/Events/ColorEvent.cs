using System;
using UnityEngine;
using UnityEngine.Events;

namespace Meta
{
    /// <summary>
    /// Unity event that passes a Color
    /// </summary>
    [Serializable]
    public class ColorEvent : UnityEvent<Color> {}
}