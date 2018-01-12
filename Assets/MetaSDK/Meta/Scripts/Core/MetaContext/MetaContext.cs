using System;
using System.Collections.Generic;
using System.Linq;

namespace Meta
{
    /// <summary>
    /// Handles setup and references to modules for access to different components of the Meta SDK
    /// </summary>
    internal class MetaContext : IMetaContextInternal
    {
        /// <summary>
		/// Dictionary used to keep all the various modules accessible via MetaContext.
        /// </summary>
        private Dictionary<Type, Object> modules = new Dictionary<Type, object>();

        /// <summary>
        /// Returns a list of all the modules currently available in MetaContext.
        /// </summary>
        /// <returns>A list of types of the modules.</returns>
        public Type[] GetModuleList()
        {
            return modules.Keys.ToArray();
        }

        /// <summary>
        /// Returns True if MetaContext contains a module of Type T.
        /// </summary>
        /// <typeparam name="T">Type to check for.</typeparam>
        /// <returns>True if a module of the type exists.</returns>
        public bool ContainsModule<T>()
        {
            return modules.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Get the module of type T. If no such module exists, returns null.
        /// </summary>
        /// <typeparam name="T">Type of module to return.</typeparam>
        /// <returns>Module of type T if it exists, otherwise null.</returns>
        public T Get<T>()
        {
            if (!modules.ContainsKey(typeof(T)))
            {
                var message = string.Format("No module of type {0} exists. Please check using MetaContext.ContainsModule<{0}>() before using MetaContext.Get<{0}>()", typeof(T));
                throw new KeyNotFoundException(message);
            }
            return (T)modules[typeof(T)];
        }

        /// <summary>
        /// The IUserSettings interface is not exposed, developers may have
        /// access to part of it- inherited from IUserSettingsDeveloper.
        /// </summary>
        /// <returns></returns>
        public IUserSettingsDeveloper GetUserSettings()
        {
            return Get<IUserSettings>();
        }

        /// <summary>
        /// Add a module to MetaContext.
        /// </summary>
        /// <typeparam name="T">Type of the module to be added.</typeparam>
        /// <param name="module">Actual object to add.</param>
        /// <returns>True if added, false otherwise</returns>
        public bool Add<T>(T module)
        {
            var type = typeof(T);
            if (modules.ContainsKey(type))
            {
                UnityEngine.Debug.LogWarningFormat("Overriding Module {0}", type);
                modules[type] = module;
            }
            else
            {
                modules.Add(typeof(T), module);
            }

            return true;
        }

        /// <summary>
        /// Removes a module from the MetaContext.
        /// </summary>
        /// <typeparam name="T">Type of the module to be removed.</typeparam>
        /// <returns>True if removed, false if does not exist</returns>
        public bool Remove<T>()
        {
            return Remove(typeof(T));
        }

        /// <summary>
        /// Removes a module from the MetaContext.
        /// </summary>
        /// <typeparam name="T">Type of the module to be removed.</typeparam>
        /// <param name="module">Actual object to remove.</param>
        /// <returns>True if removed, false if does not exist</returns>
        public bool Remove<T>(T module)
        {
            if (module == null)
                return false;

            return Remove(module.GetType());
        }

        /// <summary>
        /// Removes a module from the MetaContext by type.
        /// </summary>
        /// <param name="type">Type to be removed</param>
        /// <returns>True if removed, false otherwise</returns>
        public bool Remove(Type type)
        {
            if (!modules.ContainsKey(type))
                return false;

            modules.Remove(type);
            return true;
        }

        /// <summary>
        /// Returns the effective scale factor applied to Meta objects, based on the default scale of 1m to 100 Unity units.
        /// </summary>
        public float MeterToUnityScale
        {
            get;
            set;
        }
    }
}