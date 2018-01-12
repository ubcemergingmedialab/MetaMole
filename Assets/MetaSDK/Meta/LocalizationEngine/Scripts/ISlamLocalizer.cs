using UnityEngine.Events;

namespace Meta
{
    ///<summary>
    /// Interface for module that uses MetaSLAM as a localizer.
    /// </summary>
    public interface ISlamLocalizer
    {
        /// <summary>
        /// Occurs when Slam Mapping is completed.
        /// </summary>
        UnityEvent SlamMappingCompleted { get; }

        /// <summary>
        /// Occurs when Slam Map Loading failed.
        /// </summary>
        UnityEvent SlamMapLoadingFailed { get; }

        /// <summary>
        /// Whether the slam process is finished or not.
        /// </summary>
        bool IsFinished { get; }

        /// <summary>
        /// Enables or disables the start function slam itinialization.
        /// </summary>
        /// <param name="initializeOnStart">Whether slam initalization should be called in the start function.</param>
        void SetInitializeOnStart(bool initializeOnStart);

        /// <summary>
        /// Initializes the slam map creation process.
        /// </summary>
        void CreateSlamMap();

        /// <summary>
        /// Initializes the slam map loading process.
        /// </summary>
        /// <param name="mapName">The slam map name.</param>
        void LoadSlamMap(string mapName);

        /// <summary>
        /// Saves an slam map defined by mapName.
        /// </summary>
        /// <param name="mapName">The slam map name.</param>
        void SaveSlamMap(string mapName);
    }
}
