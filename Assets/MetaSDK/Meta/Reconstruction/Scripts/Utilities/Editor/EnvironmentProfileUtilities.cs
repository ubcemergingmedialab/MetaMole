using UnityEngine;
using UnityEditor;
using System.IO;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Handles environment profiles in editor mode.
    /// </summary>
    [InitializeOnLoad]
    public class EnvironmentProfileUtilities
    {
        [MenuItem("Meta 2/Environment Profiles/Delete All")]
        private static void DeleteAllProfiles()
        {
            if(!Application.isPlaying)
            {
                if(EditorUtility.DisplayDialog("Deleting all Environments Profiles", "Are you you want to proceed?", "Delete", "Cancel"))
                {
                    string envPath = string.Format("{0}\\{1}\\", System.Environment.GetEnvironmentVariable("meta_root"), EnvironmentConstants.EnvironmentFolderName);
                    if (Directory.Exists(envPath))
                    {
                        Directory.Delete(envPath, true);
                    }
                }
            }
            else
            {
                Debug.LogWarning("You cannot delete environments in play mode.");
            }
        }
    }
}