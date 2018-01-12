using UnityEngine;
using UnityEngine.Events;
using Meta.HandInput;

namespace Meta
{
    public class GrabEvent : UnityEvent<Hand, GameObject> { }
}
