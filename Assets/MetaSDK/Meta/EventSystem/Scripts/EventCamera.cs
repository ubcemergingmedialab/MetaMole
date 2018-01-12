using UnityEngine;

namespace Meta
{
    /// <summary>
    /// An EventCamera is used by the MetaMouse to Raycast into the scene
    /// </summary>
    internal class EventCamera : MetaBehaviourInternal, IEventCamera
    {
        /// <summary>
        /// The eventcamera camera
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// The EventCamera's Camera component
        /// </summary>
        public Camera EventCameraRef
        {
            get { return _camera; }
        }

        public Vector3 Position
        {
            get { return _camera.transform.position; }
        }

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            metaContext.Add<IEventCamera>(this);
        }

        private void OnDestroy()
        {
            var context = metaContext;
            if (context == null)
                return;
            context.Remove<IEventCamera>();
        }
    }
}