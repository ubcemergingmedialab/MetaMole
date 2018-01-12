using Meta.Audio;
using UnityEngine;

namespace Meta.HandInput
{
    /// <summary>
    /// Cursor placed on back of hand will display when it has entered a grabbable collider 
    /// and will provide feedback for when it is grabbing.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class HandCursor: MetaBehaviour
    {
        /// <summary>
        /// Represents an edge of the viewport.
        /// </summary>

        [SerializeField]
        private bool _playAudio = true;

        [SerializeField]
        private Transform _cursorTransform;


        [SerializeField]
        private SpriteRenderer _idleSprite;
        [SerializeField]
        private SpriteRenderer _idleContactSprite;
        [SerializeField]
        private SpriteRenderer _hoverSprite;
        [SerializeField]
        private SpriteRenderer _grabSprite;


        

        [SerializeField]
        private AudioRandomizer _grabAudio;

        [SerializeField]
        private AudioRandomizer _releaseAudio;


        private Hand _hand;
        private AudioSource _audioSource;
        private SpriteRenderer _centerOutOfBoundsSpriteRenderer;
        private CenterHandFeature _centerHandFeature;
        private Transform _centerOutOfBoundsSprite;
        private PalmState _lastPalmState = PalmState.Idle;
        private Vector3 _priorPos;
        private bool _vicinityOn = false;


        public bool PlayAudio
        {
            get { return _playAudio; }
            set { _playAudio = value; }
        }

        public AudioRandomizer GrabAudio
        {
            get { return _grabAudio; }
            set { _grabAudio = value; }
        }

        public AudioRandomizer ReleaseAudio
        {
            get { return _releaseAudio; }
            set { _releaseAudio = value; }
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _hand = GetComponentInParent<Hand>();
            _centerHandFeature = GetComponent<CenterHandFeature>();
            _centerHandFeature.OnEngagedEvent.AddListener(OnGrab);
            _centerHandFeature.OnDisengagedEvent.AddListener(OnRelease);
            _priorPos = ComputePriorPosition();
            _idleSprite.enabled = false;
            _idleContactSprite.enabled = false;
            _hoverSprite.enabled = false;
            _grabSprite.enabled = false;
        }

        private Vector3 ComputePriorPosition()
        {
            const float alpha = 0.75f;
            return Vector3.Lerp(_hand.Data.GrabAnchor, _hand.Palm.Position, alpha);
        }

        private void LateUpdate()
        {
            _cursorTransform.position = GetSmoothHandPosition();
            _cursorTransform.LookAt(Camera.main.transform);
            SetCursorVisualState();
        }

        /// <summary>
        /// Updates the visuals for the cursor which are not dependent upon the grab residual.
        /// </summary>
        private void SetCursorVisualState()
        {

            if (_centerHandFeature.PalmState != _lastPalmState)
            {
                switch (_centerHandFeature.PalmState)
                {

                    case PalmState.Idle:
                        _idleContactSprite.enabled = false;
                        _hoverSprite.enabled = false;
                        _grabSprite.enabled = false;
                        break;
                    case PalmState.Hovering:
                        _idleContactSprite.enabled = false;
                        _hoverSprite.enabled = true;
                        _grabSprite.enabled = false;

                        break;
                    case PalmState.Grabbing:
                        _idleContactSprite.enabled = false;
                        _hoverSprite.enabled = false;
                        _grabSprite.enabled = true;
                        break;
                }
            }
            if((_centerHandFeature.NearObjects.Count != 0) ^ _vicinityOn)
            {
                _vicinityOn = !_vicinityOn;
                if(_vicinityOn && _centerHandFeature.PalmState == PalmState.Idle)
                {
                    _idleContactSprite.enabled = true;
                }
                if (!_vicinityOn && _centerHandFeature.PalmState == PalmState.Idle)
                {
                    _idleContactSprite.enabled = false;
                }
            }

            _lastPalmState = _centerHandFeature.PalmState;
        }



        private void OnGrab(HandFeature handFeature)
        {
            PlayAudioClip(true);
        }

        private void OnRelease(HandFeature handFeature)
        {
            PlayAudioClip(false);
        }

        /// <summary>
        /// Checks if the hand is in the out of bounds region for the field of view.
        /// </summary>
        /// <returns>True, if the hand is is outside the pre-defined boundary regions.</returns>


        private void PlayAudioClip(bool isGrabbing)
        {
            if (PlayAudio)
            {
                if (isGrabbing)
                {
                    _grabAudio.Play(_audioSource);
                }
                else
                {
                    _releaseAudio.Play(_audioSource);
                }
            }
        }
        


        private Vector3 GetSmoothHandPosition()
        {
            const float alpha = 0.8f;
            Vector3 smoothPos = Vector3.Lerp(_priorPos, ComputePriorPosition(), alpha);
            _priorPos = smoothPos;
            return smoothPos;
        }
    }
}