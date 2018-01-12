namespace Meta.Reconstruction
{
    /// <summary>
    /// Verifies if an environment profile is valid.
    /// </summary>
    public interface IEnvironmentProfileVerifier
    {
        /// <summary>
        /// Whether the environment profile is valid or not.
        /// </summary>
        /// <param name="environmentProfile"></param>
        /// <returns><c>true</c> if the environment profile is valid; otherwise, <c>false</c>.</returns>
        bool IsValid(IEnvironmentProfile environmentProfile);
    }
}
