using UnityEngine;
using UnityEditor;

namespace Meta.Buttons
{
    /// <summary>
    /// Editor window that handles the emulation of the buttons of the headset
    /// </summary>
    public class EditorMetaButtonEventWindow : EditorWindow
    {
        private MetaButton _currentButtonEvent;
        private double _targetLongPressTime = 3;
        private bool _longPressed = false;
        private double _elapsedTime;
        private double _currentTime;
        private string _currentType;
        private string _currentState;
        private double _timestamp;

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("This window allows to emulate the buttons of Meta2. This is useful for debugging purposes.", MessageType.Info);
            DrawCurrentButton();
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();
                {
                    HandleButton("Camera", ButtonType.ButtonCamera);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                {
                    HandleButton("Volume Up", ButtonType.ButtonVolumeUp);
                    HandleButton("Volume Down", ButtonType.ButtonVolumeDown);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void OnInspectorUpdate()
        {
            UpdateCurrentValues();
            if (_currentButtonEvent == null)
            {
                if (_elapsedTime != 0)
                    Repaint();
                _elapsedTime = 0;
                _longPressed = false;
                return;
            }

            _elapsedTime += EditorApplication.timeSinceStartup - _currentTime;
            _currentTime = EditorApplication.timeSinceStartup;

            if (!_longPressed && _elapsedTime >= _targetLongPressTime)
            {
                _longPressed = true;
                _currentButtonEvent.State = ButtonState.ButtonLongPress;
                EditorMetaButtonEventInterop.ButtonEvents.Enqueue(_currentButtonEvent);
            }
            Repaint();
        }

        /// <summary>
        /// Handles the current button pressed
        /// </summary>
        /// <param name="name">name of the button</param>
        /// <param name="type">Type of the button</param>
        private void HandleButton(string name, ButtonType type)
        {
            bool buttonIsPressed = GUILayout.RepeatButton(name);
            if (!Application.isPlaying)
            {
                return;
            }
            if (Event.current.type != EventType.Repaint)
            {
                return;
            }

            if (buttonIsPressed)
            {
                // Create a button if the current event is null
                if (_currentButtonEvent == null)
                {
                    _currentTime = EditorApplication.timeSinceStartup;
                    _currentButtonEvent = new MetaButton(type, ButtonState.ButtonShortPress, 0);
                    EditorMetaButtonEventInterop.ButtonEvents.Enqueue(_currentButtonEvent);
                    return;
                }
            }
            else
            {
                if (_currentButtonEvent == null)
                {
                    return;
                }
                if (_currentButtonEvent.Type != type)
                {
                    return;
                }

                // Release Button
                _currentButtonEvent.State = ButtonState.ButtonRelease;
                EditorMetaButtonEventInterop.ButtonEvents.Enqueue(_currentButtonEvent);
                _currentButtonEvent = null;
            }
        }

        #region Current Button Interaction
        /// <summary>
        /// Update the current button interaction values
        /// </summary>
        private void UpdateCurrentValues()
        {
            if (_currentButtonEvent == null)
            {
                _currentType = "None";
                _currentState = "None";
                _timestamp = 0;
            }
            else
            {
                _currentType = string.Format("{0}", _currentButtonEvent.Type);
                _currentState = string.Format("{0}", _currentButtonEvent.State);
                _timestamp = _currentButtonEvent.Timestamp;
            }
        }

        /// <summary>
        /// Draws the current button interaction values
        /// </summary>
        private void DrawCurrentButton()
        {
            EditorGUILayout.LabelField("Current Button Interaction");
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(12);
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Type:");
                    EditorGUILayout.LabelField("State:");
                    EditorGUILayout.LabelField("Timestamp:");
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField(_currentType);
                    EditorGUILayout.LabelField(_currentState);
                    EditorGUILayout.LabelField(string.Format("{0:0.00}", _timestamp));
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
        #endregion
    }
}
