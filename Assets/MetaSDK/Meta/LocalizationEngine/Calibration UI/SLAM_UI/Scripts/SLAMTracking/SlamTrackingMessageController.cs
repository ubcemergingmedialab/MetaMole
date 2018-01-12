using System;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Control the display of messages when slam lose tracking
    /// </summary>
    internal class SlamTrackingMessageController : BaseSlamTrackingMessageController
    {
        [SerializeField]
        private Animator _animator;
        private Action _currentCallback;

        /// <summary>
        /// Gets or sets the Animator to be used for hiding.
        /// The Animator must contain a trigger called "Hide"
        /// </summary>
        public Animator Animator
        {
            get { return _animator; }
            set { _animator = value; }
        }

        /// <summary>
        /// Hides the Slam Messages
        /// </summary>
        public override void Hide()
        {
            Hide(null);
        }

        /// <summary>
        /// Hides the Slam Messages and executes the given callback when the animation finishes
        /// </summary>
        /// <param name="callback">Callback</param>
        public override void Hide(Action callback)
        {
            if (_currentCallback != null)
            {
                return;
            }

            _currentCallback = callback;
            _animator.SetTrigger("Hide");
        }

        /// <summary>
        /// Triggered when the animation finishes.
        /// </summary>
        public override void OnAnimationFinish()
        {
            if (_currentCallback != null)
            {
                _currentCallback.Invoke();
                _currentCallback = null;
            }
        }
    }
}