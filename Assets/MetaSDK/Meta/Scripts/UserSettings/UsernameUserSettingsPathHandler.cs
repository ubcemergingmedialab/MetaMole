using UnityEngine;
using System;
using System.IO;

namespace Meta
{
    /// <summary>
    /// The UserSettingsPathHandler which uses the username of the user to determine user settings paths
    /// </summary>
    public class UsernameUserSettingsPathHandler : IUserSettingsPathHandler
    {
        /// <summary>
        /// The username
        /// </summary>
        private string _username;

        public string Username
        {
            get
            {
                if (_username == null)
                {
                    return "default";
                }
                return _username;
            }
        }

        public string MetaSettingsFilePath
        {
            get { return UserSettingsPath + UserSettingsName; }
        }

        public string DeveloperSettingFilePath
        {
            get
            {
                return AppSettingsPath + MetaSettingsName;
            }
        }

        public string LocalSettingsPath
        {
            get
            {
                return string.Format(@"{0}\UserSettings\", Environment.GetEnvironmentVariable("META_ROOT"));
            }
        }

        public string PathDivider
        {
            get
            {
                return "/";
            }
        }

        public string MetaSettingsName
        {
            get { return Username + ".json"; }
        }

        public string AppSettingsPath
        {
            get
            {
                return string.Format("{0}/Apps/{2}{1}{3}{1}",LocalSettingsPath, PathDivider, Application.companyName, Application.productName  );
            }
        }

        public string UserSettingsPath
        {
            get { return LocalSettingsPath + "/Users/"; }
        }

        public string UserSettingsName
        {
            get
            {
                return Username + ".json";
            }
        }

        /// <summary>
        /// Create a path handler with a username
        /// </summary>
        /// <param name="name"></param>
        public UsernameUserSettingsPathHandler(string username)
        {
            _username = username;
            if (!Directory.Exists(LocalSettingsPath))
            {
                Directory.CreateDirectory(LocalSettingsPath);
            }
        }
    }
}
