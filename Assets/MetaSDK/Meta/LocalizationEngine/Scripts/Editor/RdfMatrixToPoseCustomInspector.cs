using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Custom inspector for RdfMatrixToPose
    /// </summary>
    [CustomEditor(typeof(RdfMatrixToPose))]
    public class RdfMatrixToPoseCustomInspector : Editor
    {
        private RdfMatrixToPose _component;
        private List<SerializedProperty> _matrixElements;

        /// <summary>
        /// Check the component being inspected
        /// </summary>
        private void CheckComponent()
        {
            if (_component == null)
                _component = target as RdfMatrixToPose;
        }

        /// <summary>
        /// Draw the custom inspector
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            CheckComponent();

            DrawKey();
            DrawMatrix4x4();
            DrawControls();
        }

        /// <summary>
        /// Draw the Key property.
        /// </summary>
        private void DrawKey()
        {
            var prop = serializedObject.FindProperty("_key");
            EditorGUILayout.PropertyField(prop);
        }

        /// <summary>
        /// Draw the 4x4 Matrix as a real Matrix.
        /// </summary>
        private void DrawMatrix4x4()
        {
            var list = GetMatrix4x4Elements("_poseMatrix");

            EditorGUILayout.BeginVertical();
            {
                // First Header
                EditorGUILayout.BeginHorizontal();
                {
                    // Space
                    EditorGUILayout.LabelField("", GUILayout.Width(25));

                    // Headers
                    for (int c = 0; c < 4; ++c)
                        GUILayout.Label(string.Format("[{0}]", c));
                }
                EditorGUILayout.EndHorizontal();

                for (int row = 0; row < 4; ++row)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        // Header
                        EditorGUILayout.LabelField(string.Format("[{0}]", row), GUILayout.Width(25));

                        // Data
                        for (int col = 0; col < 4; ++col)
                        {
                            var index = row + 4 * col;
                            var data = EditorGUILayout.FloatField(list[index].floatValue);
                            list[index].SetValue(data);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        /// <summary>
        /// Draw some controls for Reseting and Restoring calibration.
        /// </summary>
        private void DrawControls()
        {
            EditorGUILayout.BeginHorizontal();
            {
                // Reset
                if (GUILayout.Button("Reset to Identity"))
                {
                    _component.Reset();
                }

                // Update
                if (GUILayout.Button("Calibrate"))
                {
                    _component.CalibratePose();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Get the serialized properties of the 4x4 Matrix as a list of Serialized Properties
        /// </summary>
        /// <param name="name">Name of the 4x4 matrix</param>
        /// <returns>List of elements</returns>
        private List<SerializedProperty> GetMatrix4x4Elements(string name)
        {
            if (_matrixElements == null)
            {
                _matrixElements = new List<SerializedProperty>();
                var matrix = serializedObject.FindProperty(name);

                for (int row = 0; row < 4; ++row)
                {
                    for (int col = 0; col < 4; ++col)
                    {
                        var element = matrix.FindPropertyRelative(string.Format("e{0}{1}", row, col));
                        _matrixElements.Add(element);
                    }
                }
            }

            return _matrixElements;
        }
    }
}
