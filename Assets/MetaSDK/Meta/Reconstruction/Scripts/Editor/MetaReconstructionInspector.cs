using UnityEditor;

namespace Meta.Reconstruction
{
    [CustomEditor(typeof(MetaReconstruction))]
    public class MetaReconstructionInspector : Editor
    {
        private SerializedProperty _reconstructionStartedProperty;
        private SerializedProperty _reconstructionPausedProperty;
        private SerializedProperty _reconstructionResumedProperty;
        private SerializedProperty _reconstructionResetProperty;
        private SerializedProperty _reconstructionSavedProperty;
        private SerializedProperty _reconstructionLoadedProperty;
        private SerializedProperty _scanningMaterialProperty;
        private SerializedProperty _occlusionMaterialProperty;
        private bool _showEvents;

        private void OnEnable()
        {
            _reconstructionStartedProperty = serializedObject.FindProperty("_reconstructionStarted");
            _reconstructionPausedProperty = serializedObject.FindProperty("_reconstructionPaused");
            _reconstructionResumedProperty = serializedObject.FindProperty("_reconstructionResumed");
            _reconstructionResetProperty = serializedObject.FindProperty("_reconstructionReset");
            _reconstructionSavedProperty = serializedObject.FindProperty("_reconstructionSaved");
            _reconstructionLoadedProperty = serializedObject.FindProperty("_reconstructionLoaded");
            _scanningMaterialProperty = serializedObject.FindProperty("_scanningMaterial");
            _occlusionMaterialProperty = serializedObject.FindProperty("_occlusionMaterial");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This functionality is experimental and you might encounter issues while using it!", MessageType.Warning);
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_scanningMaterialProperty);
            EditorGUILayout.PropertyField(_occlusionMaterialProperty);

            _showEvents = EditorGUILayout.Foldout(_showEvents, "Events");

            if (_showEvents)
            {
                EditorGUILayout.PropertyField(_reconstructionStartedProperty);
                EditorGUILayout.PropertyField(_reconstructionPausedProperty);
                EditorGUILayout.PropertyField(_reconstructionResumedProperty);
                EditorGUILayout.PropertyField(_reconstructionResetProperty);
                EditorGUILayout.PropertyField(_reconstructionSavedProperty);
                EditorGUILayout.PropertyField(_reconstructionLoadedProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}