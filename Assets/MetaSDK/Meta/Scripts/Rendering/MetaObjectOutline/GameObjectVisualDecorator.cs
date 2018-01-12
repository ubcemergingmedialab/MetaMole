using UnityEngine;
using System.Collections.Generic;

namespace Meta
{
    /// <summary>
    /// A decorator which has a visual effect on the GameObject on which it is placed.
    /// </summary>
    internal abstract class GameObjectVisualDecorator : MonoBehaviour
    {
        internal abstract List<GameObject> GetObjectsToDecorate();

    }
}
