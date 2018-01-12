using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// The initialization process to just run reconstruction after slam is ready.
    /// </summary>
    public class ReconstructionOnlyEnvironmentInitialization : MultiStepEnvironmentInitialization
    {
        private readonly ISlamLocalizer _slamLocalizer;
        private readonly IMetaReconstruction _metaReconstruction;
        private readonly BaseEnvironmentScanController _scanControllerPrefab;

        /// <summary>
        /// Creates an instance of <see cref="NewDefaultEnvironmentWithReconstructionInitialization"/> class.
        /// </summary>
        /// <param name="slamLocalizer">Slam type localizer.</param>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        /// <param name="scanControllerPrefab">Triggerer of the the environment reconstruction scanning process.</param>
        public ReconstructionOnlyEnvironmentInitialization(ISlamLocalizer slamLocalizer, IMetaReconstruction metaReconstruction, BaseEnvironmentScanController scanControllerPrefab)
        {
            if (scanControllerPrefab == null)
            {
                throw new ArgumentNullException("scanControllerPrefab");
            }
            if (slamLocalizer == null)
            {
                throw new ArgumentNullException("slamLocalizer");
            }
            if (metaReconstruction == null)
            {
                throw new ArgumentNullException("metaReconstruction");
            }
            
            _metaReconstruction = metaReconstruction;
            _slamLocalizer = slamLocalizer;
            _scanControllerPrefab = scanControllerPrefab;
        }

        protected override EnvironmentInitializationStep[] CreateSteps()
        {
            return new EnvironmentInitializationStep[]
            {
                new EnvironmentNewSlamMapInitializerStep(_slamLocalizer),
                new EnvironmentReconstructionInitializerStep(new MonoBehaviourProxy<BaseEnvironmentScanController>(_scanControllerPrefab), _metaReconstruction),
            };
        }
    }
}