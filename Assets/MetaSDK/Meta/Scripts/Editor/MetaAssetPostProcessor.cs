using UnityEngine;
using UnityEditor;

/// <summary>
/// Setup important Unity configurations to use the Meta headset after importing the MetaSDK.unitypackage
/// </summary>
class MetaAssetPostProcessor : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (PlayerPrefs.GetInt("FirstImport") == 0)
        {
            AudioSettingsSetup();
            //BuildSettingsSetup();
            PlayerSettingsSetup();

            PlayerPrefs.SetInt("FirstImport", 1);
        }
    }

    /// <summary>
    /// Change default audio settings
    /// </summary>
    private static void AudioSettingsSetup()
    {
        // Change default speaker mode from Stereo to Quad
        AudioConfiguration audioConfiguration = AudioSettings.GetConfiguration();
        audioConfiguration.speakerMode = AudioSpeakerMode.Quad;
        AudioSettings.Reset(audioConfiguration);
    }

    /// <summary>
    /// Change default build settings
    /// </summary>
    private static void BuildSettingsSetup()
    {
        // Change default x86 architecture to x86_64
        EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows64;
    }

    /// <summary>
    /// Change default player settings
    /// </summary>
    private static void PlayerSettingsSetup()
    {
        // Change default API compatibility from .NET 2.0 Subset to .NET 2.0
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_2_0);
        // Enable run in background by default to avoid app freezing after windows refocus
        Application.runInBackground = true;
    }
}