using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Collection of environment profiles.
    /// </summary>
    public class EnvironmentProfileCollection
    {
        [JsonProperty(PropertyName = "collection")]
        private Dictionary<int, EnvironmentProfile> _collection;

        /// <summary>
        /// Gets the number of environment profiles.
        /// </summary>
        [JsonIgnore]
        public int Count
        {
            get { return _collection.Count; }
        }

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentProfileRepository"/> class with an empty collection.
        /// </summary>
        public EnvironmentProfileCollection()
        {
            _collection = new Dictionary<int, EnvironmentProfile>();
        }

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentProfileRepository"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public EnvironmentProfileCollection(Dictionary<int, EnvironmentProfile> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            _collection = collection;
        }

        /// <summary>
        /// Gets the list of environment profiles, sorted by the last time they were used.
        /// </summary>
        /// <returns>The list of all environment profiles.</returns>
        public List<IEnvironmentProfile> GetAll()
        {
            List<IEnvironmentProfile> environments = new List<IEnvironmentProfile>();

            foreach (var environmentProfileItem in _collection)
            {
                environments.Add(environmentProfileItem.Value);
            }
            return environments;
        }

        /// <summary>
        /// Gets all the current environment profile ids.
        /// </summary>
        /// <returns>The list of all current ids.</returns>
        public List<int> GetAllIds()
        {
            return new List<int>(_collection.Keys);
        }

        /// <summary>
        /// Adds an environment profile.
        /// </summary>
        /// <param name="environmentProfile">The environment profile to be added.</param>
        public void Add(EnvironmentProfile environmentProfile)
        {
            _collection.Add(environmentProfile.Id, environmentProfile);
        }

        /// <summary>
        /// Removes the environment profile of the given id, from the collection.
        /// </summary>
        /// <param name="id">The id of the environment to be removed.</param>
        public void Remove(int id)
        {
            _collection.Remove(id);
        }
        
        /// <summary>
        /// Gets the environment of the given id.
        /// </summary>
        /// <param name="id">The environment id.</param>
        /// <returns>The environment of the given id.</returns>
        public EnvironmentProfile GetById(int id)
        {
            EnvironmentProfile profile = TryGetById(id);
            if (profile != null)
            {
                return profile;
            }
            throw new KeyNotFoundException(string.Format("Environment profile of id {0} was not found", id));
        }

        /// <summary>
        /// Tries to get the environment of the given id.
        /// </summary>
        /// <param name="id">The environment id.</param>
        /// <returns>The environment of the given id.</returns>
        public EnvironmentProfile TryGetById(int id)
        {
            EnvironmentProfile environmentProfile;
            if (_collection.TryGetValue(id, out environmentProfile))
            {
                return environmentProfile;
            }
            return null;
        }
    }
}