using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Meta;

namespace Meta
{
    /// <summary>
    /// An implementation of a Calibration Parameter Loader which adds Additional Matrices to the list of profiles.
    /// The values matrices retrieved from the DLL are also re-based. 
    /// </summary>
    public class CalibrationParameterLoaderAdditionalMatrices : CalibrationParameterLoader
    {

        /// <summary>
        /// This key is used to reference a matrix from list of calibration profiles. 
        /// </summary>
        private string _keySelector = "g_pmd_cad";

        /// <summary>
        /// Adds a hard-coded matrix to the calibration profiles, then uses the inverse of the matrix in the calibration profile 
        /// referenced by '_keySelector' as the LHS for matrix-matrix multiplication of all matrices in the list of profiles.
        /// </summary>
        /// <param name="profiles"></param>
        /// <returns></returns>
        private Dictionary<string, CalibrationProfile> AddMatrixAndRebase(Dictionary<string, CalibrationProfile> profiles)
        {
            string profileOut = "";
            foreach (var key in profiles.Keys)
            {
                profileOut += string.Format("{0}\n{1}\n\n", key, profiles[key].RelativePose.ToString());

            }
            // Debug.Log("Before rebase:\n" + profileOut);
        
            //The new set of calibration profiles which will be multiplied by the matrix chosen by the key.
            Dictionary<string, CalibrationProfile> rebasedProfiles = new Dictionary<string, CalibrationProfile>();

            if (profiles.ContainsKey(_keySelector))
            {
                Matrix4x4 lhs = profiles[_keySelector].RelativePose.inverse;
                foreach (var key in profiles.Keys)
                {
                    var rebasedMatrix = lhs *  profiles[key].RelativePose;
                    rebasedProfiles.Add(key, new CalibrationProfile { RelativePose = rebasedMatrix, CameraModel = profiles[key].CameraModel });
                }
                return rebasedProfiles; //The profiles with modified matrices
            }
            
            Debug.LogError("CalibrationParametersAdditionalMatrices.AddMatrixAndRebase: could not find profile referenced by key selector.");
            return profiles; 
        }

        public override Dictionary<string, CalibrationProfile> Load()
        {
            var profiles = base.Load();
            if (profiles == null)
            {
                return null;
            }

            return AddMatrixAndRebase(profiles);
        }
    }

}