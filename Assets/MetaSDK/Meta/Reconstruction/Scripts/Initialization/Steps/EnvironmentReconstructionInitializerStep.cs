using UnityEngine;
using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Triggers the environment reconstruction process.
    /// </summary>
    public class EnvironmentReconstructionInitializerStep : EnvironmentInitializationStep
    {
        private readonly IMetaReconstruction _metaReconstruction;
        private  IMonoBehaviourProxy<BaseEnvironmentScanController> _scanSelectorProxy;
        private BaseEnvironmentScanController _scanController;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentReconstructionInitializerStep"/> class.
        /// </summary>
        /// <param name="scanSelectorProxy">Accessor to the the environment reconstruction scanning process.</param>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        public EnvironmentReconstructionInitializerStep(IMonoBehaviourProxy<BaseEnvironmentScanController> scanSelectorProxy, IMetaReconstruction metaReconstruction)
        {
            if (scanSelectorProxy == null)
            {
                throw new ArgumentNullException("scanSelectorProxy");
            }

            if (metaReconstruction == null)
            {
                throw new ArgumentNullException("metaReconstruction");
            }

            _metaReconstruction = metaReconstruction;
            _scanSelectorProxy = scanSelectorProxy;
        }

        /// <summary>
        /// Stops the environment initialization process.
        /// </summary>
        public override void Stop()
        {
            CleanResources();
            base.Stop();
        }

        protected override void Initialize()
        {
            Debug.Assert(_scanSelectorProxy != null, "ScanSelectorProxy cannot be null");
            _scanController = _scanSelectorProxy.Create();
            _scanController.ScanControllerDone.AddListener(OnScanDone);
            _scanController.StartScanning(_metaReconstruction);
        }

        private void OnScanDone()
        {
            Debug.Assert(_scanSelectorProxy != null, "ScanSelectorProxy cannot be null");
            CleanResources();
            Finish();
        }

        private void CleanResources()
        {
            if (_scanController != null)
            {
                _scanController.ScanControllerDone.RemoveListener(OnScanDone);
                _scanController.StopScanning();
                _scanSelectorProxy.Destroy();
            }
            _scanSelectorProxy = null;
        }
    }
}
