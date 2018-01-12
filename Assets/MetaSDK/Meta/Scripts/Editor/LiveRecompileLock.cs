using UnityEditor;
using UnityEngine;


/// <summary>
/// This script prevents Unity from hotswapping classes during runtime.
/// This is necessary because some Meta code cannot be hotswapped and will
/// instead cause Unity to crash.
/// </summary>
[InitializeOnLoad]
public class LiveRecompileLock : Editor {

    static LiveRecompileLock()
    {
        // Manual test: Make sure that the [InitializeOnLoad] attribute
        // is correctly calling the constructor.
        //Debug.Log("Running static constructor");
        UnityEditor.EditorApplication.playmodeStateChanged += ChangePlaymodeCallback;
    }

    private static void ChangePlaymodeCallback()
    {
        //Debug.Log("func running"); //manual test
        //This is only kicks off when you have exited play mode.
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorPrefs.SetBool("kAutoRefresh", false);
            // Manual test: Ensure this is only written once to console.
            // Test by stopping and starting the scene multiple times to
            // ensure that the delegate is not repeatedly being added.
            //Debug.Log("Locked assemblies in"); // manual test
        }
        else
        {
            UnityEditor.EditorPrefs.SetBool("kAutoRefresh", true);
            UnityEditor.AssetDatabase.Refresh();
            //Debug.Log("Unlocked assemblies."); //manual test
        }
    }

}
