using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Meta
{
    /// <summary>
    /// calibration parameter loader  main interface.
    /// 
    /// A calibration parameter loader must define a 
    /// way to retrieve the CalibrationParameters.
    /// </summary>
    public interface ICalibrationParameterLoader
    {
        Dictionary<string, CalibrationProfile> Load();

    }

}