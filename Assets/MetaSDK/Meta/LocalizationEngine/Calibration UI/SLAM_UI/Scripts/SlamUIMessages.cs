using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.SlamUI
{
    /// <summary>
    /// Controller for messages content and animations
    /// </summary>
    public class SlamUIMessages : BaseSlamUIMessages
    {
        [Tooltip("Text field for the message title")]
        [SerializeField]
        private Text _messageTitle;

        [Tooltip("Text field for the message content")]
        [SerializeField]
        private Text _messageContent;

        [Tooltip("Animation time in seconds to fade a message")]
        [SerializeField]
        private float _fadeTime = 1f;

        [Tooltip("Current message being displayed")]
        [SerializeField]
        private SLAMUIMessageType _currentMessage;

        private SlamMessage _slamMessage;
        private Dictionary<SLAMUIMessageType, SlamMessage> _slamUImessages;

        private Color _initialTitleColor;
        private Color _initialContentColor;

        /// <summary>
        /// Current message being displayed
        /// </summary>
        public override SLAMUIMessageType CurrentMessage
        {
            get { return _currentMessage; }
            set
            {
                if (_currentMessage != value)
                {
                    _currentMessage = value;
                    StartCoroutine(ChangeMessage());
                }
            }
        }

        private void Awake()
        {
            InitMessages();

            _initialTitleColor = _messageTitle.color;
            _initialContentColor = _messageContent.color;
        }

        private void OnValidate()
        {
            if (isActiveAndEnabled)
            {
                StartCoroutine(ChangeMessage());
            }
        }

        private void InitMessages()
        {
            _slamUImessages = new Dictionary<SLAMUIMessageType, SlamMessage>();

            _slamUImessages.Add(SLAMUIMessageType.None,
                new SlamMessage("", ""));
            _slamUImessages.Add(SLAMUIMessageType.WaitingForSensors, 
                new SlamMessage("Waiting for sensors ...", ""));
            _slamUImessages.Add(SLAMUIMessageType.WaitingForTracking,
                new SlamMessage("Initializing environment mapping", "Move your head side to side"));
            _slamUImessages.Add(SLAMUIMessageType.TurnAround,
                new SlamMessage("Turn your head", "Keep the white circle\ninside the blue marker"));
            _slamUImessages.Add(SLAMUIMessageType.HoldStill, 
                new SlamMessage("Hold your head still", ""));
            _slamUImessages.Add(SLAMUIMessageType.MappingSuccess, 
                new SlamMessage("Environment mapping complete", "", Color.green));
            _slamUImessages.Add(SLAMUIMessageType.MappingFail, 
                new SlamMessage("Unable to map the environment", "Visit metavision.com/mapping for details.\nRetrying ...", Color.red));

            _slamUImessages.Add(SLAMUIMessageType.Relocalization, 
                new SlamMessage("Relocalizing...", "Move your head side to side"));
            _slamUImessages.Add(SLAMUIMessageType.ReconstructionInstructions, 
                new SlamMessage("Reconstruction", "Move your head side to side"));
        }

        private IEnumerator ChangeMessage()
        {
            if (_slamUImessages != null)
            {
                if (_slamUImessages.TryGetValue(_currentMessage, out _slamMessage))
                {
                    // fade out
                    yield return Fade(1, 0, _fadeTime);

                    if (_messageTitle != null && _messageContent != null)
                    {
                        // set color
                        _messageTitle.color = (_slamMessage.TitleColor != null) ? _slamMessage.TitleColor.Value : _initialTitleColor;
                        _messageContent.color = (_slamMessage.ContentColor != null) ? _slamMessage.ContentColor.Value : _initialContentColor;

                        // set message
                        _messageTitle.text = _slamMessage.Title;
                        _messageContent.text = _slamMessage.Content;
                    }
                    
                    // fade in
                    yield return Fade(0, 1, _fadeTime);
                }
                else
                {
                    throw new System.Exception("Trying to access a SLAMUIMessageType key that was not inserted in the dictionary _slamUImessages.");
                }
            }
        }

        private IEnumerator Fade(float start, float end, float time)
        {
            float initialTime = Time.time;
            while (Time.time - initialTime <= time)
            {
                if (_messageTitle != null && _messageContent != null)
                {
                    float alpha = Mathf.Lerp(start, end, (Time.time - initialTime) / time);
                    _messageTitle.color = new Color(_messageTitle.color.r, _messageTitle.color.g, _messageTitle.color.b, alpha);
                    _messageContent.color = new Color(_messageContent.color.r, _messageContent.color.g, _messageContent.color.b, alpha);
                    yield return null;
                }
            }
        }
    }
}