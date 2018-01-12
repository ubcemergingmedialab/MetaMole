namespace Meta
{
    /// <summary>
    /// This is a workaround for not yet using the automatically generated interop.  This
    /// is a manual copy of all possible supported sensor types in Meta2.
    /// </summary>
    internal enum SensorType
    {
        UNKNOWN = -1,
        Group = 0,
        Depth = 1,
        Color = 2,
        Monochrome = 3,
        IMU = 4,
        Audio = 5,
        Button = 6
    };
}