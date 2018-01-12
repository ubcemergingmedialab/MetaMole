using UnityEngine;
using System.IO;

namespace Meta.Mouse
{
    internal class PlaybackInputWrapper : IInputWrapper
    {
        private int _framesRead;
        private int _totalFrames;
        private float _mouseSensitivity;
        private Vector2 _mouseScrollDelta = new Vector2();
        private float _mouseXAxis;
        private float _mouseYAxis;
        private bool _getMouseButton;
        private bool _getMouseButtonUp;
        private bool _getMouseButtonDown;
        private float _screenHeight;
        private float _screenWidth;
        private BinaryReader _reader;

        public int framesRead
        {
            get { return _framesRead; }
        }

        public int numFrames
        {
            get { return _totalFrames; }
        }

        public float MouseSensitivity
        {
            get { return _mouseSensitivity; }
        }

        public PlaybackInputWrapper(BinaryReader reader)
        {
            _reader = reader;
            _totalFrames = _reader.ReadInt32();
            _mouseSensitivity = _reader.ReadSingle();
            ReadFrame();
        }

        public void ReadFrame()
        {
            _screenWidth = _reader.ReadSingle();
            _screenHeight = _reader.ReadSingle();
            Vector2 screenSizeRatio = new Vector2(Screen.width / _screenWidth, Screen.height / _screenHeight);
            _mouseXAxis = _reader.ReadSingle() * screenSizeRatio.x;
            _mouseYAxis = _reader.ReadSingle() * screenSizeRatio.y;
            _getMouseButton = _reader.ReadBoolean();
            _getMouseButtonUp = _reader.ReadBoolean();
            _getMouseButtonDown = _reader.ReadBoolean();
            _framesRead++;
        }

        public CursorLockMode LockState { get; set; }
        public bool Visible { get; set; }

        public Vector3 GetMousePosition()
        {
            // TODO: this should almost CERTAINLY be 
            // return _mousePosition;
            //#warning returning _mouseScrollDelta instead of _mousePosition
            return _mouseScrollDelta;
        }

        public Vector2 GetMouseScrollDelta()
        {
            return Input.mouseScrollDelta;
        }

        public float GetAxis(string axisName)
        {
            if (axisName == "Mouse X")
            {
                return _mouseXAxis;
            }
            else if (axisName == "Mouse Y")
            {
                return _mouseYAxis;
            }
            return 0f;
        }

        public bool GetMouseButton(int button)
        {
            return _getMouseButton;
        }

        public bool GetMouseButtonUp(int button)
        {
            return _getMouseButtonUp;
        }

        public bool GetMouseButtonDown(int button)
        {
            return _getMouseButtonDown;
        }

        public Rect GetScreenRect()
        {
            return new Rect(0, 0, _screenWidth, _screenHeight);
        }

        public void CloseFile()
        {
            _reader.Close();
        }
    }
}