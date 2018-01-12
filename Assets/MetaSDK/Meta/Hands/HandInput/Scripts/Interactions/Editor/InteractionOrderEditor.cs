using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Meta
{
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(InteractionOrder))]
    [CanEditMultipleObjects]
    public class InteractionOrderEditor : Editor
    {
        private ReorderableList _itemReorderableList;

        private void OnEnable()
        {
            _itemReorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("_itemList"), true, true, true, true);
            _itemReorderableList.drawElementCallback = DrawElementCallback;
            _itemReorderableList.elementHeightCallback = ElementHeightCallback;
            _itemReorderableList.drawHeaderCallback = DrawHeaderCallback;
            _itemReorderableList.drawElementBackgroundCallback = DrawElementBackgroundCallback;
        }

        private void DrawHeaderCallback(Rect rect)
        {
            EditorGUI.LabelField(rect, "Interaction Order");
        }

        private float ElementHeightCallback(int index)
        {
            Repaint();
            SerializedProperty element = _itemReorderableList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty manipulatorListSerializedProperty = element.FindPropertyRelative("_interactionList");
            return (EditorGUIUtility.singleLineHeight * manipulatorListSerializedProperty.arraySize) + EditorGUIUtility.singleLineHeight + 2;
        }

        private void DrawElementBackgroundCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            Rect drawRect = new Rect(rect.x, rect.y, rect.width, ElementHeightCallback(index));
            if (isActive && isFocused)
            {
                EditorGUI.DrawRect(drawRect, new Color(80f / 255f, 138f / 255f, 204f / 255f));
            }
            else if (isActive)
            {
                EditorGUI.DrawRect(drawRect, new Color(100f / 255f, 100f / 255f, 100f / 255f));
            }
        }

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.y += 2;
            SerializedProperty element = _itemReorderableList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty manipulatorListSerializedProperty = element.FindPropertyRelative("_interactionList");

            Rect drawRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            Interaction interaction = manipulatorListSerializedProperty.GetArrayElementAtIndex(0).objectReferenceValue as Interaction;
            if (interaction != null)
            {
                EditorGUI.LabelField(drawRect, new GUIContent("State: " + interaction.State));
            }
            drawRect.y += drawRect.height;
            for (int i = 0; i < manipulatorListSerializedProperty.arraySize; ++i)
            {
                EditorGUI.PropertyField(drawRect, manipulatorListSerializedProperty.GetArrayElementAtIndex(i), GUIContent.none);
                drawRect.y += drawRect.height;
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();
            _itemReorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
            {
                InteractionOrder interactionOrder = target as InteractionOrder;
                interactionOrder.RemoveEmptyManipulations();
            }
        }
    }
}