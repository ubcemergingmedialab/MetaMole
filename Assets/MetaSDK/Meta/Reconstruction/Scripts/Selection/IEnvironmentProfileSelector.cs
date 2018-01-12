namespace Meta.Reconstruction
{
    /// <summary>
    /// Selects an environment profile.
    /// </summary>
    public interface IEnvironmentProfileSelector
    {
        /// <summary>
        /// Occurs when an environment profile is selected.
        /// </summary>
        EnvironmentSelectionResultTypeEvent EnvironmentSelected { get; }

        /// <summary>
        /// Selects an environment profile.
        /// </summary>
        void Select();

        /// <summary>
        /// Resets the environment profile selection.
        /// </summary>
        void Reset();
    }
}