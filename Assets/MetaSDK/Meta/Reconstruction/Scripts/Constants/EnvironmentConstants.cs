namespace Meta.Reconstruction
{
    /// <summary>
    /// Stores constants values and deifinitions for the environment intialization and recpnstruction.
    /// </summary>
    public static class EnvironmentConstants
    {
        /// <summary>
        /// Name of a default environment.
        /// </summary>
        public const string DefaultEnvironmentName = "Default";

        /// <summary>
        /// Folder where environment profiles should be saved.
        /// </summary>
        public const string EnvironmentFolderName = "Environments";

        /// <summary>
        /// Maximum amount of triangles contained in a mesh before splitting.
        /// </summary>
        public const int MaxTriangles = 10000;
    }
}