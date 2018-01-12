namespace Meta.Reconstruction
{
    /// <summary>
    /// Parser to serialize/deserialize the environment profiles.
    /// </summary>
    public interface IEnvironmentProfileParser
    {
        /// <summary>
        /// Deserializes the content into a dictionary of environment profiles.
        /// </summary>
        /// <param name="data">The environment profiles data.</param>
        /// <returns>The deserialized dictionary of environment profiles</returns>
        EnvironmentProfileCollection DeserializeEnvironmentProfiles(string data);

        /// <summary>
        /// Serializes the dictionary of environments profiles.
        /// </summary>
        /// <param name="environmentProfiles"></param>
        /// <returns>The serialized environments profiles</returns>
        string SerializeEnvironmentProfiles(EnvironmentProfileCollection environmentProfiles);
    }
}
