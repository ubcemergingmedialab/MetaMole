using UnityEditor;

namespace Meta.Buttons
{
    /// <summary>
    /// Custom Inspector for MetaButtonGameObjectEventBroadcaster
    /// </summary>
    [CustomEditor(typeof(MetaButtonEventBroadcaster))]
    public class MetaButtonEventBroadcasterCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.HelpBox("This broadcaster will retransmit the button events. Other scripts needs to manually subscribe to the main event.", MessageType.Info);
        }
    }
}
