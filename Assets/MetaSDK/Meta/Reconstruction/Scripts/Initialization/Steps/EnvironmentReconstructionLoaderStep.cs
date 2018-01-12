using System;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Loads the selected environment reconstruction.
    /// </summary>
    public class EnvironmentReconstructionLoaderStep : EnvironmentInitializationStep
    {
        private readonly IMetaReconstruction _metaReconstruction;
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentReconstructionLoaderStep"/> class.
        /// </summary>
        /// <param name="metaMetaReconstruction">Object that manages the environment reconstruction.</param>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        public EnvironmentReconstructionLoaderStep(IMetaReconstruction metaMetaReconstruction, IEnvironmentProfileRepository environmentProfileRepository)
        {
            if (metaMetaReconstruction == null)
            {
                throw new ArgumentNullException("metaMetaReconstruction");
            }
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }

            _environmentProfileRepository = environmentProfileRepository;
            _metaReconstruction = metaMetaReconstruction;
        }

        /// <summary>
        /// Stops the environment initialization process.
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            _metaReconstruction.ReconstructionLoaded.RemoveListener(FinishReconstruction);
        }

        protected override void Initialize()
        {
            IEnvironmentProfile currentEnvironment = _environmentProfileRepository.SelectedEnvironment;

            if (currentEnvironment != null)
            {
                _metaReconstruction.ReconstructionLoaded.AddListener(FinishReconstruction);
                _metaReconstruction.LoadReconstruction(currentEnvironment.Name);
            }
            else
            {
                Finish();
            }
        }

        private void FinishReconstruction(GameObject gameObject)
        {
            _metaReconstruction.ReconstructionLoaded.RemoveListener(FinishReconstruction);
            Finish();
        }
    }
}