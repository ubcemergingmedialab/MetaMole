using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// The initialization process to create a default environment.
    /// </summary>
    public class NewDefaultEnvironmentInitialization : MultiStepEnvironmentInitialization
    {
        private readonly ISlamLocalizer _slamLocalizer;
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;

        /// <summary>
        /// Creates an instance of <see cref="NewDefaultEnvironmentInitialization"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        /// <param name="slamLocalizer">Slam type localizer.</param>
        public NewDefaultEnvironmentInitialization(IEnvironmentProfileRepository environmentProfileRepository, ISlamLocalizer slamLocalizer)
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
                new DefaultEnvironmentCleanerStep(_environmentProfileRepository), 
                new DefaultEnvironmentProfileCreatorStep(_environmentProfileRepository),
                new EnvironmentNewSlamMapInitializerStep(_slamLocalizer),
                new EnvironmentSlamSaverStep(_slamLocalizer, _environmentProfileRepository),
                new EnvironmentProfileSaverStep(true, _environmentProfileRepository)
            };
        }
    }
}