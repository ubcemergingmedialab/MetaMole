using System;
using System.Runtime.InteropServices;
using Meta;
using Meta.Internal;

/// <summary>   A class to encapsulate all the function calls to the hand kernel. /// </summary>
public static class HandKernelInterop
{
    //todo: Change from Event + GetData format to Event HAndler passing the new Data (should be less cofusing that way)

    /// <summary>   Handler, called when the new data. </summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void NewDataHandler();

    /// <summary>   Destroys the hand consumer. </summary>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "DestroyHandConsumer")]
    public static extern void DestroyHandConsumer();

    /// <summary>   Builds hand consumer. </summary>
    /// <param name="handConsumerType"> Type of the hand consumer. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "BuildHandConsumer")]
    internal static extern bool BuildHandConsumer(string handConsumerType);

    /// <summary>   Sets depth data cleaner options. </summary>
    /// <param name="depthDataCleanerOptions">  Options for controlling the depth data cleaner. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "SetDepthDataCleanerOptions")]
    internal static extern bool SetDepthDataCleanerOptions(ref DepthDataCleanerOptions depthDataCleanerOptions);

    /// <summary>   Sets point cloud generator options./ </summary>
    /// <param name="cloudGeneratorOptions">    Options for controlling the cloud generator. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "SetPointCloudGeneratorOptions")]
    internal static extern bool SetPointCloudGeneratorOptions(ref CloudGeneratorOptions cloudGeneratorOptions);

    /// <summary>   Sets hand processor options. </summary>
    /// <param name="handProcessorOptions"> Options for controlling the hand processor. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "SetHandProcessorOptions")]
    internal static extern bool SetHandProcessorOptions(ref HandProcessorOptions handProcessorOptions);

    /// <summary>   Gets point cloud meta data. </summary>
    /// <param name="pointCloudMetaData">   Information describing the point cloud meta. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "GetPointCloudMetaData")]
    internal static extern bool GetPointCloudMetaData(ref PointCloudInteropMetaData pointCloudMetaData);

    /// <summary>   Gets point cloud data. </summary>
    /// <param name="pointCloudInteropData">    Information describing the point cloud interop. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "GetPointCloudData")]
    internal static extern bool GetPointCloudData(ref PointCloudInteropData pointCloudInteropData);

    /// <summary>
    ///     Registers the new point cloud data event handler described by newPointCloudDataCallback.
    /// </summary>
    /// <param name="newPointCloudDataCallback">    The new point cloud data callback. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "RegisterNewPointCloudDataEventHandler")]
    internal static extern bool RegisterNewPointCloudDataEventHandler(NewDataHandler newPointCloudDataCallback);

    /// <summary>
    ///     Registers the new hand data event handler described by newHanddataCallback./
    /// </summary>
    /// <param name="newHanddataCallback">  The new handdata callback. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "RegisterNewHandDataEventHandler")]
    internal static extern bool RegisterNewHandDataEventHandler(NewDataHandler newHanddataCallback);

    /// <summary>   Gets sensor meta data. </summary>
    /// <param name="sensorMetaData">   Information describing the sensor meta. </param>
    /// <returns>   true if it succeeds, false if it fails. </returns>
    [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "GetSensorMetaData")]
    internal static extern bool GetSensorMetaData(ref SensorMetaData sensorMetaData);

    /// <summary>   Sets time from unity. </summary>
    /// <param name="t">    The float to process. </param>
    [DllImport("MetaUnityDepthVisualizer", EntryPoint = "SetTimeFromUnity")]
    internal static extern void SetTimeFromUnity(float t);

    /// <summary>   Gets render event function. </summary>
    /// <returns>   The render event function. </returns>
    [DllImport("MetaUnityDepthVisualizer", EntryPoint = "GetRenderEventFunc")]
    internal static extern IntPtr GetRenderEventFunc();

    // We'll also pass native pointer to a texture in Unity.
    // The plugin will fill texture data from native code.s
    [DllImport("MetaUnityDepthVisualizer", EntryPoint = "SetTextureFromUnity")]
    internal static extern void SetTextureFromUnity(IntPtr texture, int height, int width);
}
