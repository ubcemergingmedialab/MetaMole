using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Meta.Mouse
{
    public class UnityInputWrapper : IInputWrapper
    {
        private readonly WindowsUnityWindow _unityWindow = new WindowsUnityWindow();

        public CursorLockMode LockState
        {
            get { return Cursor.lockState; }
            set
            {
                _unityWindow.SetUnityWindowForeground();
#if UNITY_EDITOR
                GetGameViewEditorWindow().Focus();
#endif
                Cursor.lockState = value;
            }
        }

        public bool Visible
        {
            get { return Cursor.visible; }
            set
            {
                Cursor.visible = value;
            }
        }

        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }

        public Vector2 GetMouseScrollDelta()
        {
            return Input.mouseScrollDelta;
        }

        public float GetAxis(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        public bool GetMouseButton(int button)
        {
            return Input.GetMouseButton(button);
        }

        public bool GetMouseButtonUp(int button)
        {
            return Input.GetMouseButtonUp(button);
        }

        public bool GetMouseButtonDown(int button)
        {
            return Input.GetMouseButtonDown(button);
        }

        public Rect GetScreenRect()
        {
#if UNITY_EDITOR
            EditorWindow window = GetGameViewEditorWindow();
            return new Rect(0, 0, window.position.width, window.position.height);
#else
            return new Rect(0, 0, 2560.0f, 1440.0f);
#endif
        }

#if UNITY_EDITOR
        private EditorWindow GetGameViewEditorWindow()
        {
            foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
            {
                if (window.GetType().Name == "GameView")
                {
                    return window;
                }
            }
            return null;
        }
#endif
    }
}