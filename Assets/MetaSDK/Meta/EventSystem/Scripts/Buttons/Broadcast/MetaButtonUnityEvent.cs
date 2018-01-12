using System;
using UnityEngine.Events;

namespace Meta.Buttons
{
    /// <summary>
    /// Unity Event extended class to handle IMetaButton events
    /// </summary>
    [Serializable]
    public class MetaButtonUnityEvent : UnityEvent<IMetaButton>
    {
    }
}
