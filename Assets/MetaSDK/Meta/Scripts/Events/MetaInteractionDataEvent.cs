using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Meta
{
    [Serializable]
    public class MetaInteractionDataEvent : UnityEvent<MetaInteractionData> { }
}