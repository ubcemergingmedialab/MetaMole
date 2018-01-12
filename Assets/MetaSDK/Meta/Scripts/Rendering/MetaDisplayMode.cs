
namespace Meta.DisplayMode
{
    /// <summary>
    /// Represents the current Display mode
    /// </summary>
    public enum MetaDisplayMode
    {
        /// <summary>
        /// No Meta connected display
        /// </summary>
        None,

        /// <summary>
        /// Display is working as another Monitor
        /// </summary>
        ExtendedMode,

        /// <summary>
        /// NVidia's Direct Display Technology
        /// </summary>
        DirectMode,

        /// <summary>
        /// AMD's Direct Display Technology
        /// </summary>
        DirectToDisplay
    }
}
