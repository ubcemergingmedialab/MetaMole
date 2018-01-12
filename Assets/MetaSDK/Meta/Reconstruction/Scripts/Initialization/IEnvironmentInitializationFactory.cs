namespace Meta.Reconstruction
{
    /// <summary>
    /// Initializez an environment.
    /// </summary>
    public interface IEnvironmentInitializationFactory
    {
        /// <summary>
        /// Creates an <see cref="IEnvironmentInitializer"/> for the given environment selection result.
        /// </summary>
        /// <param name="environmentInitializaterType">The environment initializer type.</param>
        /// <returns>The environment initializer for the given environment initializer type.</returns>
        IEnvironmentInitialization CreateEnvironmentInitialization(EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType environmentSelectionResult);
    }
}
