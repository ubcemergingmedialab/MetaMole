using System.Runtime.InteropServices;

namespace Meta.Tests
{
    /// <summary>
    ///     A class to encapsulate all the function calls to the playbacking back sensor data to the hand kernel. To be
    ///     used to Integration Tests
    /// </summary>
    public static class HandKernelPlayback
    {
    
        /// <summary>   Creates asynchronous playback of sensor data. </summary>
        /// <param name="sensorPlaybackFolder"> Pathname of the sensor playback folder. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
    
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "CreateAsynchronousPlayback")]
        public static extern bool CreateAsynchronousPlayback(string sensorPlaybackFolder,string pluginsFolder);

    
        /// <summary>   Gets number of frames in playback </summary>
        /// <returns>   The number of frames. </returns>
    
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "GetNumberOfFrames")]
        public static extern int GetNumberOfFrames();

        /// <summary>   Updates the playback data. </summary>
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "UpdatePlaybackData")]
        public static extern void UpdatePlaybackData();

        /// <summary>   Stops asynchronous playback. </summary>
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "StopAsynchronousPlayback")]
        public static extern void StopAsynchronousPlayback();
    }
}
