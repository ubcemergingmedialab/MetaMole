using Meta.DevMode;

namespace Meta.DisplayMode
{
    /// <summary>
    /// Interface to access the current Display information for Meta2
    /// </summary>
    public interface IMetaDisplayModeInformationProvider
    {
        /// <summary>
        /// Gets the information of Meta2 Display if available, otherwise returns null.
        /// </summary>
        IMetaDisplayInfo MetaDisplayInformation
        {
            get;
        }

        /// <summary>
        /// Gets the current Display Mode for Meta2
        /// </summary>
        MetaDisplayMode CurrentDisplayMode
        {
            get;
        }
    }
}
