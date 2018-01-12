using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Checks if slam map can be localized.
    /// </summary>
    public interface ISlamChecker
    {
        /// <summary>
        /// Tries to localize an slam map.
        /// </summary>
        /// <param name="mapPath">The path of the slam map file.</param>
        /// <param name="doneAction">Action called with the localization response.</param>
        void TryLocalizeMap(string mapPath, Action<bool> doneAction);

        /// <summary>
        /// Stops the slam checking process.
        /// </summary>
        void Stop();
    }
}
