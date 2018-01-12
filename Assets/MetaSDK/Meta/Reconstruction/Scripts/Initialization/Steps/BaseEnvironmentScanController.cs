using UnityEngine;
using UnityEngine.Events;
using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Controlls the environment reconstruction scanning process.
    /// </summary>
    public abstract class BaseEnvironmentScanController : MonoBehaviour
    {
        [Tooltip("Event triggered when the scan process is finished.")]
        [SerializeField]
        private UnityEvent _scanControllerDone = new UnityEvent();

        protected IMetaReconstruction _metaReconstruction;

        /// <summary>
        /// Occurs when the scan controller finishes the environment reconstruction scanning process.
        /// </summary>
        public UnityEvent ScanControllerDone
        {
            get { return _scanControllerDone; }
        }

        /// <summary>
        /// Stops the environment reconstruction canning process.
        /// </summary>
        public abstract void StopScanning();

        /// <summary>
        /// Starts the environment reconstruction scanning process.
        /// </summary>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        public void StartScanning(IMetaReconstruction metaReconstruction)
        {
            if (metaReconstruction == null)
            {
                throw new ArgumentNullException("metaReconstruction");
            }

            _metaReconstruction = metaReconstruction;
            Initialize();
        }

        /// <summary>
        /// Initializes the scan controller.
        /// </summary>
        protected virtual void Initialize() { }

        /// <summary>
        /// Finishes the scan controller.
        /// </summary>
        protected void Finish()
        {
            _scanControllerDone.Invoke();
        }
    }
}