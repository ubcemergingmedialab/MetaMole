
namespace Meta
{
    /// <summary>
    /// Interface for configuring a canvas in front of the stereo cameras
    /// </summary>
    internal interface ISlamTrackingCanvasConfigurer
    {
        /// <summary>
        /// Automatically configure the Canvas attached to this GameObject.
        /// </summary>
        /// <returns>True if configuration was successful, false otherwise</returns>
        bool AutoConfigure();

        /// <summary>
        /// Configures the canvas to render in from of the stereo cameras.
        /// This will attach the canvas to the Event Camera, adjust it's size and relative position.
        /// </summary>
        /// <returns>True if configuration was successful, false otherwise</returns>
        bool Configure();
    }
}
