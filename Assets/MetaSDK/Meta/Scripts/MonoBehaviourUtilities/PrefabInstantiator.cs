using System;
using UnityEngine;

namespace Meta
{
    using Object = UnityEngine.Object;

    /// <summary>
    /// Handles creation and destruction of a prefab.
    /// </summary>
    public class PrefabInstantiator : MonoBehaviour
    {
        /// <summary>
        /// Instantiates an object from a prefab.
        /// </summary>
        /// <param name="prefab">The prefab.</param>
        /// <returns>The new object.</returns>
        public T InstantiateObject<T>(T prefab) where T : UnityEngine.Object
        {
            if (prefab == null)
            {
                throw new ArgumentNullException("prefab");
            }

            return Object.Instantiate(prefab);
        }

        /// <summary>
        /// Destroys the game object go.
        /// </summary>
        /// <param name="go">The game object to be destroyed.</param>
        public void DestroyGameObject(UnityEngine.Object go)
        {
            if (go == null)
            {
                throw new ArgumentNullException("go");
            }

            #if UNITY_EDITOR
            {
                DestroyImmediate(go);
            }
            #else
            {
                Destroy(go);
            }
            #endif
        }

        /// <summary>
        /// Destroys this object.
        /// </summary>
        public void Destroy()
        {
            #if UNITY_EDITOR
            {
                DestroyImmediate(gameObject);
            }
            #else
            {
                Destroy(gameObject);
            }
            #endif
        }
    }
}
