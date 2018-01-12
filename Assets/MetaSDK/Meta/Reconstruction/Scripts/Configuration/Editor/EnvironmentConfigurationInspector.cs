using UnityEditor;
using UnityEngine;

namespace Meta.Reconstruction
{
    [CustomEditor(typeof(EnvironmentConfiguration))]
    public class EnvironmentConfigurationInspector : Editor
    {
        private SerializedProperty _environmentScanControllerPrefabProperty;
        private SerializedProperty _metaReconstructionPrefabProperty;
        private SerializedProperty _slamRelocalizationActiveProperty;
        private SerializedProperty _surfaceReconstructionActiveProperty;
        private SerializedProperty _environmentProfileTypeProperty;

        private void OnEnable()
        {
            _environmentScanControllerPrefabProperty = serializedObject.FindProperty("_environmentScanControllerPrefab");
            _metaReconstructionPrefabProperty = serializedObject.FindProperty("_metaReconstructionPrefab");
            _slamRelocalizationActiveProperty = serializedObject.FindProperty("_slamRelocalizationActive");
            _surfaceReconstructionActiveProperty = serializedObject.FindProperty("_surfaceReconstructionActive");
            _environmentProfileTypeProperty = serializedObject.FindProperty("_environmentProfileType");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnvironmentConfiguration environmentConfiguration = target as EnvironmentConfiguration;
            Transform previousMetaReconstructionPrefab = environmentConfiguration.MetaReconstructionPrefab;

            serializedObject.Update();
            EditorGUILayout.PropertyField(_slamRelocalizationActiveProperty);
            EditorGUILayout.PropertyField(_surfaceReconstructionActiveProperty);

            ApplyModifiedProperties();

            if (environmentConfiguration.SlamRelocalizationActive)
            {
                EditorGUILayout.PropertyField(_environmentProfileTypeProperty);
            }

            if (environmentConfiguration.SurfaceReconstructionActive)
            {
                EditorGUILayout.PropertyField(_environmentScanControllerPrefabProperty);
                EditorGUILayout.PropertyField(_metaReconstructionPrefabProperty);
            }

            ApplyModifiedProperties();

            //Being sure that the the meta reconstruction transform has the meta reconstruction script.
            FixMetaReconstructionPrefab(environmentConfiguration, previousMetaReconstructionPrefab);
        }

        private void FixMetaReconstructionPrefab(EnvironmentConfiguration environmentConfiguration, Transform previousMetaReconstructionPrefab)
        {
            if (!Application.isPlaying)
            {
                Transform currentMetaReconstructionPrefab = environmentConfiguration.MetaReconstructionPrefab;

                if (currentMetaReconstructionPrefab != null)
                {
                    IMetaReconstruction metaReconstruction = environmentConfiguration.MetaReconstructionPrefab.GetComponent<IMetaReconstruction>();
                    if (metaReconstruction == null)
                    {
                        environmentConfiguration.MetaReconstructionPrefab = previousMetaReconstructionPrefab;
                    }
                }
            }
        }

        private void ApplyModifiedProperties()
        {
            if (!Application.isPlaying)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}