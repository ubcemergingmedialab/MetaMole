using System.Collections;
using UnityEngine;

namespace Meta.SlamUI
{
    /// <summary>
    /// Encapsulate a controller for the Slam UI
    /// </summary>
    public abstract class BaseSlamUI : MonoBehaviour
    {
        /// <summary>
        /// Change the current UI stage based on the calibration process
        /// </summary>
        /// <param name="calibrationStage"></param>
        /// <returns></returns>
        public abstract IEnumerator ChangeUIStage(CalibrationStage calibrationStage);
    }
}