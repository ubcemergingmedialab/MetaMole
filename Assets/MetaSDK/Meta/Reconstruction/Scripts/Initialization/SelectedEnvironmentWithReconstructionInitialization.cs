using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// The initialization process of a selected environment with reconstruction.
    /// </summary>
    public class SelectedEnvironmentWithReconstructionInitialization : MultiStepEnvironmentInitialization
    {
        private readonly IMetaReconstruction _metaReconstruction;
        private readonly ISlamLocalizer _slamLocalizer;
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;

        /// <summary>
        /// Creates an instance of <see cref="SelectedEnvironmentWithReconstructionInitialization"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        /// <param name="slamLocalizer">Slam type localizer.</param>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        public SelectedEnvironmentWithReconstructionInitialization(IEnvironmentProfileRepository environmentProfileRepository, ISlamLocalizer slamLocalizer, IMetaReconstruction metaMetaReconstruction)
        {
            if (metaMetaReconstruction == null)
            {
                throw new ArgumentNullException("metaMetaReconstruction");
            }
            if (slamLocalizer == null)
            {
                throw new ArgumentNullException("slamLocalizer");
            }
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }

            _environmentProfileRepository = environmentProfileRepository;
            _slamLocalizer = slamLocalizer;
            _metaReconstruction = metaMetaReconstruction;
        }

        protected override EnvironmentInitializationStep[] CreateSteps()
        {
            return new EnvironmentInitializationStep[]
            {
                new EnvironmentProfileSaverStep(true, _environmentProfileRepository),
                new EnvironmentSlamSaverStep(_slamLocalizer, _environmentProfileRepository),
                new EnvironmentReconstructionLoaderStep(_metaReconstruction, _environmentProfileRepository)
            };
        }
    }
}