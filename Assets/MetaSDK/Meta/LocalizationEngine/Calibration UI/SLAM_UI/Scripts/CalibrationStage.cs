namespace Meta.SlamUI
{
    /// <summary>
    /// Stages of the entire calibration process
    /// </summary>
    public enum CalibrationStage
    {
        WaitingForSensors,
        WaitingForTracking,
        Mapping,
        Remapping,
        HoldStill,
        Completed,
        Fail
    }
}