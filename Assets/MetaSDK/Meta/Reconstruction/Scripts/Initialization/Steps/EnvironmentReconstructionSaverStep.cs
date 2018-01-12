using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Saves the selected environment reconstruction.
    /// </summary>
    public class EnvironmentReconstructionSaverStep : EnvironmentInitializationStep
    {
        private readonly IMetaReconstruction _metaReconstruction;
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentReconstructionSaverStep"/> class.
        /// </summary>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        public EnvironmentReconstructionSaverStep(IMetaReconstruction metaReconstruction, IEnvironmentProfileRepository environmentProfileRepository)
        {
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
        }

        /// <summary>
        /// Stops the environment initialization process.
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            _metaReconstruction.ReconstructionSaved.RemoveListener(FinishSave);
        }

        protected override void Initialize()
        {
            IEnvironmentProfile currentEnvironment = _environmentProfileRepository.SelectedEnvironment;
            if (currentEnvironment != null)
            {
                _metaReconstruction.ReconstructionSaved.AddListener(FinishSave);
                _metaReconstruction.SaveReconstruction(currentEnvironment.Name, false);
            }
            else
            {
                Finish();
            }
        }

        private void FinishSave()
        {
            _metaReconstruction.ReconstructionSaved.RemoveListener(FinishSave);
            Finish();
        }
    }
}