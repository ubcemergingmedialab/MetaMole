using System;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Base class to Control the display of messages when slam lose tracking
    /// </summary>
    internal abstract class BaseSlamTrackingMessageController : MonoBehaviour, ISlamTrackingMessageController
    {
        /// <summary>
        /// Hides the Slam Messages
        /// </summary>
        public abstract void Hide();

        /// <summary>
        /// Hides the Slam Messages and executes the given callback when the animation finishes
        /// </summary>
        /// <param name="callback">Callback</param>
        public abstract void Hide(Action callback);

        /// <summary>
        /// Triggered when the animation finishes.
        /// </summary>
        public abstract void OnAnimationFinish();
    }
}
