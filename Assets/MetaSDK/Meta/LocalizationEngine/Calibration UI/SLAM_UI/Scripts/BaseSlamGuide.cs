namespace Meta.SlamUI
{
    /// <summary>
    /// Base for the user's steps guide for the SLAM calibration
    /// </summary>
    public abstract class BaseSlamGuide : MetaBehaviour
    {
        /// <summary>
        /// Start tracking users steps to guide calibration
        /// </summary>
        /// <param name="calibrationStage"></param>
        public abstract void StartTrackCalibrationSteps(SlamInitializationType calibrationStage);
    }
}