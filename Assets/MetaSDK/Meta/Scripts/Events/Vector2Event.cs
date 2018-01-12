using System;
using UnityEngine;
using UnityEngine.Events;

namespace Meta
{
    /// <summary>
    /// Unity event that passes a Vector2
    /// </summary>
    [Serializable]
    public class Vector2Event : UnityEvent<Vector2> {}
}