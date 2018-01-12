using Meta.Tween;
using System.Collections;
using UnityEngine;

namespace Meta.Mouse
{
    /// <summary>
    /// Class responsible to provide user feedback about the meta mouse. It handles animations, sounds, etc...
    /// </summary>
    [DisallowMultipleComponent]
    internal class MetaMouseMessage : MonoBehaviour
    {
        /// <summary>
        /// Class that encapsulate the animation config values for the MetaMouseFeedback
        /// </summary>
        [System.Serializable]
        internal class MouseAnimationParameters
        {
            /// <summary>
            /// Duration of the mouse scale animation
            /// </summary>
            [SerializeField]
            private float _cursorScaleDuration;

            /// <summary>
            /// Curve of the mouse scale animation
            /// </summary>
            [SerializeField]
            private CurveAsset _cursorScaleAnimationCurve;

            /// <summary>
            /// Mouse text fade animation duration
            /// </summary>
            [SerializeField]
            private float _textFadeDuration;

            /// <summary>
            /// Mouse text stay duration, before disappearing
            /// </summary>
            [SerializeField]
            private float _textStayDuration;

            /// <summary>
            /// Mouse text message to show in the animation
            /// </summary>
            [SerializeField]
            private string _message;

            /// <summary>
            /// Mouse audio clip to play during the animation
            /// </summary>
            [SerializeField]
            private AudioClip _audioClip;

            /// <summary>
            /// Duration of the mouse scale animation
            /// </summary>
            public float CursorScaleDuration
            {
                get { return _cursorScaleDuration; }
                set { _cursorScaleDuration = value; }
            }

            /// <summary>
            /// Curve of the mouse scale animation
            /// </summary>
            public CurveAsset CursorScaleAnimationCurve
            {
                get { return _cursorScaleAnimationCurve; }
                set { _cursorScaleAnimationCurve = value; }
            }

            /// <summary>
            /// Mouse text fade animation duration
            /// </summary>
            public float TextFadeDuration
            {
                get { return _textFadeDuration; }
                set { _textFadeDuration = value; }
            }

            /// <summary>
            /// Mouse text stay duration, before disappearing
            /// </summary>
            public float TextStayDuration
            {
                get { return _textStayDuration; }
                set { _textStayDuration = value; }
            }

            /// <summary>
            /// Mouse text message to show in the animation
            /// </summary>
            public string Message
            {
                get { return _message; }
                set { _message = value; }
            }

            /// <summary>
            /// Mouse audio clip to play during the animation
            /// </summary>
            public AudioClip AudioClip
            {
                get { return _audioClip; }
                set { _audioClip = value; }
            }
        }

        /// <summary>
        /// Mouse cursor transform. Use it to change its properties during animations.
        /// </summary>
        [SerializeField]
        private Transform _cursor;
        /// <summary>
        /// Main text shown in the animaition. Use it to change its properties during animations.
        /// </summary>
        [SerializeField]
        private TextMesh _mainText;
        /// <summary>
        /// Text showing the state of the mouse during the animation. Use it to change its properties during animations.
        /// </summary>
        [SerializeField]
        private TextMesh _stateText;
        /// <summary>
        /// Audio source necessary to play audio clips during the animation.
        /// </summary>
        [SerializeField]
        private AudioSource _audioSource;
        /// <summary>
        /// Parameters of the animation we play when the mouse is turned on.
        /// </summary>
        [SerializeField]
        private MouseAnimationParameters _onAnimationParameters;
        /// <summary>
        /// Parameters of the animation we play when the mouse is turned off.
        /// </summary>
        [SerializeField]
        private MouseAnimationParameters _offAnimationParameters;

        /// <summary>
        /// Scale used to hide the mouse during the animations.
        /// </summary>
        private Vector3 _hideScale = Vector3.zero;
        /// <summary>
        /// Scale used to show the mouse during the animations.
        /// </summary>
        private Vector3 _regularCursorScale;

        /// <summary>
        /// Mouse cursor transform. Use it to change its properties during animations.
        /// </summary>
        public Transform Cursor
        {
            get { return _cursor; }
            set { _cursor = value; }
        }

        /// <summary>
        /// Main text shown in the animaition. Use it to change its properties during animations.
        /// </summary>
        public TextMesh MainText
        {
            get { return _mainText; }
            set { _mainText = value; }
        }

        /// <summary>
        /// Text showing the state of the mouse during the animation. Use it to change its properties during animations.
        /// </summary>
        public TextMesh StateText
        {
            get { return _stateText; }
            set { _stateText = value; }
        }

        /// <summary>
        /// Audio source necessary to play audio clips during the animation.
        /// </summary>
        public AudioSource AudioSource
        {
            get { return _audioSource; }
            set { _audioSource = value; }
        }

        /// <summary>
        /// Parameters of the animation we play when the mouse is turned on.
        /// </summary>
        public MouseAnimationParameters OnAnimationParameters
        {
            get { return _onAnimationParameters; }
            set { _onAnimationParameters = value; }
        }

        /// <summary>
        /// Parameters of the animation we play when the mouse is turned off.
        /// </summary>
        public MouseAnimationParameters OffAnimationParameters
        {
            get { return _offAnimationParameters; }
            set { _offAnimationParameters = value; }
        }

        /// <summary>
        /// Start the mouse configuration. This will set the initial state of the mouse, but with no animation.
        /// </summary>
        /// <param name="visible"></param>
        public void StartMouse(bool visible)
        {
            _regularCursorScale = _cursor.localScale;
            EnableText(false);

            if (visible)
            {
                _cursor.localScale = _regularCursorScale;
            }
            else
            {
                _cursor.localScale = _hideScale;
            }
        }

        /// <summary>
        /// Play the animations to show or hide the mouse.
        /// </summary>
        /// <param name="visible"></param>
        public void EnableMouse(bool visible)
        {
            StopAllCoroutines();
            if (visible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        /// <summary>
        /// Enable or disable the main text and the state text.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableText(bool enabled)
        {
            _mainText.gameObject.SetActive(enabled);
            _stateText.gameObject.SetActive(enabled);
            if (!enabled)
            {
                SetTextTransparency(_mainText, 0);
                SetTextTransparency(_stateText, 0);
            }
        }

        /// <summary>
        /// Coroutine that plays the animation to show or hide the main and state texts.
        /// </summary>
        /// <param name="animationParameters"></param>
        /// <returns></returns>
        private IEnumerator ShowText(MouseAnimationParameters animationParameters)
        {
            float fadeTimeMult = 1 / animationParameters.TextFadeDuration;

            _stateText.text = animationParameters.Message;

            EnableText(true);
            StartCoroutine(TextMeshTweens.Fade(_mainText, 1f, fadeTimeMult, null, null));
            yield return StartCoroutine(TextMeshTweens.Fade(_stateText, 1f, fadeTimeMult, null, null));
            yield return new WaitForSeconds(animationParameters.TextStayDuration);
            StartCoroutine(TextMeshTweens.Fade(_mainText, 0f, fadeTimeMult, null, null));
            yield return StartCoroutine(TextMeshTweens.Fade(_stateText, 0f, fadeTimeMult, null, null));
            EnableText(false);
        }

        /// <summary>
        /// Play the animation to show the mouse
        /// </summary>
        private void Show()
        {
            StopAllCoroutines();
            if (_onAnimationParameters.AudioClip != null)
            {
                _audioSource.PlayOneShot(_onAnimationParameters.AudioClip);
            }
            StartCoroutine(PlayShowAnimation());
        }

        /// <summary>
        /// Play the animation to hide the mouse
        /// </summary>
        private void Hide()
        {
            StopAllCoroutines();
            if (_offAnimationParameters.AudioClip != null)
            {
                _audioSource.PlayOneShot(_offAnimationParameters.AudioClip);
            }
            StartCoroutine(PlayHideAnimation());
        }

        /// <summary>
        /// Coroutine that plays the animation to show the mouse
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayShowAnimation()
        {
            StartCoroutine(TransformTweens.ToScale(_cursor, _regularCursorScale, 1 / _onAnimationParameters.CursorScaleDuration, _onAnimationParameters.CursorScaleAnimationCurve.Curve, null));
            yield return StartCoroutine(ShowText(_onAnimationParameters));
        }

        /// <summary>
        /// Coroutine that plays the animation to hide the mouse
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayHideAnimation()
        {
            yield return StartCoroutine(ShowText(_offAnimationParameters));
            StartCoroutine(TransformTweens.ToScale(_cursor, _hideScale, 1 / _offAnimationParameters.CursorScaleDuration, _offAnimationParameters.CursorScaleAnimationCurve.Curve, null));
        }

        /// <summary>
        /// Set a text transparency
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alpha"></param>
        private static void SetTextTransparency(TextMesh text, float alpha)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }       
    }
}