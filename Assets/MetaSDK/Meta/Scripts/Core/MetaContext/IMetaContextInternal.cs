using System;

namespace Meta
{
    /// <summary>
    /// Handles setup and references to modules for access to different components of the Meta SDK
    /// </summary>
    internal interface IMetaContextInternal : IMetaContext
    {
        /// <summary>
        /// Returns the effective scale factor applied to Meta objects, based on the default scale of 1m to 100 Unity units.
        /// </summary>
        float MeterToUnityScale
        {
            get;
            set;
        }

        /// <summary>
        /// Add a module to MetaContext.
        /// </summary>
        /// <typeparam name="T">Type of the module to be added.</typeparam>
        /// <param name="module">Actual object to add.</param>
        /// <returns>True if added, false otherwise</returns>
        bool Add<T>(T module);

        /// <summary>
        /// Removes a module from the MetaContext.
        /// </summary>
        /// <typeparam name="T">Type of the module to be removed.</typeparam>
        /// <returns>True if removed, false if does not exist</returns>
        bool Remove<T>();

        /// <summary>
        /// Removes a module from the MetaContext.
        /// </summary>
        /// <typeparam name="T">Type of the module to be removed.</typeparam>
        /// <param name="module">Actual object to remove.</param>
        /// <returns>True if removed, false if does not exist</returns>
        bool Remove<T>(T module);

        /// <summary>
        /// Removes a module from the MetaContext by type.
        /// </summary>
        /// <param name="type">Type to be removed</param>
        /// <returns>True if removed, false otherwise</returns>
        bool Remove(Type type);
    }
}
