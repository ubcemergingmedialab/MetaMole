namespace Meta
{
    /// <summary>
    /// Temporary class to emulate working of the authentication to retreive user settings
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// A key that is unique to the user. There can only be one user assigned to a user key
        /// </summary>
        public readonly string UserKey;
        /// <summary>
        /// A key that is unique to the application. There can only be one application assigned to an application key.
        /// </summary>
        public readonly string ApplicationKey;

        public Credentials(string userKey, string applicationKey)
        {
            this.UserKey = userKey;
            this.ApplicationKey = applicationKey;
        }

        public Credentials()
        {
        }
    }

}