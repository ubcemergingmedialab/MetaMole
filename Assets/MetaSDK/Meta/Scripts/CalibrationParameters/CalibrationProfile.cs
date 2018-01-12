using UnityEngine;

namespace Meta
{
    /// <summary>
    /// A container for tuples of matrices. 
    /// The matrices are represented as one dimensional arrays
    /// </summary>
    public struct CalibrationProfile
    {
        public Matrix4x4 RelativePose;
        public double[] CameraModel;
    }


}