namespace Meta
{
    /// <summary>
    /// MonoBehaviour containing the current MetaContext
    /// </summary>
    public class MetaContextBridge : BaseMetaContextBridge
    {
        private IMetaContextInternal _context;

        /// <summary>
        /// Internal set for the current Meta Context
        /// </summary>
        /// <param name="context">Current Meta Context</param>
        internal void SetContext(IMetaContextInternal context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the Meta Context by Type.
        /// This is useful when asking for Internal or Public interfaces
        /// </summary>
        /// <typeparam name="T">Type of IMetaContext</typeparam>
        /// <returns>Interface of MetaContext</returns>
        public override T GetContext<T>()
        {
            return (T)_context;
        }

        /// <summary>
        /// Get the current Meta Context for internal purposes
        /// </summary>
        internal override IMetaContextInternal CurrentContextInternal
        {
            get { return _context; }
        }

        /// <summary>
        /// Get the current Meta Context
        /// </summary>
        public override IMetaContext CurrentContext
        {
            get { return _context; }
        }
    }
}
