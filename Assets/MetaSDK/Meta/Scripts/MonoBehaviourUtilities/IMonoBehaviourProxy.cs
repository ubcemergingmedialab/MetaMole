using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Handles the access to a prefab, in order to create and destroy one instance of it.
    /// </summary>
    public interface IMonoBehaviourProxy<T> where T : Object
    {
        /// <summary>
        /// Creates a new instance of the prefab
        /// </summary>
        T Create();

        /// <summary>
        /// Destroys the current instantiated object
        /// </summary>
        void Destroy();
    }
}
