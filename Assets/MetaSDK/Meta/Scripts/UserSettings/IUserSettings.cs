namespace Meta
{
    /// <summary>
    /// The complete UserSettings interface. Control over access to certain classes of settings
    /// may be achieved by providing the user with access to one of the interfaces.
    /// </summary>
    internal interface IUserSettings : IUserSettingsDeveloper, IUserSettingsMeta { }
}
