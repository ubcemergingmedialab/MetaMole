using UnityEditor;

namespace Meta.Buttons
{
    /// <summary>
    /// Custom Inspector for MetaButtonGameObjectEventBroadcaster
    /// </summary>
    [CustomEditor(typeof(MetaButtonGameObjectEventBroadcaster))]
    public class MetaButtonGameObjectEventBroadcasterCustomInspector : Editor
    {
        private MetaButtonGameObjectEventBroadcaster _component;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (_component == null)
            {
                _component = target as MetaButtonGameObjectEventBroadcaster;
            }

            string message = "";
            MessageType messageType = MessageType.Info;
            switch (_component.BroadcastType)
            {
                case ButtonBroadcastType.None:
                    message = "Broadcast Disabled";
                    messageType = MessageType.Warning;
                    break;
                case ButtonBroadcastType.Children:
                    message = "Broadcast to all the children of the current GameObject";
                    messageType = MessageType.Info;
                    break;
                case ButtonBroadcastType.Parents:
                    message = "Broadcast to all the parents of the current GameObject";
                    messageType = MessageType.Info;
                    break;
                case ButtonBroadcastType.Scene:
                    message = "This Broadcaster will call the OnMetaButtonEvent method of all the implementors of Meta.Buttons.BaseMetaButtonInteractionObject in the scene.";
                    messageType = MessageType.Warning;
                    break;
            }

            EditorGUILayout.HelpBox(message, messageType);
        }
    }
}
