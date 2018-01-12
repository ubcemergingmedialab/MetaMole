using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Resets the environment reconstruction.
    /// </summary>
    public class ReconstructionOnlyEnvironmentReset : IEnvironmentReset
    {
        private IMetaReconstruction _metaReconstruction;

        /// <summary>
        /// Creates an instance of <see cref="ReconstructionOnlyEnvironmentReset"/> class.
        /// </summary>
        /// <param name="metaReconstruction">Object that manages the environment reconstruction.</param>
        public ReconstructionOnlyEnvironmentReset(IMetaReconstruction metaReconstruction)
        {
            if (metaReconstruction == null)
            {
                throw new ArgumentNullException("metaReconstruction");
            }

            _metaReconstruction = metaReconstruction;
        }

        /// <summary>
        /// Resets the current environment environment.
        /// </summary>
        public void Reset()
        {
            if (_metaReconstruction != null)
            {
                _metaReconstruction.CleanMeshes();
            }
        }
    }
}