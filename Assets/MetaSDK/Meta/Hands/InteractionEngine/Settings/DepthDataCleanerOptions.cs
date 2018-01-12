using System.Runtime.InteropServices;

/// <summary>   A depth data cleaner options. </summary>
[StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
internal struct DepthDataCleanerOptions
{
    /// <summary>   The minimum cut off depth. </summary>
    public int minimumDepth;
    /// <summary>   The maximum cut off depth. </summary>
    public int maximumDepth;
    /// <summary>   The minimum cut off confidence. </summary>
    public int minimumConfidence;
    /// <summary>   The maximum cut off noise. </summary>
    public float maximumNoise;
    /// <summary>   Size of the median filter. </summary>
    public int medianFilterSize;
    /// <summary>   Size of the morphological filter. </summary>
    public int morphologicalFilterSize;
    /// <summary>   The morpholical iteration. </summary>
    public int morpholicalIteration;
    /// <summary>   The minimum ir distance. </summary>
    public int minimumIRDistance;
    /// <summary>   The minimum ir cutoff. </summary>
    public int minimumIRCutoff;
    /// <summary>   true to debug display. </summary>
    public bool debugDisplay;
    /// <summary>   true to camera rotated 180. </summary>
    public bool cameraRotated180;
};