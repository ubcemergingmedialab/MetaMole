using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Base abstract class for the MetaContextBridge
    /// </summary>
    public abstract class BaseMetaContextBridge : MonoBehaviour
    {
        /// <summary>
        /// Get the Meta Context by Type.
        /// This is useful when asking for Internal or Public interfaces
        /// </summary>
        /// <typeparam name="T">Type of IMetaContext</typeparam>
        /// <returns>Interface of MetaContext</returns>
        public abstract T GetContext<T>() where T : IMetaContext;

        /// <summary>
        /// Get the current Meta Context for internal purposes
        /// </summary>
        internal abstract IMetaContextInternal CurrentContextInternal
        {
            get;
        }

        /// <summary>
        /// Get the current Meta Context
        /// </summary>
        public abstract IMetaContext CurrentContext
        {
            get;
        }
    }
}
