using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// The environment initialization process split in a list of steps.
    /// </summary>
    public abstract class MultiStepEnvironmentInitialization : IEnvironmentInitialization
    {
        private EnvironmentInitializationStep[] _steps;

        /// <summary>
        /// Starts the environment initialization process.
        /// </summary>
        public void Initialize()
        {
            SetUpSteps();
            if (_steps.Length > 0)
            {
                _steps[0].Start();
            }
        }

        /// <summary>
        /// Stops the environment initialization process.
        /// </summary>
        public void Stop()
        {
            for (int i = 0; i < _steps.Length - 1; i++)
            {
                _steps[i].Stop();
            }

            _steps = null;
        }

        /// <summary>
        /// Creates the sorted list of initialization steps.
        /// </summary>
        /// <returns>The list of initialization steps</returns>
        protected abstract EnvironmentInitializationStep[] CreateSteps();

        /// <summary>
        /// SetUp the list of initialization steps.
        /// </summary>
        private void SetUpSteps()
        {
            if (_steps != null)
            {
                return;
            }

            _steps = CreateSteps();

            if (_steps == null)
            {
                throw new Exception("StepsChain not specified");
            }

            for (int i = 0; i < _steps.Length - 1; i++)
            {
                _steps[i].SetSuccessor(_steps[i + 1]);
            }
        }
    }
}