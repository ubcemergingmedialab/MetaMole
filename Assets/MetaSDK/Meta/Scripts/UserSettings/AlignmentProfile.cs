using UnityEngine;
using System.Collections;
using System.IO;

namespace Meta
{
    /// <summary>
    /// An object which collects Alignment profile information. 
    /// One alignment profile contains the positions for eyes (from eye-alignment)
    /// and the paths to the related maps. 
    /// </summary>
    public class AlignmentProfile
    {
        /// <summary>
        /// The position of the left eye
        /// </summary>
        public Vector3 EyePositionLeft;

        /// <summary>
        /// The position of the right eye
        /// </summary>
        public Vector3 EyePositionRight;

        /// <summary>
        /// The path to the distortion map for the left eye.
        /// </summary>
        public string DistortionMapPathLeft;

        /// <summary>
        /// The path to the distortion map for the right eye.
        /// </summary>
        public string DistortionMapPathRight;

        /// <summary>
        /// The path to the mask map for the left eye.
        /// </summary>
        public string MaskMapPathLeft;

        /// <summary>
        /// The path to the mask map for the right eye.
        /// </summary>
        public string MaskMapPathRight;

        /// <summary>
        /// Determine if the profile is valid or if its paths are valid.
        /// </summary>
        /// <param name="mapsDir">The base directory for the maps</param>
        /// <returns>true if all the maps exist in the directory</returns>
        public bool ProfileMapPathsValid(string mapsDir)
        {
            string[] filenames = new[]
            {
                    MaskMapPathLeft,
                    MaskMapPathRight,
                    DistortionMapPathLeft,
                    DistortionMapPathRight
            };

            foreach (var path in filenames)
            {
                if (!File.Exists(mapsDir + path))
                {
                    Debug.LogError("Could not find map image file that was referenced in the calibration profile.");
                    return false;
                }
            }

            return true;
        }
    }
}
