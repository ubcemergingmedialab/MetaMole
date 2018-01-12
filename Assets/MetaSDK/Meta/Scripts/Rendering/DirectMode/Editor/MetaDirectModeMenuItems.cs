//-----------------------------------------------------------
// Copyright (c) 2017 Meta Company. All rights reserved.
//-----------------------------------------------------------
using UnityEditor;

namespace Meta.DirectMode
{
    /// <summary>
    /// Direct Mode for Meta2 Unity Meny Items
    /// </summary>
    public class MetaDirectModeMenuItems
    {
        /// <summary>
        /// Enables Direct Mode in Editor
        /// </summary>
        [MenuItem("Meta 2/DirectMode/Enable DirectMode on Meta2")]
        private static void EnableDirectMode()
        {
            if (EditorUtility.DisplayDialog("Are you sure?", "Unity might crash. Please save before doing this. Continue?", "Yes", "No"))
            {
                // call batch file to enable it
                MetaDirectModeBridge.CallMetaDirectMode("-enable");
            }
        }

        /// <summary>
        /// Disables Direct Mode in Editor
        /// </summary>
        [MenuItem("Meta 2/DirectMode/Disable DirectMode on Meta2")]
        private static void DisableDirectMode()
        {
            // call the batch file to turn direct mode on. show the result in the console output.
            MetaDirectModeBridge.CallMetaDirectMode("-disable");
        }
    }
}
