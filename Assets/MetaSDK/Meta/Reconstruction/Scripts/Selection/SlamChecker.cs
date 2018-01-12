using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Checks if slam map can be localized.
    /// </summary>
    public class SlamChecker : ISlamChecker
    {
        private readonly ISlamLocalizer _slamLocalizer;
        private Action<bool> _doneAction;

        /// <summary>
        /// Creates an instance of <see cref="SlamChecker"/> class.
        /// </summary>
        /// <param name="slamLocalizer">Slam type localizer.</param>
        public SlamChecker(ISlamLocalizer slamLocalizer)
        {
            _slamLocalizer = slamLocalizer;
            if (_slamLocalizer != null)
            {
                _slamLocalizer.SetInitializeOnStart(false);
            }
        }

        /// <summary>
        /// Tries to localize an slam map.
        /// </summary>
        /// <param name="mapPath">The path of the slam map file.</param>
        /// <param name="doneAction">Action called with the localization response.</param>
        public void TryLocalizeMap(string mapPath, Action<bool> doneAction)
        {
            if (_slamLocalizer == null)
            {
                if (doneAction != null)
                {
                    doneAction(false);
                }
                return;
            }

            if (_slamLocalizer.IsFinished)
            {
                throw new Exception("SlamLocalizer was already initialized");
            }
            
            SetSlamListener();
            _doneAction = doneAction;
            _slamLocalizer.LoadSlamMap(mapPath);
        }

        /// <summary>
        /// Stops the slam checking process.
        /// </summary>
        public void Stop()
        {
            StopSlamListener();
        }

        private void SetSlamListener()
        {
            _slamLocalizer.SlamMapLoadingFailed.AddListener(SlamNotLocalized);
            _slamLocalizer.SlamMappingCompleted.AddListener(SlamLocalized);
        }

        private void StopSlamListener()
        {
            _slamLocalizer.SlamMapLoadingFailed.RemoveListener(SlamNotLocalized);
            _slamLocalizer.SlamMappingCompleted.RemoveListener(SlamLocalized);
        }

        private void SlamNotLocalized()
        {
            Finish(false);
        }

        private void SlamLocalized()
        {
            Finish(true);
        }

        private void Finish(bool couldLocalizeMap)
        {
            Stop();
            if (_doneAction != null)
            {
                _doneAction(couldLocalizeMap);
            }
        }
    }
}