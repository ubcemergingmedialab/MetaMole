using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// The initialization process to create a default environment with reconstruction.
    /// </summary>
    public class NewDefaultEnvironmentWithReconstructionInitialization : MultiStepEnvironmentInitialization
    {
        private readonly ISlamLocalizer _slamLocalizer;
        private readonly IMetaReconstruction _metaReconstruction;
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;
        private readonly BaseEnvironmentScanController _scanControllerPrefab;

        /// <summary>
        /// Creates an instance of <see cref="NewDefaultEnvironmentWithReconstructionInitialization"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        /// <param name="slamLocalizer">Slam type localizer.</param>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        /// <param name="scanControllerPrefab">Triggerer of the the environment reconstruction scanning process.</param>
        public NewDefaultEnvironmentWithReconstructionInitialization(IEnvironmentProfileRepository environmentProfileRepository, ISlamLocalizer slamLocalizer, IMetaReconstruction metaReconstruction, BaseEnvironmentScanController scanControllerPrefab)
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
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }

            _environmentProfileRepository = environmentProfileRepository;
            _metaReconstruction = metaReconstruction;
            _slamLocalizer = slamLocalizer;
            _scanControllerPrefab = scanControllerPrefab;
        }

        protected override EnvironmentInitializationStep[] CreateSteps()
        {
            return new EnvironmentInitializationStep[]
            {
                new DefaultEnvironmentCleanerStep(_environmentProfileRepository), 
                new DefaultEnvironmentProfileCreatorStep(_environmentProfileRepository),
                new EnvironmentNewSlamMapInitializerStep(_slamLocalizer),
                new EnvironmentSlamSaverStep(_slamLocalizer, _environmentProfileRepository),
                new EnvironmentProfileSaverStep(true, _environmentProfileRepository),
                new EnvironmentReconstructionInitializerStep(new MonoBehaviourProxy<BaseEnvironmentScanController>(_scanControllerPrefab), _metaReconstruction),
                new EnvironmentReconstructionSaverStep(_metaReconstruction, _environmentProfileRepository),
                new EnvironmentProfileSaverStep(true, _environmentProfileRepository)
            };
        }
    }
}