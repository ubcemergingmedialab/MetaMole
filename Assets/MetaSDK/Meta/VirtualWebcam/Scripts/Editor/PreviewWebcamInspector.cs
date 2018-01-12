using UnityEditor;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// The inspector for the PreviewWebcam script. This will assign to the TargetDisplay property
    /// and as a result will perform a broadcast to all the listeners.
    /// </summary>
    [CustomEditor(typeof(PreviewWebcam))]
    public class PreviewWebcamInspector : Editor
    {
        
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                SerializedProperty targetDisplayPropertyAfterChange = serializedObject.FindProperty("_targetDisplay");
                if (Application.isPlaying)
                {
                    PreviewWebcam pw = target as PreviewWebcam;
                    //Must obtain value as int and cast to WebcamMirrorModes instead of getting value as WebcamMirrorModes.
                    //Getting the value as WebcamMirrorModes will get one enum value higher than the actual.
                    pw.TargetDisplay = (WebcamMirrorModes)targetDisplayPropertyAfterChange.Value<int>();
                }
            }
        }
    }
}
