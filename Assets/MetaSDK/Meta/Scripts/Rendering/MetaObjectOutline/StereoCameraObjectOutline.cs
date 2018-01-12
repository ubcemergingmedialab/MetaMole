using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Meta
{
    /// <summary>
    /// Adds Draw events to the Meta cameras in order to draw the outline effect.
    /// </summary>
    internal class StereoCameraObjectOutline : MonoBehaviour
    {
        private const CameraEvent HookedCameraEvent = CameraEvent.AfterForwardOpaque;

        private CommandBuffer _renderCommands;

        private bool _isInitialized;

        /// <summary>
        /// The object-outlining commandbuffer's size in bytes.
        /// </summary>
        /// <returns></returns>
        internal int GetCameraBufferSizeInBytes()
        {
            return _renderCommands.sizeInBytes;
        }

        [SerializeField]
        private Camera[] _targetCameras;

        [SerializeField]
        private List<OutlineObjectVisualDecorator> _decorators;

        
        internal void Start()
        {
            if (_isInitialized)
                return;

            _decorators = new List<OutlineObjectVisualDecorator>();
            _renderCommands = new CommandBuffer();

            if (_targetCameras != null)
            {
                for (int i = 0; i < _targetCameras.Length; ++i)
                {
                    _targetCameras[i].AddCommandBuffer(HookedCameraEvent, _renderCommands);
                }
            }

            _isInitialized = true;
        }

        private void OnApplicationQuit()
        {
            for (int i = 0; i < _targetCameras.Length; ++i)
            {
                _targetCameras[i].RemoveCommandBuffer(HookedCameraEvent, _renderCommands);
            }
        }

        private void OnDestroy()
        {
            _renderCommands.Clear();
            _renderCommands.Dispose();
        }

        /// <summary>
        /// Adds an object to be outlined.
        /// </summary>
        /// <param name="decorator"></param>
        internal void AddOutlinedObject(OutlineObjectVisualDecorator decorator)
        {
            if (_decorators.Contains(decorator))
            {
                return;
            }
            _decorators.Add(decorator);
            DrawOutline(decorator);
        }

        /// <summary>
        /// Removes an object from those to be outlined.
        /// </summary>
        /// <param name="decorator"></param>
        internal void RemoveOutlinedObject(OutlineObjectVisualDecorator decorator)
        {
            if (_decorators.Remove(decorator))
            {
                _renderCommands.Clear();
                foreach (var dec in _decorators)
                {
                    DrawOutline(dec);
                }
            }
        }

        private void DrawOutline(OutlineObjectVisualDecorator decorator)
        {
            List<GameObject> objectsToOutline = decorator.GetObjectsToDecorate();
            foreach (var go in objectsToOutline)
            {
                Renderer renderer = go.GetComponent<Renderer>();
                if (renderer)
                {
                    for (int i = 0; i < renderer.sharedMaterials.Length; ++i)
                    {
                        _renderCommands.DrawRenderer(renderer, decorator.OutlineMaterial, i);
                    }
                }
            }
        }
    }
}
