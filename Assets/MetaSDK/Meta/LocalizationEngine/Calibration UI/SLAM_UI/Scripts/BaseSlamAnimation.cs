using UnityEngine;

namespace Meta.SlamUI
{
    /// <summary>
    /// Encapsulate animation states controller for the SLAM UI
    /// </summary>
    public abstract class BaseSlamAnimation : MonoBehaviour
    {
        /// <summary>
        /// Start the UI animation
        /// </summary>
        public abstract void StartAnimation();

        /// <summary>
        /// Stop the UI animation
        /// </summary>
        public abstract void StopAnimation();

        /// <summary>
        /// Play animation track related to the current calibration stage
        /// </summary>
        /// <param name="calibrationStage"></param>
        public abstract void PlayAnimation(CalibrationStage calibrationStage);
    }
}