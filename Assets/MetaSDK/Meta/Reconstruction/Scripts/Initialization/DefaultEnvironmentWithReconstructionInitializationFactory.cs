using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Creates a <see cref="IEnvironmentInitialization"/> object to initialize a default environment.
    /// </summary>
    public class DefaultEnvironmentWithReconstructionInitializationFactory : IEnvironmentInitializationFactory
    {
        private readonly IMetaReconstruction _metaReconstruction;
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;
        private readonly ISlamLocalizer _slamLocalizer;
        private readonly BaseEnvironmentScanController _scanControllerPrefab;

        /// <summary>
        /// Creates an instance of <see cref="DefaultEnvironmentWithReconstructionInitializationFactory"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        /// <param name="slamLocalizer">Slam type localizer. If null, means that other localizer was selected.</param>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        /// <param name="scanControllerPrefab">Triggerer of the the environment reconstruction scanning process.</param>
        public DefaultEnvironmentWithReconstructionInitializationFactory(IEnvironmentProfileRepository environmentProfileRepository, ISlamLocalizer slamLocalizer, IMetaReconstruction metaReconstruction, BaseEnvironmentScanController scanControllerPrefab)
        {
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }

            if (scanControllerPrefab == null)
            {
                throw new ArgumentNullException("scanControllerPrefab");
            }

            if (metaReconstruction == null)
            {
                throw new ArgumentNullException("metaReconstruction");
            }

            _environmentProfileRepository = environmentProfileRepository;
            _slamLocalizer = slamLocalizer;
            _scanControllerPrefab = scanControllerPrefab;
            _metaReconstruction = metaReconstruction;
        }

        /// <summary>
        /// Creates a <see cref="IEnvironmentInitialization"/> object to initialize a default environment, according to the results of the environment selection process.
        /// </summary>
        /// <param name="environmentSelectionResult">The result of the environment selection process.</param>
        /// <returns>The environment initialization of the given selection process result.</returns>
        public IEnvironmentInitialization CreateEnvironmentInitialization(EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType environmentSelectionResult)
        {
            switch (environmentSelectionResult)
            {
                case EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType.NewEnvironment:
                    return new NewDefaultEnvironmentWithReconstructionInitialization(_environmentProfileRepository, _slamLocalizer, _metaReconstruction, _scanControllerPrefab);
                case EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType.SelectedEnvironment:
                    return new SelectedEnvironmentWithReconstructionInitialization(_environmentProfileRepository, _slamLocalizer, _metaReconstruction);
                default:
                    throw new Exception(string.Format("EnvironmentSelectionResultType {0} is not supported.", environmentSelectionResult));
            }
        }
    }
}