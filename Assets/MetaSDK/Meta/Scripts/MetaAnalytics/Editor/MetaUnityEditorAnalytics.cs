using System;
using System.Diagnostics;
using Meta.MetaAnalytics;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Meta
{
    [InitializeOnLoad]
    public class MetaUnityEditorAnalytics
    {

        private static string SettingName = "MetaAnalyticsUnityUptime";

        /// <summary>
        /// Amount to add to the recorded value to prevent multiple runs 
        /// which occur in a short amount of time from one another from 
        /// generating another 'first run' event
        /// </summary>
        private static int DebounceAmount = 100;

        static MetaUnityEditorAnalytics()
        {
            OnScriptLoaded();

        }

        /// <summary>
        /// This will be run every time that this script is loaded.
        /// The script is loaded when Unity refreshes its resources
        /// i.e when code is recompiled, when a scene starts, when
        /// Unity opens.
        /// 
        /// </summary>
        private static void OnScriptLoaded()
        {
            var time = DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime();
            int msRunning = (int)time.TotalMilliseconds;
            int lastRecordedMsRunning = PlayerPrefs.GetInt(SettingName, int.MaxValue);

            if (msRunning < lastRecordedMsRunning)
            {
                //The first time that the script is loaded into Unity.
                OnUnityOpenedAnalytics();
            }

            PlayerPrefs.SetInt(SettingName, msRunning - DebounceAmount);
            PlayerPrefs.Save();
        }

        private static void OnUnityOpenedAnalytics()
        {
            IMetaAnalytics _analytics = new MetaAnalytics.MetaAnalytics();

            JObject o = new JObject();
            o["scene_unityVersion"] = Application.unityVersion;
            _analytics.SendAnalytics("scene_unityVersion", o.ToString());
        }

    }
}