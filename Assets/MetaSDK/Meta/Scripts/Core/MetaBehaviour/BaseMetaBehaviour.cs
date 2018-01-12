using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Base abstract class to access the MetaContext either Internal or Public
    /// </summary>
    /// <typeparam name="T">IMetaContext Type</typeparam>
    public abstract class BaseMetaBehaviour<T> : MonoBehaviour
        where T : IMetaContext
    {
        /// <summary>
        /// Context member
        /// </summary>
        private T _metaContext;

        /// <summary>
        /// Contains references to modules used to provide Meta functionality in the scene.
        /// </summary>
        protected T metaContext
        {
            get
            {
                if (_metaContext == null)
                {
                    var contextBridge = FindObjectOfType<BaseMetaContextBridge>();
                    if (contextBridge != null)
                    {
                        _metaContext = contextBridge.GetContext<T>();
                    }
                    else
                    {
                        Debug.LogError("Error: The Meta2 prefab is missing from the scene");
                    }
                }
                return _metaContext;
            }
        }
    }
}
