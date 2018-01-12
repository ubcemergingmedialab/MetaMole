using System;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Events
{
    /// <summary>
    /// Unity event that passes a Vector3
    /// </summary>
    [Serializable]
    public class Vector3UnityEvent : UnityEvent<Vector3> { }
}