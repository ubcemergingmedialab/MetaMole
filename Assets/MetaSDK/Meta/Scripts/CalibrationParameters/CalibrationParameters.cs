using UnityEngine;
using System.Collections.Generic;

namespace Meta
{
    internal delegate void OnParametersReady();

    /// <summary>
    /// A class that stores a dict of calibration profiles.
    /// An instance of this class is loaded into the MetaContext module array.
    /// </summary>
    internal class CalibrationParameters : IEventReceiver
    {
        private ICalibrationParameterLoader _loader;


        /// <summary>
        /// Other objects may have events fired off when the calibration parameters are ready.
        /// </summary>
        public OnParametersReady OnParametersReady;

        public CalibrationParameters(ICalibrationParameterLoader loader)
        {
            _loader = loader;
        }

        private Dictionary<string, CalibrationProfile> _profiles = null;

        public Dictionary<string, CalibrationProfile> Profiles
        {
            get { return _profiles; }
        }

        public void Update()
        {
            if (_profiles == null)
            {
                _profiles = _loader.Load();
                if (_profiles != null)
                {
                    if (OnParametersReady != null)
                    {
                        OnParametersReady();
                    }
                }
            }
        }

        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnUpdate(Update);
        }

        public override string ToString()
        {
            string outputStr = base.ToString() + ":\n";
            if (_profiles != null)
            {
                foreach (string s in _profiles.Keys)
                {
                    var profile = _profiles[s];
                    string relativePoses = profile.RelativePose.ToString();
                    outputStr += string.Format("profile: name: {0}, rel:\n [{1}]", s, relativePoses);
                }
            }
            else
            {
                outputStr += "Not loaded";
            }

            return outputStr;
        }


        public static Matrix4x4 MatrixFromArray(double[] vals)
        {
            var poseMat = new Matrix4x4();
            if (vals != null && vals.Length >= 12)
            {
                poseMat.SetRow(0, new Vector4((float) vals[0], (float) vals[1], (float) vals[2], (float) vals[3]));
                poseMat.SetRow(1, new Vector4((float) vals[4], (float) vals[5], (float) vals[6], (float) vals[7]));
                poseMat.SetRow(2, new Vector4((float) vals[8], (float) vals[9], (float) vals[10], (float) vals[11]));
                poseMat.SetRow(3, new Vector4(0, 0, 0, 1));
            }
            else
            {
                Debug.LogError(string.Format("CalibrationParameters.MatrixFromArray: the array '{0}' was insufficient for a matrix4x4.", vals));
            }

            return poseMat;
        }
    }
}