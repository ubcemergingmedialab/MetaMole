namespace Meta
{
    /// <summary>
    /// Access to settings which are kept across applications but
    /// should be restricted.
    /// </summary>
    internal interface IUserSettingsMeta
    {

        void RemoveSetting(MetaConfiguration config, int index);

        T RemoveSetting<T>(MetaConfiguration config, int index);

        void ClearMetaSettings();

        bool HasKey(MetaConfiguration config, int index);

        T GetSetting<T>(MetaConfiguration config, int index);

        bool SetSetting(MetaConfiguration config, int index, object value);

        /*Common */

        bool UserLogin(Credentials creds);

        bool UsersSwitch(Credentials creds);

        bool UserLogout();

        bool DeserializePersistentSettings();

        bool SerializePersistentSettings();
    }
}
