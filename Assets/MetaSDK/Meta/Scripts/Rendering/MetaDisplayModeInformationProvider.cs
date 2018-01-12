using Meta.DisplayMode.DirectMode;
using Meta.DevMode;

namespace Meta.DisplayMode
{
    /// <summary>
    /// Holds the Display information for Meta2
    /// </summary>
    public class MetaDisplayModeInformationProvider : IMetaDisplayModeInformationProvider
    {
        private MetaDisplayMode _currentMode = MetaDisplayMode.None;
        private IMetaDisplayInfo _metaDisplayInfo;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public MetaDisplayModeInformationProvider()
        {
            UpdateCurrentDisplayStatus();
        }

        /// <summary>
        /// Update the current display status
        /// </summary>
        private void UpdateCurrentDisplayStatus()
        {
            // Check Extended Mode
            var extendedModeInfo = new MetaDisplayInformationProvider();
            _metaDisplayInfo = extendedModeInfo.GetMetaDisplayInformation();
            if (_metaDisplayInfo != null)
            {
                _currentMode = MetaDisplayMode.ExtendedMode;
                return;
            }

            // Check Direct Mode
            var dmDisplayCount = MetaDirectModeInformation.GetNumberOfConnectedDisplays();
            if (dmDisplayCount > 0)
            {
                _currentMode = MetaDisplayMode.DirectMode;
                return;
            }

            // TODO: Check Direct To Display Mode
        }

        /// <summary>
        /// Gets the information of Meta2 Display if available, otherwise returns null.
        /// </summary>
        public IMetaDisplayInfo MetaDisplayInformation
        {
            get { return _metaDisplayInfo; }
        }

        /// <summary>
        /// Gets the current Display Mode for Meta2
        /// </summary>
        public MetaDisplayMode CurrentDisplayMode
        {
            get { return _currentMode; }
        }
    }
}
