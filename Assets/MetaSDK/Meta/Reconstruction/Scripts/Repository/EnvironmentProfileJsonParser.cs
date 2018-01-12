using System;
using Newtonsoft.Json;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Json parser to serialize/deserialize the environment profiles.
    /// </summary>
    public class EnvironmentProfileJsonParser : IEnvironmentProfileParser
    {
        private static JsonSerializerSettings _jsonDeserializingSettings = new JsonSerializerSettings {MissingMemberHandling = MissingMemberHandling.Error};

        /// <summary>
        /// Deserializes the content into a dictionary of environment profiles.
        /// </summary>
        /// <param name="data">The environment profiles data.</param>
        /// <returns>The deserialized dictionary of environment profiles</returns>
        public EnvironmentProfileCollection DeserializeEnvironmentProfiles(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }
            EnvironmentProfileCollection jsonDictionary = JsonConvert.DeserializeObject<EnvironmentProfileCollection>(data, _jsonDeserializingSettings);
            return jsonDictionary;
        }

        /// <summary>
        /// Serializes the dictionary of environments profiles.
        /// </summary>
        /// <param name="environmentProfiles"></param>
        /// <returns>The serialized environments profiles</returns>
        public string SerializeEnvironmentProfiles(EnvironmentProfileCollection environmentProfiles)
        {
            if (environmentProfiles == null)
            {
                throw new ArgumentNullException("environmentProfiles");
            }
            return JsonConvert.SerializeObject(environmentProfiles, Formatting.Indented);
        }
    }
}
