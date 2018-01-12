using UnityEngine;
using System.Collections;

namespace Meta
{
    /// <summary>
    /// Shows a temporal help animation message.
    /// </summary>
    public class TemporalHelpAnimationMessageController : MonoBehaviour
    {
        [Tooltip("ANimation to show the message.")]
        [SerializeField]
        private Animator _targetAnimation;

        [Tooltip("Whether to show on Start.")]
        [SerializeField]
        private bool _showOnStart;

        [Tooltip("Time in seconds to wait before showing the target.")]
        [SerializeField]
        private float _showDelay;

        [Tooltip("Time in seconds to wait before hiding the target.")]
        [SerializeField]
        private float _stayTime;

        private void Start()
        {
            if (_showOnStart)
            {
                Show();
            }
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        public void Show()
        {
            StopAllCoroutines();
            StartCoroutine(ShowTargetCoroutine());
        }

        /// <summary>
        /// Hides the message.
        /// </summary>
        public void Hide()
        {
            StopAllCoroutines();
            StartHideAnimation();
        }

        private void StartShowAnimation()
        {
            _targetAnimation.SetBool("Show", true);
        }

        private void StartHideAnimation()
        {
            _targetAnimation.SetBool("Show", false);
        }

        private IEnumerator ShowTargetCoroutine()
        {
            yield return new WaitForSeconds(_showDelay);
            StartShowAnimation();
            yield return new WaitForSeconds(_stayTime);
            StartHideAnimation();
        }
    }
}
