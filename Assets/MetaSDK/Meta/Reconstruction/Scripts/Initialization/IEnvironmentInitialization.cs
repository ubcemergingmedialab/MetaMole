namespace Meta.Reconstruction
{
    /// <summary>
    /// The environment initialization process.
    /// </summary>
    public interface IEnvironmentInitialization
    {
        /// <summary>
        /// Starts the environment initialization process.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Stops the environment initialization process.
        /// </summary>
        void Stop();
    }
}