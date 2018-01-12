namespace Meta
{
    /// <summary>
    /// A flavour of the UserSettings which uses the event receiver to serialize user settings.
    /// </summary>
    internal class EventReceivingUserSettings : UserSettings, IEventReceiver
    {
        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnDestroy(OnDestroy);
        }

        private void OnDestroy()
        {
            SerializePersistentSettings();
        }

        public EventReceivingUserSettings(Credentials creds) : base(creds)
        {
        }
    }

}
