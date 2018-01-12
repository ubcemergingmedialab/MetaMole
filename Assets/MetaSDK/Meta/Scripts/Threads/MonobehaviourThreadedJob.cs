using UnityEngine;
using UnityEngine.Events;

namespace Meta
{
    /// <summary>
    /// Used by <see cref="UnityThreadedJob"/> in order to acces to MonoBehaviour functions.
    /// </summary>
    public class MonoBehaviourThreadedJob : MonoBehaviour
    {
        private UnityEvent _disabled = new UnityEvent();
        private bool _hasToDestroy;

        /// <summary>
        /// Occurs when the object is disabled.
        /// </summary>
        public UnityEvent Disabled
        {
            get { return _disabled; }
        }

        private void Update()
        {
            if (_hasToDestroy)
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

        private void OnDisable()
        {
            _disabled.Invoke();
        }

        /// <summary>
        /// Mark this order in order to be destroyed.
        /// </summary>
        public void MarkToDestroy()
        {
            _hasToDestroy = true;
        }
    }
}
