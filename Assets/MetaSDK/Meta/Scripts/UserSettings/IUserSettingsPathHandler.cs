namespace Meta
{
    /// <summary>
    /// Handles paths to UserSettings related files
    /// </summary>
    public interface IUserSettingsPathHandler
    {
        /// <summary>
        /// The complete user settings file path
        /// </summary>
        string DeveloperSettingFilePath { get; }

        /// <summary>
        /// The complete application settings file path
        /// </summary>
        string MetaSettingsFilePath { get; }
    }
}
