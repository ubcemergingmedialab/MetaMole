using System;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Handles the access to a prefab, in order to create and destroy one instance of it.
    /// </summary>
    /// <typeparam name="T">Prefab type.</typeparam>
    public class MonoBehaviourProxy<T> : IMonoBehaviourProxy<T> where T : UnityEngine.Object
    {
        private readonly T _prefab;
        private PrefabInstantiator _instantiator;

        private PrefabInstantiator Instantiator
        {
            get
            {
                if (_instantiator == null)
                {
                    _instantiator = new GameObject("instantiator").AddComponent<PrefabInstantiator>();
                    #if UNITY_EDITOR
                    {
                        _instantiator.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
                    }
                    #endif
                }
                return _instantiator;
            }
        }

        /// <summary>
        /// Gets the current instantiated object.
        /// </summary>
        public T CurrentObject { get; private set; }

        /// <summary>
        /// Creates an instance of <see cref="CurrentObject"/> class.
        /// </summary>
        public MonoBehaviourProxy(T prefab)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException("prefab");
            }
            _prefab = prefab;
        }

        /// <summary>
        /// Creates a new instance of the prefab.
        /// </summary>
        public T Create()
        {
            if (CurrentObject != null)
            {
                throw new Exception("You can only instantiate one object");
            }

            CurrentObject = Instantiator.InstantiateObject(_prefab);
            return CurrentObject;
        }

        /// <summary>
        /// Destroys the current instantiated object.
        /// </summary>
        public void Destroy()
        {
            if (_instantiator != null)
            {
                Component currentComponent = CurrentObject as Component;
                if (currentComponent != null)
                {
                    _instantiator.DestroyGameObject(currentComponent.gameObject);
                }
                else
                {
                    _instantiator.DestroyGameObject(CurrentObject);
                }

                _instantiator.Destroy();

                CurrentObject = null;
                _instantiator = null;
            }
            else
            {
                throw new Exception("You can only Destroy if you created the object before.");
            }
        }
    }
}