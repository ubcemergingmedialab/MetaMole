namespace Meta
{
    /// <summary>
    /// The user settings that are exposed to developers. 
    /// </summary>
    public interface IUserSettingsDeveloper
    {

        void RemoveSetting(string key);

        T RemoveSetting<T>(string key);

        void ClearAppSettings();

        bool HasKey(string key);

        T GetSetting<T>(string key);

        bool SetSetting(string key, object value);

        /*Common */

        bool UserLogin(Credentials creds);

        bool UsersSwitch(Credentials creds);

        bool UserLogout();

        bool DeserializePersistentSettings();

        bool SerializePersistentSettings();

    }
}