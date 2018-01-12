using UnityEngine;

namespace Meta
{
    /// This script is used to emulate calibrated cameras in Unity. It uses camera intrinsics to 
    /// create an "opengl-style" projection matrix for a rendering camera.
    /// 
    /// *** Attach this script to a gameobject that has a camera component.
    ///
    /// See equation used for projection matrix below:
    /// 
    /// [2*K00/width,  -2*K01/width,   (width - 2*K02 + 2*x0)/width,                            0]
    /// [          0, -2*K11/height, (height - 2*K12 + 2*y0)/height,                            0]
    /// [          0,             0, (-zfar - znear)/(zfar - znear), -2*zfar*znear/(zfar - znear)]
    /// [          0,             0,                             -1,                            0]
    public class CameraSimulator : MetaBehaviour
    {

        /// <summary>
        /// The safeguarded data for the camera model. Currently the interpretation of this array is
        /// [fx, fy, cx, cy, k1, k2, k3], where ki are radial distortion coefficients (symmetric).
        /// This script only uses fx, fy, cx, cy to simulate the rectilinear part of the
        /// camera (omitting the distortion).
        /// </summary>
        [SerializeField]
        private double[] _cameraModel = null;

        /// <summary>
        /// The key used to access a calibration profile
        /// </summary>
        [SerializeField]
        private string _key = "rgb";

        /// <summary>
        /// Zoom factor (can be greater or smaller than 1).
        /// </summary>
        [SerializeField]
        private double _zoom = 1.0;

        /// <summary>
        /// The x resolution of the simulated imager (in pixels).
        /// </summary>
        [SerializeField]
        private double _xResolution = 1280.0;

        /// <summary>
        /// The y resolution of the simulated imager (in pixels).
        /// </summary>
        [SerializeField]
        private double _yResolution = 720.0;

        /// <summary>
        /// Near clipping plane.
        /// </summary>
        [SerializeField]
        private double _zNear = 0.01;

        /// <summary>
        /// Far clipping plane.
        /// </summary>
        [SerializeField]
        private double _zFar = 1000.0;

        [SerializeField]
        private double _fX = 639.0, _fY = 639.0, _cX = 640.0, _cY = 360.0;

        /// <summary>
        /// Custom view-projection matrix for the camera based on the physical camera calibration.
        /// </summary>
        private Matrix4x4 _openGLMatrix;

        /// <summary>
        /// Default Matrix 4x4
        /// </summary>
        private Matrix4x4 _defaultValues;

        /// <summary>
        /// Reference to the camera use to draw the webcam POV.
        /// </summary>
        private Camera _cameraRef;

        /// <summary>
        /// Accesses the context and registers the parameter fetching delegate.
        /// </summary>
        private void Start()
        {
            SetOpenGLMatrix();

            //Get the module from the metaContext
            CalibrationParameters pars = metaContext.Get<CalibrationParameters>();
            if (pars != null) //the metaContext may not have the module if it was not loaded correctly.
            {
                //Will be called when the parameters have been loaded.
                pars.OnParametersReady += ImportCameraModel;
            }
        }

        private void OnEnable()
        {
            _cameraRef = GetComponent<Camera>();
            _defaultValues = _cameraRef.projectionMatrix;
        }

        private void OnDisable()
        {
            _cameraRef.projectionMatrix = _defaultValues;
        }

        private void ImportCameraModel()
        {
            CalibrationParameters pars = metaContext.Get<CalibrationParameters>();
            if (pars.Profiles.ContainsKey(_key)) //check if the dict has the key for the calibration you're after.
            {
                CalibrationProfile profile = pars.Profiles[_key]; //get the calibration
                _cameraModel = profile.CameraModel;
                UpdateParameters();
            }
        }

        private bool UpdateParameters()
        {
            if (_cameraModel.Length >= 4)
            {
                _fX = _cameraModel[0];
                _fY = _cameraModel[1];
                _cX = _cameraModel[2];
                _cY = _cameraModel[3];

                SetOpenGLMatrix();

                return true;
            }
            return false;
        }

        private void Update()
        {
            // TODO: Move this out of the Update Method since the matrix value change only once or twice in the lifetime of this script.
            _cameraRef.projectionMatrix = _openGLMatrix;
        }

        private void SetOpenGLMatrix()
        {
            _openGLMatrix = CalculateOpenGLMatrixFromIntrinsics(
                (float)(_fX * _zoom), (float)(-_fY * _zoom),
                (float)_cX, (float)_cY,
                (float)_xResolution, (float)_yResolution,
                (float)_zFar, (float)_zNear);
        }

        // The fx, fy, tx, ty are assumed to be in pixel units.
        public Matrix4x4 CalculateOpenGLMatrixFromIntrinsics(float fx, float fy, float tx, float ty, float width, float height, float zfar, float znear)
        {
            // x 0 a 0
            // 0 y b 0
            // 0 0 c d
            // 0 0 e 0

            float x = 2.0f * fx / width;
            float y = 2.0f * fy / height;
            float a = (width - 2.0f * tx) / width;
            float b = (height - 2.0f * ty) / height;
            float c = -(zfar + znear) / (zfar - znear);
            float d = -(2.0f * zfar * znear) / (zfar - znear);  // if far is too big, then: -(2.0 * near)
            float e = -1.0f;

            Matrix4x4 m = new Matrix4x4();
            m[0, 0] = x;
            m[0, 1] = 0;
            m[0, 2] = a;
            m[0, 3] = 0;

            m[1, 0] = 0;
            m[1, 1] = y;
            m[1, 2] = b;
            m[1, 3] = 0;

            m[2, 0] = 0;
            m[2, 1] = 0;
            m[2, 2] = c;
            m[2, 3] = d;

            m[3, 0] = 0;
            m[3, 1] = 0;
            m[3, 2] = e;
            m[3, 3] = 0;

            return m;
        }

    }

}