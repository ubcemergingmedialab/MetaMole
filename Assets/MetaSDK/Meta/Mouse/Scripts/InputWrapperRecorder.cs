using UnityEngine;
using System.IO;

namespace Meta.Mouse
{
    internal class InputWrapperRecorder
    {
        private readonly IInputWrapper _inputWrapper;
        private readonly BinaryWriter _writer;
        private int _numFrames = 0;

        public InputWrapperRecorder(BinaryWriter writer, IInputWrapper inputWrapper, MetaMouseConfig metaMouseConfig)
        {
            _inputWrapper = inputWrapper;
            _writer = writer;
            // placeholder for number of frames recorded
            _writer.Write(0);
            _writer.Write(metaMouseConfig.Sensitivity);
        }

        public void Record(Vector3 virtualPointerPos)
        {
            _writer.Write(_inputWrapper.GetScreenRect().width);
            _writer.Write(_inputWrapper.GetScreenRect().height);
            _writer.Write(_inputWrapper.GetMousePosition().x);
            _writer.Write(_inputWrapper.GetMousePosition().y);
            _writer.Write(_inputWrapper.GetAxis("Mouse X"));
            _writer.Write(_inputWrapper.GetAxis("Mouse Y"));
            _writer.Write(_inputWrapper.GetMouseButton(0));
            _writer.Write(_inputWrapper.GetMouseButtonUp(0));
            _writer.Write(_inputWrapper.GetMouseButtonDown(0));
            _numFrames++;
        }

        public void CloseFile()
        {
            _writer.Seek(0, SeekOrigin.Begin);
            _writer.Write(_numFrames);
            _writer.Close();
        }
    }
}