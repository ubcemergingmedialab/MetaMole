using System;
using System.IO;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Resets the default environment.
    /// </summary>
    public class DefaultEnvironmentReset : IEnvironmentReset
    {
        private IEnvironmentProfileRepository _environmentProfileRepository;
        private IMetaReconstruction _metaReconstruction;

        /// <summary>
        /// Creates an instance of <see cref="DefaultEnvironmentReset"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        public DefaultEnvironmentReset(IEnvironmentProfileRepository environmentProfileRepository, IMetaReconstruction metaReconstruction)
        {
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }

            if (metaReconstruction == null)
            {
                throw new ArgumentNullException("metaReconstruction");
            }

            _environmentProfileRepository = environmentProfileRepository;
            _metaReconstruction = metaReconstruction;
        }

        /// <summary>
        /// Creates an instance of <see cref="DefaultEnvironmentReset"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        public DefaultEnvironmentReset(IEnvironmentProfileRepository environmentProfileRepository)
        {
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }
            
            _environmentProfileRepository = environmentProfileRepository;
        }

        /// <summary>
        /// Resets the current environment environment.
        /// </summary>
        public void Reset()
        {
            DeleteDefaultEnvironments();
            if (_metaReconstruction != null)
            {
                _metaReconstruction.CleanMeshes();
            }
        }

        private void DeleteDefaultEnvironments()
        {
            IEnvironmentProfile defaultEnvironment = _environmentProfileRepository.FindByName(EnvironmentConstants.DefaultEnvironmentName);

            if (defaultEnvironment == null)
            {
                return;
            }

            string environmentPath = _environmentProfileRepository.GetPath(defaultEnvironment.Id);
            _environmentProfileRepository.Delete(defaultEnvironment.Id);

            if (Directory.Exists(environmentPath))
            {
                Directory.Delete(environmentPath, true);
            }

            _environmentProfileRepository.Save();
        }
    }
}