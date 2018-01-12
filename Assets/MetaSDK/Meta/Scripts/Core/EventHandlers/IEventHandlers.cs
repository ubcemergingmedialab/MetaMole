using System;

namespace Meta
{
    /// <summary>
    /// Subscribe or unsubscribe to specific unity events
    /// </summary>
    internal interface IEventHandlers
    {
        /// <summary>
        /// Subsribe to Unity Awake event
        /// </summary>
        /// <param name="action">Action to be triggered on Awake</param>
        void SubscribeOnAwake(Action action);

        /// <summary>
        /// Unsubscribe to Unity Awake event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from Awake</param>
        void UnSubscribeOnAwake(Action action);

        /// <summary>
        /// Subsribe to Unity Start event
        /// </summary>
        /// <param name="action">Action to be triggered on Start</param>
        void SubscribeOnStart(Action action);

        /// <summary>
        /// Unsubscribe to Unity Start event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from Start</param>
        void UnSubscribeOnStart(Action action);

        /// <summary>
        /// Subsribe to Unity Update event
        /// </summary>
        /// <param name="action">Action to be triggered on Update</param>
        void SubscribeOnUpdate(Action action);

        /// <summary>
        /// Unsubscribe to Unity Update event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from Update</param>
        void UnSubscribeOnUpdate(Action action);

        /// <summary>
        /// Subsribe to Unity FixedUpdate event
        /// </summary>
        /// <param name="action">Action to be triggered on FixedUpdate</param>
        void SubscribeOnFixedUpdate(Action action);

        /// <summary>
        /// Unsubscribe to Unity FixedUpdate event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from FixedUpdate</param>
        void UnSubscribeOnFixedUpdate(Action action);

        /// <summary>
        /// Subsribe to Unity LateUpdate event
        /// </summary>
        /// <param name="action">Action to be triggered on LateUpdate</param>
        void SubscribeOnLateUpdate(Action action);

        /// <summary>
        /// Unsubscribe to Unity LateUpdate event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from LateUpdate</param>
        void UnSubscribeOnLateUpdate(Action action);

        /// <summary>
        /// Subsribe to Unity Destroy event
        /// </summary>
        /// <param name="action">Action to be triggered on Destroy</param>
        void SubscribeOnDestroy(Action action);

        /// <summary>
        /// Unsubscribe to Unity Destroy event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from Destroy</param>
        void UnSubscribeOnDestroy(Action action);

        /// <summary>
        /// Subsribe to Unity ApplicationQuit event
        /// </summary>
        /// <param name="action">Action to be triggered on ApplicationQuit</param>
        void SubscribeOnApplicationQuit(Action action);

        /// <summary>
        /// Unsubscribe to Unity ApplicationQuit event
        /// </summary>
        /// <param name="action">Action to be unsubscribed from ApplicationQuit</param>
        void UnSubscribeOnApplicationQuit(Action action);
    }
}
