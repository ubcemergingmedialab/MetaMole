using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Triggers the environment initialization using environment profiles.
    /// </summary>
    internal class EnvironmentProfileInitializer : IEnvironmentInitializer
    {
        private IEnvironmentProfileSelector _environmentProfileSelector;
        private IEnvironmentInitializationFactory _environmentInitializationFactory;
        private IEnvironmentReset _environmentReset;
        private IEnvironmentInitialization _environmentInitialization;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentProfileInitializer"/> class.
        /// </summary>
        /// <param name="environmentProfileSelector">The object to select an environment profile.</param>
        /// <param name="environmentInitializationFactory">The factory to create an environment initialization object.</param>
        /// <param name="environmentReset">The object to reset the current environment.</param>
        /// <param name="metaLocalization">The object that handles the world localization.</param>
        public EnvironmentProfileInitializer(IEnvironmentProfileSelector environmentProfileSelector, IEnvironmentInitializationFactory environmentInitializationFactory, IEnvironmentReset environmentReset, MetaLocalization metaLocalization)
        {
            if (environmentProfileSelector == null)
            {
                throw new ArgumentNullException("environmentProfileSelector");
            }

            if (environmentInitializationFactory == null)
            {
                throw new ArgumentNullException("environmentInitializationFactory");
            }

            if (environmentReset == null)
            {
                throw new ArgumentNullException("environmentReset");
            }

            if (metaLocalization == null)
            {
                throw new ArgumentNullException("metaLocalization");
            }

            _environmentProfileSelector = environmentProfileSelector;
            _environmentInitializationFactory = environmentInitializationFactory;
            _environmentReset = environmentReset;
            
            _environmentProfileSelector.EnvironmentSelected.AddListener(EnvironmentSelected);
            metaLocalization.LocalizationReset.AddListener(Reset);
        }

        /// <summary>
        /// Triggers the environment initialization process.
        /// </summary>
        public void Start()
        {
            _environmentProfileSelector.Select();
        }

        private void Reset()
        {
            if (_environmentInitialization != null)
            {
                _environmentInitialization.Stop();
                _environmentInitialization = null;
            }

            _environmentReset.Reset();
            _environmentProfileSelector.Reset();
        }

        private void EnvironmentSelected(EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType selectionResult)
        {
            _environmentInitialization = _environmentInitializationFactory.CreateEnvironmentInitialization(selectionResult);
            _environmentInitialization.Initialize();
        }
    }
}