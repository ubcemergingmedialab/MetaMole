using UnityEngine;

namespace Meta.Buttons
{
    public class SoundEffectTrigger : MonoBehaviour, IOnMetaButtonEvent
    {
        [SerializeField]
        [Tooltip("Target Button Type")]
        private ButtonType _buttonType;
        [SerializeField]
        [Tooltip("Target Button State")]
        private ButtonState _buttonState;
        [SerializeField]
        [Tooltip("Clip to play when the conditions are met")]
        private AudioClip _clip;
        [SerializeField]
        [Tooltip("Audio Source where to play the clip")]
        private AudioSource _source;

        /// <summary>
        /// Process the Meta Button Event
        /// </summary>
        /// <param name="button">Button Message</param>
        public void OnMetaButtonEvent(IMetaButton button)
        {
            if (button.Type != _buttonType)
            {
                return;
            }
            if (button.State != _buttonState)
            {
                return;
            }

            _source.PlayOneShot(_clip);
        }
    }
}
