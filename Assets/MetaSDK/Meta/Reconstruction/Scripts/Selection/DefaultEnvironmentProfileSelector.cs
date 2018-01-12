using System;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Auto selects the default environment profile.
    /// </summary>
    public class DefaultEnvironmentProfileSelector : IEnvironmentProfileSelector
    {
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;
        private readonly ISlamChecker _slamChecker;
        private readonly EnvironmentSelectionResultTypeEvent _environmentSelected = new EnvironmentSelectionResultTypeEvent();
        private IEnvironmentProfile _defaultEnvironmentProfile;

        /// <summary>
        /// Occurs when an environment profile is selected.
        /// </summary>
        public EnvironmentSelectionResultTypeEvent EnvironmentSelected
        {
            get { return _environmentSelected; }
        }

        /// <summary>
        /// Creates an instance of <see cref="DefaultEnvironmentProfileSelector"/> class.
        /// </summary>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        /// <param name="slamChecker">Object to check if an slam map can be localized.</param>
        public DefaultEnvironmentProfileSelector(IEnvironmentProfileRepository environmentProfileRepository, ISlamChecker slamChecker)
        {
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }

            if (slamChecker == null)
            {
                throw new ArgumentNullException("slamChecker");
            }

            _environmentProfileRepository = environmentProfileRepository;
            _slamChecker = slamChecker;
        }

        /// <summary>
        /// Selects an environment profile.
        /// </summary>
        public void Select()
        {
            Read();
            FinishReading();
        }

        /// <summary>
        /// Resets the environment profile selection.
        /// </summary>
        public void Reset()
        {
            if (_slamChecker != null)
            {
                _slamChecker.Stop();
            }

            _environmentSelected.Invoke(EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType.NewEnvironment);
        }
        
        private void Read()
        {
            _environmentProfileRepository.Read();
            _defaultEnvironmentProfile = _environmentProfileRepository.GetDefault();
        }
        
        private void FinishReading()
        {
            //If there is no default environment profile, or if it is not valid, just skip automatically and return to start creating one.
            if (_defaultEnvironmentProfile == null || !_environmentProfileRepository.Verify(_defaultEnvironmentProfile.Id))
            {
                if (_environmentSelected != null)
                {
                    _environmentSelected.Invoke(EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType.NewEnvironment);
                }
            }
            else
            {
                //Otherwise, lets select the first one.
                SelectEnvironment(_defaultEnvironmentProfile);
            }
        }

        private void SelectEnvironment(IEnvironmentProfile environmentProfile)
        {
            _slamChecker.TryLocalizeMap(environmentProfile.MapName, (ok) =>
            {
                if (ok)
                {
                    _environmentProfileRepository.Select(environmentProfile.Id);
                }
                _environmentSelected.Invoke(ok ? EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType.SelectedEnvironment : EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType.NewEnvironment);
            });
        }
    }
}