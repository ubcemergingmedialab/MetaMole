using System;

namespace Meta
{
    /// <summary>
    /// Interface to Control the display of messages when slam lose tracking
    /// </summary>
    internal interface ISlamTrackingMessageController
    {
        /// <summary>
        /// Hides the Slam Messages
        /// </summary>
        void Hide();

        /// <summary>
        /// Hides the Slam Messages and executes the given callback when the animation finishes
        /// </summary>
        /// <param name="callback">Callback</param>
        void Hide(Action callback);

        /// <summary>
        /// Triggered when the animation finishes.
        /// </summary>
        void OnAnimationFinish();
    }
}
