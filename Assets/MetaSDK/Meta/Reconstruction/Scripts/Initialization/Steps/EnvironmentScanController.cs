using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Triggers the environment reconstruction scanning process, using the keyboard.
    /// </summary>
    public class EnvironmentScanController : BaseEnvironmentScanController
    {
        private enum ScanState
        {
            Waiting,
            Scanning,
            Finished
        }

        [Tooltip("Keys used to start the scan process.")]
        [SerializeField]
        private KeySet _startScanningKey;

        [Tooltip("Keys used to stop the scan process.")]
        [SerializeField]
        private KeySet _stopScanningKey;

        [Tooltip("Time to wait before finish the scan selector.")]
        [SerializeField]
        private float _finishWaitDuration = 0f;

        [Tooltip("Occurs when scan controller is ready to start scanning.")]
        [SerializeField]
        private UnityEvent _scanControllerStarted = new UnityEvent();

        [Tooltip("Occurs when scan process is finished.")]
        [SerializeField]
        private UnityEvent _scanFinished = new UnityEvent();

        [Tooltip("Occurs when scan process is stopped.")]
        [SerializeField]
        private UnityEvent _scanStopped = new UnityEvent();

        private ScanState _scanState = ScanState.Waiting;
        
        private void Update()
        {
            if (_startScanningKey.GetDown())
            {
                StartScanning();
            }

            if (_stopScanningKey.GetDown())
            {
                FinishScanning();
            }
        }

        /// <summary>
        /// Stops environment reconstruction scanning process.
        /// </summary>
        public override void StopScanning()
        {
            StopAllCoroutines();
            if (_scanState == ScanState.Waiting || _scanState == ScanState.Finished)
            {
                return;
            }
            StopReconstruction();
            _scanStopped.Invoke();
        }

        private void StopReconstruction()
        {
            Validate();
            _metaReconstruction.StopReconstruction();
        }

        private void StartScanning()
        {
            Validate();
            if (_scanState == ScanState.Finished || _scanState == ScanState.Scanning)
            {
                return;
            }
            if (_scanState == ScanState.Waiting)
            {
                _scanState = ScanState.Scanning;
                _metaReconstruction.InitReconstruction();
            }
        }

        private void FinishScanning()
        {
            if (_scanState == ScanState.Waiting || _scanState == ScanState.Finished)
            {
                return;
            }
            StopReconstruction();
            StartCoroutine(FinishScan());
        }

        /// <summary>
        /// Initializes the scan controller.
        /// </summary>
        protected override void Initialize()
        {
            _scanControllerStarted.Invoke();
        }

        private IEnumerator FinishScan()
        {
            _scanState = ScanState.Finished;
            _scanFinished.Invoke();
            if (_finishWaitDuration > 0)
            {
                yield return new WaitForSeconds(_finishWaitDuration);
            }
            Finish();
        }

        private void Validate()
        {
            if (_metaReconstruction == null)
            {
                throw new Exception("Please set a MetaReconstruction in order to control the scan process.");
            }
        }
    }
}