using System;

namespace Meta.Reconstruction
{
    /// <summary>
    /// An step of the environment initialization process. When it finishes, calls the next step if there is one.
    /// </summary>
    public abstract class EnvironmentInitializationStep
    {
        protected EnvironmentInitializationStep _successor;

        /// <summary>
        /// Starts processing the initialization step.
        /// </summary>
        public void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Set a successor step to continue with environment initialization.
        /// </summary>
        /// <param name="successor">The successor step to continue with environment initialization</param>
        public void SetSuccessor(EnvironmentInitializationStep successor)
        {
            if (successor == null)
            {
                throw new ArgumentNullException("successor");
            }
            _successor = successor;
        }

        /// <summary>
        /// Stops the environment initialization process.
        /// </summary>
        public virtual void Stop()
        {
            _successor = null;
        }

        /// <summary>
        /// Initializes the scan step.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Finishes the scan step.
        /// </summary>
        protected void Finish()
        {
            if (_successor != null)
            {
                _successor.Start();
            }
        }
    }
}