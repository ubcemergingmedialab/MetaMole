using System.Collections.Generic;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Environment profile that has information to localize the environment resources.
    /// </summary>
    public interface IEnvironmentProfile
    {
        /// <summary>
        /// Gets the environment profile id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the environment profile name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the environment profile meshes.
        /// </summary>
        List<string> Meshes { get; }

        /// <summary>
        /// Gets the environment profile map name.
        /// </summary>
        string MapName { get; }

        /// <summary>
        /// Gets the environment profile last time used timestamp.
        /// </summary>
        long LastTimeUsed { get; }

        /// <summary>
        /// Whether the environment profile is default or not.
        /// </summary>
        bool IsDefault { get; }
    }
}