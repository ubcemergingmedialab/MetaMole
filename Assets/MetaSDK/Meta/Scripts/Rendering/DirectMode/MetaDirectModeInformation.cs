using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Meta.DisplayMode.DirectMode
{
    /// <summary>
    /// Meta Direct Mode DLL wrapper
    /// </summary>
    public static class MetaDirectModeInformation
    {
        #region DLL External
        /// <summary>
        /// Get the number of connected displays in direct mode (x64 version)
        /// </summary>
        /// <returns>Number of connected displays</returns>
        [DllImport(MetaDirectModeInformationDLLReference.DLL64Name, EntryPoint = MetaDirectModeInformationDLLReference.GetConnectedDisplays)]
        private static extern int GetConnectedDisplays64();
        #endregion

        /// <summary>
        /// Gets the number of connected displays in direct mode.
        /// Return -1 if there was an error
        /// </summary>
        /// <returns>Number of connected displays in direct mode.</returns>
        public static int GetNumberOfConnectedDisplays()
        {
            int result = -1;
            try
            {
                result = GetConnectedDisplays64();
            }
            catch (Exception exception)
            {
                Debug.LogErrorFormat("Exception on Getting connected displays: {0}", exception.Message);
            }
            if (result < 0)
            {
                Debug.LogWarning("Error on Getting connected displays");
            }

            return result;
        }
    }
}
