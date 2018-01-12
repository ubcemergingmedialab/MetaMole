using UnityEngine;

namespace Meta.Mouse
{
    /// <summary>
    /// Stores Configuration for MetaMouse
    /// </summary>
    [System.Serializable]
    public class MetaMouseConfig
    {
        [SerializeField]
        private float _sensitivity = 1.0f;

        [Tooltip("Distance pointer will float away from camera when not hovering over an item.")]
        [SerializeField]
        private float _floatDistance = 100f;

        [Tooltip("Damping on depth changes of pointer.")]
        [SerializeField]
        private float _distanceDamp = .2f;

        [Tooltip("Whether the cursor should be activated and controllable when the scene is started.")]
        [SerializeField]
        private bool _enableOnStart = true;

        /// <summary>
        /// Distance pointer will float away from camera when not hovering over an item.
        /// </summary>
        public float FloatDistance
        {
            get { return _floatDistance; }
        }

        /// <summary>
        /// Damping on depth changes of pointer.
        /// </summary>
        public float DistanceDamp
        {
            get { return _distanceDamp; }
        }

        /// <summary>
        /// Whether the cursor should be activated and controllable when the scene is started.
        /// </summary>
        public float Sensitivity
        {
            get { return _sensitivity; }
            set { _sensitivity = value; }
        }

        public bool EnableOnStart
        {
            get { return _enableOnStart || !Application.isEditor; }
        }
    }
}