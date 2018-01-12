using System;
using UnityEngine;
using UnityEngine.Events;

namespace Meta
{
    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> {}
}