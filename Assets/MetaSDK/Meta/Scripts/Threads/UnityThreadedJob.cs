using System;
using System.Collections;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Runs a function in a thread, and waits for it. Then call a finish event in the unity thread.
    /// </summary>
    public class UnityThreadedJob : ThreadedJob, IDisposable
    {
        private bool _disposed;
        private Action _currentThreadFunction;
        private MonoBehaviourThreadedJob _threadedJobObject;

        private MonoBehaviourThreadedJob ThreadedJobObject
        {
            get
            {
                if (_threadedJobObject == null)
                {
                    _threadedJobObject = new GameObject("ThreadedJob").AddComponent<MonoBehaviourThreadedJob>();
                    _threadedJobObject.Disabled.AddListener(Abort);
                    #if UNITY_EDITOR
                    {
                        _threadedJobObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
                    }
                    #endif
                }
                return _threadedJobObject;
            }
        }
        
        ~UnityThreadedJob()
        {
            Clean();
        }
        
        /// <summary>
        /// Cleans resourses.
        /// </summary>
        public void Dispose()
        {
            Clean();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Runs a function in the thread from. doneAction will be called when the thread is ready.
        /// </summary>
        /// <param name="threadAction">The function to be run in a thread.</param>
        /// <param name="doneAction">Event that occurs when the threadAction is done.</param>
        public void RunFunction(System.Action threadAction, System.Action doneAction)
        {
            if (threadAction == null)
            {
                throw new ArgumentNullException("threadAction");
            }

            ThreadedJobObject.StartCoroutine(RunFunctionFromGameObject(threadAction, () =>
            {
                if (doneAction != null)
                {
                    doneAction();
                }
            }));
        }

        /// <summary>
        /// Runs a function in the thread from a gameObject. doneAction will be called when the thread is ready.
        /// </summary>
        /// <param name="threadAction">The function to be run in a thread.</param>
        /// <param name="doneAction">Event that occurs when the threadAction is done.</param>
        public IEnumerator RunFunctionFromGameObject(System.Action action, System.Action doneAction)
        {
            while (!IsDone)
            {
                yield return 0;
            }

            _currentThreadFunction = action;

            Start();
            while (!IsDone)
            {
                yield return 0;
            }

            if (doneAction != null)
            {
                doneAction();
            }
        }

        /// <summary>
        /// Thread function that will run in the thread.
        /// </summary>
        protected override void ThreadFunction()
        {
            if (_currentThreadFunction != null)
            {
                _currentThreadFunction();
            }
        }

        private void Clean()
        {
            if (_disposed)
            {
                return;
            }

            if (_threadedJobObject != null)
            {
                _threadedJobObject.MarkToDestroy();
                _threadedJobObject = null;
            }

            _disposed = true;
        }
    }
}