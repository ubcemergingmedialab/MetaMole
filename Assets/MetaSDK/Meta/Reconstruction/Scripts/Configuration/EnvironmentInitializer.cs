using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Triggers the environment initialization.
    /// </summary>
    internal class EnvironmentInitializer : IEnvironmentInitializer
    {
        private IEnvironmentReset _environmentReset;
        private IEnvironmentInitialization _environmentInitialization;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentInitializer"/> class.
        /// </summary>
        /// <param name="environmentInitialization">The object to initialize an environment.</param>
        /// <param name="environmentReset">The object to reset the current environment.</param>
        /// <param name="metaLocalization">The object that handles the world localization.</param>
        public EnvironmentInitializer(IEnvironmentInitialization environmentInitialization, IEnvironmentReset environmentReset, MetaLocalization metaLocalization)
        {
            if (environmentInitialization == null)
            {
                throw new ArgumentNullException("environmentInitialization");
            }

            if (environmentReset == null)
            {
                throw new ArgumentNullException("environmentReset");
            }

            if (metaLocalization == null)
            {
                throw new ArgumentNullException("metaLocalization");
            }

            _environmentInitialization = environmentInitialization;
            _environmentReset = environmentReset;
            metaLocalization.LocalizationReset.AddListener(Reset);
        }

        /// <summary>
        /// Triggers the environment initialization process.
        /// </summary>
        public void Start()
        {
            _environmentInitialization.Initialize();
        }

        private void Reset()
        {
            _environmentInitialization.Stop();
            _environmentReset.Reset();
            _environmentInitialization.Initialize();
        }
    }
}