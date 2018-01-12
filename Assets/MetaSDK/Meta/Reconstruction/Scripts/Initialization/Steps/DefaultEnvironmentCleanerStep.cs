using System;
using System.IO;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Deletes the default environment.
    /// </summary>
    public class DefaultEnvironmentCleanerStep : EnvironmentInitializationStep
    {
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;

        /// <summary>
        /// Creates an instance of <see cref="DefaultEnvironmentCleanerStep"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        public DefaultEnvironmentCleanerStep(IEnvironmentProfileRepository environmentProfileRepository)
        {
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }
            _environmentProfileRepository = environmentProfileRepository;
        }

        protected override void Initialize()
        {
            IEnvironmentProfile defaultEnvironment = _environmentProfileRepository.GetDefault();

            if (defaultEnvironment == null)
            {
                Finish();
                return;
            }

            string environmentPath = _environmentProfileRepository.GetPath(defaultEnvironment.Id);
            _environmentProfileRepository.Delete(defaultEnvironment.Id);

            if (Directory.Exists(environmentPath))
            {
                Directory.Delete(environmentPath, true);
            }
            
            Finish();
        }
    }
}