using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Environment profile that has information to localize the environment resources.
    /// </summary>
    public class EnvironmentProfile : IEnvironmentProfile
    {
        [JsonProperty(PropertyName = "id")]
        private int _id;
        [JsonProperty(PropertyName = "name")]
        private string _name;
        [JsonProperty(PropertyName = "meshes")]
        private List<string> _meshes;
        [JsonProperty(PropertyName = "map_name")]
        private string _mapName;
        [JsonProperty(PropertyName = "last_time_used")]
        private long _lastTimeUsed;

        /// <summary>
        /// Gets the environment profile id.
        /// </summary>
        [JsonIgnore]
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the environment profile name.
        /// </summary>
        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the environment profile meshes.
        /// </summary>
        [JsonIgnore]
        public List<string> Meshes
        {
            get { return _meshes; }
            set { _meshes = value == null ? new List<string>() : value; }
        }

        /// <summary>
        /// Gets or sets the environment profile map name.
        /// </summary>
        [JsonIgnore]
        public string MapName
        {
            get { return _mapName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("MapName");
                }
                _mapName = value;
            }
        }

        /// <summary>
        /// Gets or sets the environment profile last time used timestamp.
        /// </summary>
        [JsonIgnore]
        public long LastTimeUsed
        {
            get { return _lastTimeUsed; }
            set { _lastTimeUsed = value; }
        }

        /// <summary>
        /// Whether the environment profile is default or not.
        /// </summary>
        [JsonIgnore]
        public bool IsDefault
        {
            get { return _name == EnvironmentConstants.DefaultEnvironmentName; }
        }

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentProfile"/> class.
        /// </summary>
        /// <param name="id">The environment profile id</param>
        /// <param name="name">The environment profile name</param>
        public EnvironmentProfile(int id, string name)
        {
            _id = id;
            Name = name;
            Meshes = null;
            UpdateLastTimeUsed();
        }
        
        /// <summary>
        /// Sets the current time as the last time this environment was used.
        /// </summary>
        public void UpdateLastTimeUsed()
        {
            LastTimeUsed = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}