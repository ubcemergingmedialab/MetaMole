using UnityEditor;

namespace Meta.Buttons
{
    /// <summary>
    /// Handles the menu item for the Buttons Emulation
    /// </summary>
    public static class MetaButtonEventMenuItem
    {

        /// <summary>
        /// Create a new Editor window for Button Events
        /// </summary>
        [MenuItem("Meta 2/Emulation/Buttons")]
        public static void Init()
        {
            var window = EditorWindow.GetWindow<EditorMetaButtonEventWindow>();
            window.titleContent.text = "Meta2 Buttons";
            window.Show();
        }
    }
}
