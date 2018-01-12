using System.Linq;
using UnityEngine;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Meta
{
    using System;
    using System.IO;

    internal static class MetaUtils
    {
        /// <summary>
        /// The URL of docs homepage, accessed from MetaWindow and MetaUpdaterUI menu items and buttons
        /// </summary>
        public static string metaDocsURL
        {
            get
            {

                if (MetaUtils.IsBeta())
                {
                    return "file:///%META_SDK2_BETA%/Docs/SDK Guide/index.html";
                }
                else
                {
                    return "file:///%META_SDK2%/Docs/SDK Guide/index.html";
                }
            }
        }

        /// <summary>
        /// The path to the Meta release version text file document.
        /// </summary>
        public static readonly string MetaVersionPath = @".\Assets\MetaSDK\Meta\SDK2Version.txt";


        private static string _sdkVersion = null;

        /// <summary>
        /// Get the version of the SDK that is written in the release version text file.
        /// </summary>
        /// <returns></returns>
        public static string SDKVersion
        {
            get
            {
                if (_sdkVersion == null)
                {
                    _sdkVersion = File.ReadAllLines(MetaVersionPath).First();
                }
                return _sdkVersion;
            }
        }

        /// <summary>
        /// Checks if the unitypackage is beta
        /// </summary>
        public static bool IsBeta()
        {
            if (Assembly.GetAssembly(MethodBase.GetCurrentMethod().DeclaringType).GetType("MetaBeta", false, true) != null
                && Assembly.GetAssembly(MethodBase.GetCurrentMethod().DeclaringType).GetType("MetaInternal", false, true) == null)
            {
                return true;
            }
            return false;
        }

        public static string GetPluginsFolderPath()
        {
            string applicationDataFolder, pluginsRelativePath;
            string platformFolder = Is64Bit() ? "x86_64" : "x86";
            if (Application.isEditor)
            {
                applicationDataFolder = Path.Combine(Application.dataPath, "MetaSDK");
                pluginsRelativePath = Path.Combine("Plugins", platformFolder);
            }
            else
            {
                applicationDataFolder = Application.dataPath;
                pluginsRelativePath = "Plugins";
            }
            return Path.Combine(applicationDataFolder, pluginsRelativePath).Replace("/", "\\");
        }

        /// <summary>
        /// Checks if we are in internal
        /// </summary>
        public static bool IsInternal()
        {
            if (Assembly.GetAssembly(MethodBase.GetCurrentMethod().DeclaringType).GetType("MetaInternal", false, true) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the Unity Editor is 64-bit
        /// </summary>
        public static bool Is64Bit()
        {
            return (System.IntPtr.Size == 8);
        }

        /// <summary>
        /// Opens a URL within the META_SDK directory, where %META_SDK% is replaced by the SDK path.
        /// </summary>
        /// <param name="url">The URL to be parsed.</param>
        public static void OpenURL(string url)
        {
            url = url.Replace("%META_SDK2%", System.Environment.GetEnvironmentVariable("META_SDK2"))
                .Replace("%META_SDK2_BETA%", System.Environment.GetEnvironmentVariable("META_SDK2_BETA"))
                .Replace("\\", "/");
            Application.OpenURL(url);
        }

        /// <summary> Float to vector 3.</summary>
        ///
        /// <param name="data">   The data.</param>
        /// <param name="vector"> The vector.</param>
        public static void FloatToVector3(float[] data, ref Vector3 vector)
        {
            vector.Set(data[0], data[1], data[2]);
        }

        /// <summary> Float to vector 3.</summary>
        ///
        /// <param name="data"> The data.</param>
        ///
        /// <returns> A Vector3.</returns>
        public static Vector3 FloatToVector3(float[] data)
        {
            return new Vector3(data[0], data[1], data[2]);
        }

        public static void FloatToVector2(float[] data, ref Vector2 vector)
        {
            vector.Set(data[0], data[1]);
        }


        public static Vector2 FloatToVector2(float[] data)
        {
            return new Vector2(data[0], data[1]);
        }

        public static bool CppBoolToCsBool(byte val)
        {
            return val > 0;
        }

        public static byte CsBoolToCppBool(bool val)
        {
            return val ? (byte)1 : (byte)0;
        }

        [DllImport("MetaVisionDLL", EntryPoint = "getCurrentMicroseconds")]
        public static extern long GetCurrentMicroseconds();

        public static string GetCurrentSystemTime(string format = "{0:HH:mm:ss.ffff}")
        {
            return string.Format(format, System.DateTime.Now);
        }

    }

}
