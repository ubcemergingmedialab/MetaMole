using System;
using  UnityEngine;
using UnityEngine.Events;

namespace Meta.UI
{
    /// <summary>
    /// Unity event that passes a collider
    /// </summary>
    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> { }
}