using System;
using UnityEngine;
using UnityEditor;
using Meta.EditorUtils;

namespace Meta.Buttons
{
    /// <summary>
    /// Custom Inspector for MetaButtonIndividualEventBroadcaster
    /// </summary>
    [CustomEditor(typeof(MetaButtonIndividualEventBroadcaster))]
    public class MetaButtonIndividualEventBroadcasterCustomInspector : Editor
    {
        private bool _foldCamera = true;
        private bool _foldVolumeUp = true;
        private bool _foldVolumeDown = true;
        private int offset = 12;
        private MetaButtonIndividualEventBroadcaster _component;
        private ColorStack _colorStack = new ColorStack();

        public override void OnInspectorGUI()
        {
            if (_component == null)
            {
                _component = (MetaButtonIndividualEventBroadcaster)target;
            }
            _colorStack.CollectDefaults();

            // Camera Events
            _component.EnableCameraEvents = EditorGUILayout.Toggle("Enable Camera Events", _component.EnableCameraEvents);
            DrawWithSpace(DrawCameraEvents);

            // Volume Up Events
            _component.EnableVolumeUpEvents = EditorGUILayout.Toggle("Enable Volume Up Events", _component.EnableVolumeUpEvents);
            DrawWithSpace(DrawVolumeUpEvents);

            // Volume Down Events
            _component.EnableVolumeDownEvents = EditorGUILayout.Toggle("Enable Volume Up Events", _component.EnableVolumeDownEvents);
            DrawWithSpace(DrawVolumeDownEvents);

            // Update Object
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        /// <summary>
        /// Draw the Camera Unity Events
        /// </summary>
        private void DrawCameraEvents()
        {
            _colorStack.PushBackground(_component.EnableCameraEvents ? Color.green : Color.yellow);
            _foldCamera = EditorGUILayout.Foldout(_foldCamera, "Camera Events");
            if (_foldCamera)
            {
                var cameraField = serializedObject.FindProperty("_cameraEvent");
                EditorGUILayout.PropertyField(cameraField);

            }
            _colorStack.PopBackground();
        }

        /// <summary>
        /// Draw the Volume Up Unity Events
        /// </summary>
        private void DrawVolumeUpEvents()
        {
            _colorStack.PushBackground(_component.EnableVolumeUpEvents ? Color.green : Color.yellow);
            _foldVolumeUp = EditorGUILayout.Foldout(_foldVolumeUp, "Volume Up Events");
            if (_foldVolumeUp)
            {
                var volumeUpField = serializedObject.FindProperty("_volumeUpEvent");
                EditorGUILayout.PropertyField(volumeUpField);
            }
            _colorStack.PopBackground();
        }

        /// <summary>
        /// Draw the Volume Down Unity Events
        /// </summary>
        private void DrawVolumeDownEvents()
        {
            _colorStack.PushBackground(_component.EnableVolumeDownEvents ? Color.green : Color.yellow);
            _foldVolumeDown = EditorGUILayout.Foldout(_foldVolumeDown, "Volume Down Events");
            if (_foldVolumeDown)
            {
                var volumeDownField = serializedObject.FindProperty("_volumeDownEvent");
                EditorGUILayout.PropertyField(volumeDownField);
            }
            _colorStack.PopBackground();
        }

        /// <summary>
        /// Draw the given action inside a tabulated space 
        /// </summary>
        /// <param name="drawAction">Action to execute inside the space</param>
        private void DrawWithSpace(Action drawAction)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(offset);
                EditorGUILayout.BeginVertical();
                {
                    drawAction.Invoke();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
