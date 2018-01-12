using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Controls the pose of the current GameObject based on Calibration Parameters
    /// </summary>
    public class RdfMatrixToPose : MetaBehaviour
    {
        /// <summary>
        /// The safeguarded data for the pose matrix.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        private Matrix4x4 _poseMatrix;

        /// <summary>
        /// The key used to access a calibration profile
        /// </summary>
        [HideInInspector]
        [SerializeField]
        private string _key;


        /// <summary>
        /// Register when Calibration parameters are ready
        /// </summary>
        private void Start()
        {
            _poseMatrix = Matrix4x4.identity;
            //Get the module from the metaContext
            CalibrationParameters pars = metaContext.Get<CalibrationParameters>();
            if (pars != null) //the metaContext may not have the module if it was not loaded correctly.
            {
                //Will be called when the parameters have been loaded.
                pars.OnParametersReady += CalibratePose;
            }
        }

        /// <summary>
        /// Import the Pose from Calibration Parameters
        /// </summary>
        public void CalibratePose()
        {
            CalibrationParameters pars = metaContext.Get<CalibrationParameters>();
            // Remove from event
            pars.OnParametersReady -= CalibratePose;

            if (pars.Profiles.ContainsKey(_key)) //check if the dict has the key for the calibration you're after.
            {
                CalibrationProfile profile = pars.Profiles[_key]; //get the calibration
                _poseMatrix = profile.RelativePose;
                UpdatePose();
            }
        }

        /// <summary>
        /// Reset the Matrix
        /// </summary>
        public void Reset()
        {
            _poseMatrix = Matrix4x4.identity;
            UpdatePose();
        }

        /// <summary>
        /// Updates the pose of this GameObject
        /// </summary>
        public void UpdatePose()
        {
            // Get translation.
            Vector3 translation = ExtractTranslationFromCvMatrix(ref _poseMatrix);
            transform.localPosition = translation;

            // Get rotation.
            Quaternion rotation = ExtractRotationFromCvMatrix(ref _poseMatrix);
            transform.localRotation = rotation;
        }

        /// <summary>
        /// The write-protected PoseMatrix member variable.
        /// </summary>
        public Matrix4x4 PoseMatrix
        {
            get { return _poseMatrix; }
        }

        #region Static Functions
        // Cv matrix means that the rows are:
        // x = Right
        // y = Down
        // z = Forward
        public static Vector3 ExtractTranslationFromCvMatrix(ref Matrix4x4 matrix)
        {
            Vector3 translate;
            translate.x = matrix.m03;
            translate.y = -matrix.m13;
            translate.z = matrix.m23;
            return translate;
        }

        // Cv matrix means that the rows are:
        // x = Right
        // y = Down
        // z = Forward
        //
        // Signs applied on the original matrix:
        // [ 1   -1   1 ;
        //  -1    1  -1 ;
        //   1   -1   1   ]
        public static Quaternion ExtractRotationFromCvMatrix(ref Matrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = -matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = -matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = -matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }
        #endregion
    }
}