using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using Meta;
using UnityEditor.SceneManagement;

/// <summary>
/// Changes the appearance of the the Inspector used for the script 
/// 'MetaLocalizationSettings'. See script 'MetaLocalizationSettings'
/// for more information.
/// </summary>
[CustomEditor(typeof(MetaLocalizationSettings))]
[Serializable]
public class MetaLocalizationSettingsInspector : Editor {


    public override void OnInspectorGUI()
    {
        MetaLocalizationSettings mls = target as MetaLocalizationSettings;
        List<Type> types = mls.GetLocalizationTypes();

        EditorGUI.BeginChangeCheck();
        ILocalizer assignedLocalizer = mls.GetAssignedLocalizer();
        int oldTypeIndex = (assignedLocalizer == null)? 0 : types.IndexOf(assignedLocalizer.GetType());
        
        int typeIndex = EditorGUILayout.Popup("Current Localizer: ", oldTypeIndex, types.ConvertAll(t => t.ToString()).ToArray());

        if (types.Count > 0)
        {
            string assignedLocalizerName = (assignedLocalizer == null)? types[0].ToString() : assignedLocalizer.GetType().ToString();
            SerializedProperty selectedLocalizerProperty = serializedObject.FindProperty("_selectedLocalizerName");
            if (selectedLocalizerProperty != null)
            {
                selectedLocalizerProperty.SetValue(assignedLocalizerName);
            }
            serializedObject.ApplyModifiedProperties();
        }
        
        if (EditorGUI.EndChangeCheck() || assignedLocalizer == null)
        {
            Undo.RecordObject(mls, "localizer changed");
            // This instructs Unity to produce a new snapshot of the 'MetaLocalizationSettings' instance 
            // so that it is maintained across from the editor to playing in the editor.
            EditorUtility.SetDirty(mls); 
            mls.AssignLocalizationType(types[typeIndex]);
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            

            //IMPORTANT: Nothing after ExitGUI will be called!
            EditorGUIUtility.ExitGUI(); // prevent the GUI from being drawn with a null member
            //Nothing after ExitGUI will be called.
        }
    }
}
