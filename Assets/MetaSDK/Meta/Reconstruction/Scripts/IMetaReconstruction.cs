using UnityEngine.Events;

namespace Meta
{
    public interface IMetaReconstruction
    {
        /// <summary>
        /// Occurs after the reconstruction process is initialized.
        /// </summary>
        UnityEvent ReconstructionStarted { get; }

        /// <summary>
        /// Occurs when the reconstruction process is paused.
        /// </summary>
        UnityEvent ReconstructionPaused { get; }

        /// <summary>
        /// Occurs when the reconstruction process is resumed.
        /// </summary>
        UnityEvent ReconstructionResumed { get; }

        /// <summary>
        /// Occurs after the reconstruction is reset.
        /// </summary>
        UnityEvent ReconstructionReset { get; }

        /// <summary>
        /// Occurs after all the meshes are saved.
        /// </summary>
        UnityEvent ReconstructionSaved { get; }

        /// <summary>
        /// Occurs after all saved meshes are loaded on the scene. Returns the parent GameObject of all the reconstruction meshes.
        /// </summary>
        GameObjectEvent ReconstructionLoaded { get; }

        /// <summary>
        /// Initializes the reconstruction process.
        /// </summary>
        void InitReconstruction();

        /// <summary>
        /// Toggles on and off the reconstruction process.
        /// </summary>
        void PauseResumeReconstruction();

        /// <summary>
        /// Resets the reconstruction mesh.
        /// </summary>
        void ResetReconstruction();

        /// <summary>
        /// Restart the reconstruction process.
        /// </summary>
        void RestartReconstruction();

        /// <summary>
        /// Stops the reconstruction process.
        /// </summary>
        void StopReconstruction();

        /// <summary>
        /// Loads the reconstruction for the given map or the one currently active.
        /// <param name="profileName">The slam map name</param>
        /// </summary>
        void LoadReconstruction(string profileName = null);

        /// <summary>
        /// Cleans the current environment meshes.
        /// </summary>
        void CleanMeshes();

        /// <summary>
        /// Save the current scanned reconstruction in .obj files
        /// <param name="environmentProfileName">The environment profile name</param>
        /// <param name="saveChangesInProfile">Whether to save changes in profile or not</param>
        /// </summary>
        void SaveReconstruction(string environmentProfileName = null, bool saveChangesInProfile = true);

        /// <summary>
        /// Delete meshes related to the given map.
        /// </summary>
        /// <param name="profileName">The slam map name</param>
        /// <param name="saveChangesInProfile">Whether to save changes in profile or not</param>
        void DeleteReconstructionMeshFiles(string profileName, bool saveChangesInProfile = true);
    }
}