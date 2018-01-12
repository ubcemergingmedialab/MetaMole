//-----------------------------------------------------------
// Copyright (c) 2017 Meta Company. All rights reserved.
//-----------------------------------------------------------
using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using UnityEditor;

namespace Meta.DirectMode
{
    /// <summary>
    /// Bride for comunication between Unity and MetaDirectModeUtil
    /// </summary>
    internal static class MetaDirectModeBridge
    {
        private const string META2_DIRECT_MODE_KEY = "META_2_DIRECTMODE";
        private const string META2_DIRECT_UTIL_RELATIVE_PATH = "bin/MetaDirectModeUtil.exe";
        private const string META2_DIRECT_WHITE_REG_RELATIVE_PATH = "whitelist.reg";
        private const string REGISTRY_PATH = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\nvlddmkm";
        private const string REGISTRY_ATTRIBUTE = "1641970VRWhiteList";

        /// <summary>
        /// Call Direct Mode utility with the given argument
        /// </summary>
        /// <param name="argument">Argument for Direct Mode</param>
        public static void CallMetaDirectMode(string argument)
        {
            // check for registry key
            if (pathToDirectModeTool.StartsWith("/"))
            {
                EditorUtility.DisplayDialog("DirectMode Utilities not found", "To be able to use DirectMode, please restart your computer.", "Got it");
                return;
            }
            if (!RegistryKeyExists())
            {
                // warning
                EditorUtility.DisplayDialog("DirectMode registry entry not found", "DirectMode is not yet enabled on your computer. You might need to restart your machine.", "Got it");
                return;
            }

            // call the batch file to turn direct mode on. show the result in the console output.
            var proc = new Process();
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = pathToDirectModeTool;
            startInfo.Arguments = argument;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            proc.StartInfo = startInfo;

            proc.Start();

            // show both errors and normal output
            UnityEngine.Debug.Log(proc.StandardOutput.ReadToEnd());
            var error = proc.StandardError.ReadToEnd();
            if (error != "")
            {
                UnityEngine.Debug.LogError(error);
                EditorUtility.DisplayDialog("Could not enable DirectMode", "Please make sure that SDK2 is installed correctly, you have the newest NVidia graphics drivers, and you restarted your machine after installing.", "Got it");
            }

            proc.WaitForExit();
        }

        private static bool RegistryKeyExists()
        {
            // HACK does not contain the real value, but at least not null if the value is present
            var value = Registry.GetValue(REGISTRY_PATH, REGISTRY_ATTRIBUTE, "");
            if (value == null)
                return false;
            return !string.IsNullOrEmpty(value.ToString());
        }

        private static string pathToDirectModeTool
        {
            get
            {
                var path = Environment.GetEnvironmentVariable(META2_DIRECT_MODE_KEY);
                return Path.Combine(path, META2_DIRECT_UTIL_RELATIVE_PATH);
            }
        }

        private static string pathToWhitelist
        {
            get
            {
                var path = Environment.GetEnvironmentVariable(META2_DIRECT_MODE_KEY);
                return Path.Combine(path, META2_DIRECT_WHITE_REG_RELATIVE_PATH); ;
            }
        }
    }
}
