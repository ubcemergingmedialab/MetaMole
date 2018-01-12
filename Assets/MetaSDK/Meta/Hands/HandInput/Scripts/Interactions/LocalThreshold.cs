using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Grab/Release thresholds
    /// </summary>
    [System.Serializable]
    public class LocalThreshold
    {
        /// <summary>
        /// Should the LocalThreshold be used?
        /// </summary>
        [SerializeField]
        private bool _use = false;

        /// <summary>
        /// Grab threshold
        /// A higher value makes it [] to grab
        /// </summary>
        [SerializeField]
        private float _grab = .3f;

        /// <summary>
        /// Release threshold
        /// A higher value makes it [] to release
        /// </summary>
        [SerializeField]
        private float _release = .4f;

        /// <summary>
        /// Should the LocalThreshold be used?
        /// </summary>
        public bool Use
        {
            get { return _use; }
        }

        /// <summary>
        /// Grab threshold
        /// A higher value makes it [] to grab
        /// </summary>
        public float Grab
        {
            get { return _grab; }
        }

        /// <summary>
        /// Release threshold
        /// A higher value makes it [] to release
        /// </summary>
        public float Release
        {
            get { return _release; }
        }
    }
}