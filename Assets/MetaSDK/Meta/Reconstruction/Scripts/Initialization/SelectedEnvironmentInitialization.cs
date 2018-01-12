using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// The initialization process of a selected environment.
    /// </summary>
    public class SelectedEnvironmentInitialization : MultiStepEnvironmentInitialization
    {
        private readonly ISlamLocalizer _slamLocalizer;
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;

        /// <summary>
        /// Creates an instance of <see cref="SelectedEnvironmentWithReconstructionInitialization"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        /// <param name="slamLocalizer">Slam type localizer.</param>
        public SelectedEnvironmentInitialization(IEnvironmentProfileRepository environmentProfileRepository, ISlamLocalizer slamLocalizer)
        {
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
        }

        protected override EnvironmentInitializationStep[] CreateSteps()
        {
            return new EnvironmentInitializationStep[]
            {
                new EnvironmentProfileSaverStep(true, _environmentProfileRepository),
                new EnvironmentSlamSaverStep(_slamLocalizer, _environmentProfileRepository)
            };
        }
    }
}