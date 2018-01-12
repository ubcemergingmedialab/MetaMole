using System.Collections.Generic;
using System.IO;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Verifies if an environment profile is valid, checking if their resources excist.
    /// </summary>
    public class EnvironmentProfileVerifier : IEnvironmentProfileVerifier
    {
        /// <summary>
        /// Whether the environment profile is valid or not.
        /// </summary>
        /// <param name="environmentProfile"></param>
        /// <returns><c>true</c> if the environment profile is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid(IEnvironmentProfile environmentProfile)
        {
            return AreMeshesValid(environmentProfile.Meshes) && IsFileValid(environmentProfile.MapName + ".mmf");
        }

        private bool IsFileValid(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            
            return File.Exists(fileName);
        }

        private bool AreMeshesValid(List<string> meshes)
        {
            if (meshes == null)
            {
                return false;
            }

            for (int i = 0; i < meshes.Count; i++)
            {
                if (!IsFileValid(meshes[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
