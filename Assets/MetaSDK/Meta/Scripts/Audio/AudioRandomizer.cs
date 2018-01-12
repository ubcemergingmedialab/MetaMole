using UnityEngine;

namespace Meta.Audio
{
    [CreateAssetMenu(menuName = "Audio Randomizer")]
    public class AudioRandomizer : ScriptableObject
    {
        [SerializeField]
        private AudioClip[] _clips;

        [SerializeField, Range(0, 2)]
        private float _minPitch = 0.8f;
        [SerializeField, Range(0, 2)]
        private float _maxPitch = 1.2f;

        public void Play(AudioSource audioSource)
        {
            if (_clips.Length != 0)
            {
                audioSource.clip = _clips[Random.Range(0, _clips.Length)];
                audioSource.pitch = Random.Range(_minPitch, _maxPitch);
                audioSource.Play();
            }
        }
    }
}