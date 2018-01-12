using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

namespace Meta.Events
{
    /// <summary>
    /// Plays audio on pointer interactions
    /// </summary>
    public class AudioEventTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField]
        private AudioClip _downClickSound = null;

        [SerializeField]
        private AudioClip _upClickSound = null;

        [SerializeField]
        private AudioClip _dragUpClickSound = null;

        [SerializeField]
        private AudioClip _dragTickSound = null;

        [SerializeField]
        private AudioClip _dragContinuousSound = null;

        [SerializeField]
        private float _volume = 1f;

        [SerializeField]
        private AudioMixerGroup _audioMixerGroup = null;

        private float _dragSoundAccumulator;
        private AudioSource _oneShotAudioSource;
        private AudioSource _continuousAudioSource;
        private PointerEventData _eventData;

        private void Start()
        {
            _oneShotAudioSource = gameObject.AddComponent<AudioSource>();
            _oneShotAudioSource.spatialBlend = .5f;
            _oneShotAudioSource.volume = _volume;
            _oneShotAudioSource.outputAudioMixerGroup = _audioMixerGroup;
            if (_dragContinuousSound != null)
            {
                _continuousAudioSource = gameObject.AddComponent<AudioSource>();
                _continuousAudioSource.spatialBlend = .5f;
                _continuousAudioSource.loop = true;
                _continuousAudioSource.volume = 0f;
                _continuousAudioSource.clip = _dragContinuousSound;
                _continuousAudioSource.outputAudioMixerGroup = _audioMixerGroup;
                _continuousAudioSource.Play();
            }
        }

        private void Update()
        {
            if (_continuousAudioSource != null)
            {
                if (_eventData != null && _eventData.dragging && _eventData.delta.sqrMagnitude > 10f)
                {
                    _continuousAudioSource.volume += Time.deltaTime*2f;
                }
                else
                {
                    _continuousAudioSource.volume -= Time.deltaTime*2f;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_downClickSound != null)
                _oneShotAudioSource.PlayOneShot(_downClickSound);
            _eventData = eventData;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.dragging && _dragUpClickSound != null)
            {
                _oneShotAudioSource.PlayOneShot(_dragUpClickSound);
            }
            else if (_upClickSound != null)
            {
                _oneShotAudioSource.PlayOneShot(_upClickSound);
            }
            _eventData = null;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragTickSound != null)
            {
                _dragSoundAccumulator += Mathf.Clamp(eventData.delta.sqrMagnitude, 0f, 30f);
                if (_dragSoundAccumulator > 500f)
                {
                    _dragSoundAccumulator = 0f;
                    if (_dragTickSound != null)
                    {
                        _oneShotAudioSource.PlayOneShot(_dragTickSound);
                    }
                }
            }
        }
    }
}