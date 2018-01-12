using System;

namespace Meta
{
    /// <summary>
    /// Read only interface for MetaContext.
    /// Handles setup and references to modules for access to different components of the Meta SDK
    /// </summary>
    public interface IMetaContext
    {
        /// <summary>
        /// Returns a list of all the modules currently available in MetaContext.
        /// </summary>
        /// <returns>A list of types of the modules.</returns>
        Type[] GetModuleList();

        /// <summary>
        /// Returns True if MetaContext contains a module of Type T.
        /// </summary>
        /// <typeparam name="T">Type to check for.</typeparam>
        /// <returns>True if a module of the type exists.</returns>
        bool ContainsModule<T>();

        /// <summary>
        /// Get the module of type T. If no such module exists, returns null.
        /// </summary>
        /// <typeparam name="T">Type of module to return.</typeparam>
        /// <returns>Module of type T if it exists, otherwise null.</returns>
        T Get<T>();

        /// <summary>
        /// The IUserSettings interface is not exposed, developers may have
        /// access to part of it- inherited from IUserSettingsDeveloper.
        /// </summary>
        /// <returns>IUserSettingsDeveloper</returns>
        IUserSettingsDeveloper GetUserSettings();
    }
}
