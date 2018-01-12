namespace Meta.SlamUI
{
    /// <summary>
    /// Message types to represent slam states in UI.
    /// </summary>
    public enum SLAMUIMessageType
    {
        None,
        WaitingForSensors,
        WaitingForTracking,
        TurnAround,
        HoldStill,
        MappingSuccess,
        MappingFail,
        Relocalization,
        ReconstructionInstructions
    }
}