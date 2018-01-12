using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Creates a <see cref="IEnvironmentInitialization"/> object to initialize a default environment.
    /// </summary>
    public class DefaultEnvironmentInitializationFactory : IEnvironmentInitializationFactory
    {
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;
        private readonly ISlamLocalizer _slamLocalizer;

        /// <summary>
        /// Creates an instance of <see cref="DefaultEnvironmentInitializationFactory"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        /// <param name="slamLocalizer">Slam type localizer. If null, means that other localizer was selected.</param>
        public DefaultEnvironmentInitializationFactory(IEnvironmentProfileRepository environmentProfileRepository, ISlamLocalizer slamLocalizer)
        {
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }

            _environmentProfileRepository = environmentProfileRepository;
            _slamLocalizer = slamLocalizer;
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
                    return new NewDefaultEnvironmentInitialization(_environmentProfileRepository, _slamLocalizer);
                case EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType.SelectedEnvironment:
                    return new SelectedEnvironmentInitialization(_environmentProfileRepository, _slamLocalizer);
                default:
                    throw new Exception(string.Format("EnvironmentSelectionResultType {0} is not supported.", environmentSelectionResult));
            }
        }
    }
}