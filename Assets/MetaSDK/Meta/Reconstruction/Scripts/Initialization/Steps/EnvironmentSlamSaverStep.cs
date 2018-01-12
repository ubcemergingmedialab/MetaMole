using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Saves the current environment slam map.
    /// </summary>
    public class EnvironmentSlamSaverStep : EnvironmentInitializationStep
    {
        private readonly ISlamLocalizer _slamLocalizer;
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentSlamSaverStep"/> class.
        /// </summary>
        /// <param name="slamLocalizer">Slam type localizer.</param>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        public EnvironmentSlamSaverStep(ISlamLocalizer slamLocalizer, IEnvironmentProfileRepository environmentProfileRepository)
        {
            if (slamLocalizer == null)
            {
                throw new ArgumentNullException("slamLocalizer");
            }
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }

            _environmentProfileRepository = environmentProfileRepository;
            _slamLocalizer = slamLocalizer;
        }

        protected override void Initialize()
        {
            IEnvironmentProfile environmentProfile = _environmentProfileRepository.SelectedEnvironment;
            if (_slamLocalizer.IsFinished && environmentProfile != null)
            {
                string mapName = environmentProfile.MapName;
                if (string.IsNullOrEmpty(mapName))
                {
                    mapName = string.Format("{0}\\{1}", _environmentProfileRepository.GetPath(environmentProfile.Id), environmentProfile.Id);
                    _environmentProfileRepository.SetMapName(environmentProfile.Id, mapName);
                }
                _slamLocalizer.SaveSlamMap(mapName);
            }

            Finish();
        }
    }
}