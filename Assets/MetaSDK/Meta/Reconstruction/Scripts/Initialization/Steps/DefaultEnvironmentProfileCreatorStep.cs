using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Creates a default environment profile.
    /// </summary>
    public class DefaultEnvironmentProfileCreatorStep : EnvironmentInitializationStep
    {
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;

        /// <summary>
        /// Creates an instance of <see cref="DefaultEnvironmentProfileCreatorStep"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        public DefaultEnvironmentProfileCreatorStep(IEnvironmentProfileRepository environmentProfileRepository)
        {
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }
            _environmentProfileRepository = environmentProfileRepository;
        }

        protected override void Initialize()
        {
            _environmentProfileRepository.CreateDefault();
            Finish();
        }
    }
}