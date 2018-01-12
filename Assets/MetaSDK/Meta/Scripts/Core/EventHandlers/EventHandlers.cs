using System;

namespace Meta
{
    /// <summary>
    /// Contains event delegates to allow control over execution of registered modules.
    /// </summary>
    public class EventHandlers : IEventHandlers
    {
        private event Action _awakeEvent;
        private event Action _startEvent;
        private event Action _updateEvent;
        private event Action _fixedUpdateEvent;
        private event Action _lateUpdateEvent;
        private event Action _onDestroyEvent;
        private event Action _onApplicationQuitEvent;

        #region Subscription
        /// <summary>
        /// Subsribe to Unity Awake event
        /// </summary>
        /// <param name="action">Action to be triggered on Awake</param>
        public void SubscribeOnAwake(Action action)
        {
            _awakeEvent += action;
        }

        /// <summary>
        /// Unsubscribe to Unity Awake event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from Awake</param>
        public void UnSubscribeOnAwake(Action action)
        {
            _awakeEvent -= action;
        }

        /// <summary>
        /// Subsribe to Unity Start event
        /// </summary>
        /// <param name="action">Action to be triggered on Start</param>
        public void SubscribeOnStart(Action action)
        {
            _startEvent += action;
        }

        /// <summary>
        /// Unsubscribe to Unity Start event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from Start</param>
        public void UnSubscribeOnStart(Action action)
        {
            _startEvent -= action;
        }

        /// <summary>
        /// Subsribe to Unity Update event
        /// </summary>
        /// <param name="action">Action to be triggered on Update</param>
        public void SubscribeOnUpdate(Action action)
        {
            _updateEvent += action;
        }

        /// <summary>
        /// Unsubscribe to Unity Update event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from Update</param>
        public void UnSubscribeOnUpdate(Action action)
        {
            _updateEvent -= action;
        }

        /// <summary>
        /// Subsribe to Unity FixedUpdate event
        /// </summary>
        /// <param name="action">Action to be triggered on FixedUpdate</param>
        public void SubscribeOnFixedUpdate(Action action)
        {
            _fixedUpdateEvent += action;
        }

        /// <summary>
        /// Unsubscribe to Unity FixedUpdate event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from FixedUpdate</param>
        public void UnSubscribeOnFixedUpdate(Action action)
        {
            _fixedUpdateEvent -= action;
        }

        /// <summary>
        /// Subsribe to Unity LateUpdate event
        /// </summary>
        /// <param name="action">Action to be triggered on LateUpdate</param>
        public void SubscribeOnLateUpdate(Action action)
        {
            _lateUpdateEvent += action;
        }

        /// <summary>
        /// Unsubscribe to Unity LateUpdate event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from LateUpdate</param>
        public void UnSubscribeOnLateUpdate(Action action)
        {
            _lateUpdateEvent -= action;
        }

        /// <summary>
        /// Subsribe to Unity Destroy event
        /// </summary>
        /// <param name="action">Action to be triggered on Destroy</param>
        public void SubscribeOnDestroy(Action action)
        {
            _onDestroyEvent += action;
        }

        /// <summary>
        /// Unsubscribe to Unity Destroy event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from Destroy</param>
        public void UnSubscribeOnDestroy(Action action)
        {
            _onDestroyEvent -= action;
        }

        /// <summary>
        /// Subsribe to Unity ApplicationQuit event
        /// </summary>
        /// <param name="action">Action to be triggered on ApplicationQuit</param>
        public void SubscribeOnApplicationQuit(Action action)
        {
            _onApplicationQuitEvent += action;
        }

        /// <summary>
        /// Unsubscribe to Unity ApplicationQuit event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from ApplicationQuit</param>
        public void UnSubscribeOnApplicationQuit(Action action)
        {
            _onApplicationQuitEvent -= action;
        }
        #endregion

        /// <summary>
        /// Raise the Awake event
        /// </summary>
        public void RaiseOnAwake()
        {
            if (_awakeEvent == null)
                return;
            _awakeEvent.Invoke();
        }

        /// <summary>
        /// Raise the Start event
        /// </summary>
        public void RaiseOnStart()
        {
            if (_startEvent == null)
                return;
            _startEvent.Invoke();
        }

        /// <summary>
        /// Raise the Update event
        /// </summary>
        public void RaiseOnUpdate()
        {
            if (_updateEvent == null)
                return;
            _updateEvent.Invoke();
        }

        /// <summary>
        /// Raise the FixedUpdate event
        /// </summary>
        public void RaiseOnFixedUpdate()
        {
            if (_fixedUpdateEvent == null)
                return;
            _fixedUpdateEvent.Invoke();
        }

        /// <summary>
        /// Raise the LateUpdate event
        /// </summary>
        public void RaiseOnLateUpdate()
        {
            if (_lateUpdateEvent == null)
                return;
            _lateUpdateEvent.Invoke();
        }

        /// <summary>
        /// Raise the OnDestroy event
        /// </summary>
        public void RaiseOnDestroy()
        {
            if (_onDestroyEvent == null)
                return;
            _onDestroyEvent.Invoke();
        }

        /// <summary>
        /// Raise the OnApplicationQuit event
        /// </summary>
        public void RaiseOnApplicationQuit()
        {
            if (_onApplicationQuitEvent == null)
                return;
            _onApplicationQuitEvent.Invoke();
        }
    }
}