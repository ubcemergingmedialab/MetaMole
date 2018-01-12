using System.IO;
using System;
using UnityEngine;

namespace Meta
{

    ///// <summary>
    ///// MetaPlugin adds dll path to the programs path.
    ///// </summary>
    ///// <remarks>
    ///// It adds Assets/Plugins/x86 to the path in the editor, and ApplicationDataFolder\Plugins to the build path.
    ///// *NOTE*The static constructor for this class needs to be loaded before the assembly tris to load the dlls. therfore, changing the MetaWorld script exxecution order will create problems for builds.*NOTE*
    ///// </remarks>
    internal class MetaPathVariables
    {
        public void AddPathVariables()
        {
            string metaCoreEnvironmentVar = "META_CORE";

            // Add the unity plugins folder to the path.
            string pluginsPath = Application.dataPath + Path.DirectorySeparatorChar + (Application.isEditor ? "MetaSDK" + Path.DirectorySeparatorChar : "") + "Plugins";
            pluginsPath = pluginsPath.Replace("/", "\\");

            // Add meta core path.  IMPORTANT that this added AFTER the plugins path.
            string coreDllsPath = Environment.GetEnvironmentVariable(metaCoreEnvironmentVar);

            AddPathVariable(pluginsPath);
            AddPathVariable(coreDllsPath);
        }

        /// <summary>
        /// Add From lowest precedence to highest precedence.
        /// </summary>
        /// <param name="dllPath">directory to add to the path.</param>
        private void AddPathVariable(string dllPath)
        {
            String currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);

            // Check that we haven't added it already.
            if (currentPath.Contains(dllPath))
            {
                return;
            }

            // Add the dllpath to the 
            Environment.SetEnvironmentVariable("PATH", dllPath + Path.PathSeparator + currentPath, EnvironmentVariableTarget.Process);
        }

    }
}
