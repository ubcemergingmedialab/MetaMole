namespace Meta.Reconstruction
{
    /// <summary>
    /// Reads/Writes the environment profiles data.
    /// </summary>
    public interface IEnvironmentProfileIOStream
    {
        /// <summary>
        /// Reads the environment profiles data.
        /// </summary>
        /// <returns>The environment profiles data</returns>
        string Read();

        /// <summary>
        /// Writes the environment profiles data.
        /// </summary>
        void Write(string content);
    }
}
