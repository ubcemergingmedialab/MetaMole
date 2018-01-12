using System.Threading;

namespace Meta
{
    /// <summary>
    /// Runs a function in a thread.
    /// </summary>
    public abstract class ThreadedJob
    {
        private bool _isDone = true;
        private object _jobLock = new object();
        private Thread _thread = null;

        /// <summary>
        /// Whether the thread function is done or not.
        /// </summary>
        protected bool IsDone
        {
            get
            {
                bool isDone;
                lock (_jobLock)
                {
                    isDone = _isDone;
                }
                return isDone;
            }
            set
            {
                lock (_jobLock)
                {
                    _isDone = value;
                }
            }
        }
        
        /// <summary>
        /// The function that is going to run in the thread.
        /// </summary>
        protected abstract void ThreadFunction();

        private void Run()
        {
            ThreadFunction();
            IsDone = true;
        }

        /// <summary>
        /// Creates and start a new thread.
        /// </summary>
        public virtual void Start()
        {
            if (_isDone)
            {
                _isDone = false;
                _thread = new Thread(Run);
                _thread.Start();
            }
            else
            {
                throw new System.Exception("ThreadedJob.Start: A job is already running");
            }
        }

        /// <summary>
        /// Abort the current thread.
        /// </summary>
        public virtual void Abort()
        {
            if (_thread != null && _thread.ThreadState == ThreadState.Running)
            {
                _thread.Abort();
            }
            _isDone = true;
        }
    }
}
