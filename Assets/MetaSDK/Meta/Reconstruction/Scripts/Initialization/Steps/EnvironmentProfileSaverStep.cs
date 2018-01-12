using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Saves the changes of the selected environment.
    /// </summary>
    public class EnvironmentProfileSaverStep : EnvironmentInitializationStep
    {
        private readonly IEnvironmentProfileRepository _environmentProfileRepository;
        private readonly bool _async;
        private UnityThreadedJob _threadedJob;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentProfileSaverStep"/> class.
        /// </summary>
        /// <param name="async">Whether to perform an asynchronous save process or not.</param>
        /// <param name="environmentProfileRepository">Repository to access to the environment profiles.</param>
        public EnvironmentProfileSaverStep(bool async, IEnvironmentProfileRepository environmentProfileRepository)
        {
            if (environmentProfileRepository == null)
            {
                throw new ArgumentNullException("environmentProfileRepository");
            }
            _environmentProfileRepository = environmentProfileRepository;
            _async = async;
        }

        /// <summary>
        /// Stops the environment initialization process.
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            if (_threadedJob != null)
            {
                _threadedJob.Abort();
                _threadedJob.Dispose();
                _threadedJob = null;
            }
        }

        protected override void Initialize()
        {
            if (_environmentProfileRepository.SelectedEnvironment != null)
            {
                if (_async)
                {
                    _threadedJob = new UnityThreadedJob();
                    _threadedJob.RunFunction(Save, Saved);
                }
                else
                {
                    Save();
                    Saved();
                }
            }
            else
            {
                Finish();
            }
        }

        private void Save()
        {
            _environmentProfileRepository.Save();
        }

        private void Saved()
        {
            if (_threadedJob != null)
            {
                _threadedJob.Dispose();
                _threadedJob = null;
            }
            Finish();
        }
    }
}