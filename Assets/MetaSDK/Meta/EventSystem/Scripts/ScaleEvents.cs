using Meta.Events;
using UnityEngine;
using System;

namespace Meta
{
    /// <summary>
    /// Class to encapsulate scale events.
    /// </summary>
    [Serializable]
    public class ScaleEvents
    {
        /// <summary>
        /// Event called while scaling
        /// </summary>
        [SerializeField]
        private Vector3UnityEvent _onScaleChangeEvent = new Vector3UnityEvent();

        /// <summary>
        /// Event called when we start scaling
        /// </summary>
        [SerializeField]
        private Vector3UnityEvent _onScaleStartEvent = new Vector3UnityEvent();

        /// <summary>
        /// Event called when we finish scaling
        /// </summary>
        [SerializeField]
        private Vector3UnityEvent _onScaleFinishEvent = new Vector3UnityEvent();

        /// <summary>
        /// Event called when the object is ready to be manipulated
        /// </summary>
        [SerializeField]
        private Vector3UnityEvent _onFirstScaleReadyEvent = new Vector3UnityEvent();

        /// <summary>
        /// Event called when we start scaling
        /// </summary>
        public Vector3UnityEvent OnScaleChangeEvent
        {
            get { return _onScaleChangeEvent; }
            set { _onScaleChangeEvent = value; }
        }

        /// <summary>
        /// Event called when we start scaling
        /// </summary>
        public Vector3UnityEvent OnScaleStartEvent
        {
            get { return _onScaleStartEvent; }
            set { _onScaleChangeEvent = value; }
        }

        /// <summary>
        /// Event called when we finish scaling
        /// </summary>
        public Vector3UnityEvent OnScaleFinishEvent
        {
            get { return _onScaleFinishEvent; }
            set { _onScaleChangeEvent = value; }
        }

        /// <summary>
        /// Event called when the object is ready to be manipulated
        /// </summary>
        public Vector3UnityEvent OnFirstScaleReadyEvent
        {
            get { return _onFirstScaleReadyEvent; }
            set { _onFirstScaleReadyEvent = value; }
        }
    }
}