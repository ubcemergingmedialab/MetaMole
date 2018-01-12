using UnityEngine;
using UnityEngine.UI;

namespace Meta
{
    /// <summary>
    /// Controls messages to be displayed on the Sensor Failure UI
    /// </summary>
    internal class MetaSensorMessageController : SlamTrackingCanvasConfigurer
    {
        [SerializeField]
        private Text _messageText;

        [SerializeField]
        private Text _headingText;

        [SerializeField]
        private Image _backgroundImage;

        /// <summary>
        /// Changes the message displayed
        /// </summary>
        /// <param name="newMessage">The new message to display.</param>
        internal void ChangeMessage(string newMessage)
        {
            _messageText.text = newMessage;
        }

        private void Start()
        {
            base.AutoConfigure();

            if (!_messageText || !_backgroundImage || !_headingText)
            {
                Debug.LogError(GetType() + " is not configured correctly.");
            }
        }

        /// <summary>
        /// Fade the alpha transparency of the UI to a desired target.
        /// </summary>
        /// <param name="targetAlpha">The target alpha transparency</param>
        public void FadeToAlphaOverSeconds(float targetAlpha, float seconds)
        {
            _backgroundImage.CrossFadeAlpha(targetAlpha, seconds, true);
            _messageText.CrossFadeAlpha(targetAlpha, seconds, true);
            _headingText.CrossFadeAlpha(targetAlpha, seconds, true);
        }

        public void SetTitleVisibility(bool isVisible)
        {
            _headingText.gameObject.SetActive(isVisible);
        }
    }
}