using System.Collections;
using UnityEngine;

namespace Meta.Buttons
{
    /// <summary>
    /// Base class that controls the volume of the AudioListener in Unity
    /// </summary>
    public class MetaButtonVolumeController : MonoBehaviour, IOnMetaButtonEvent
    {
        [SerializeField]
        [Range(0, 1)]
        [Tooltip("Represents the delta volume for every time the button is pressed")]
        private float _delta = 0.05f;
        private float _targetTime = 0.25f;
        private float _currentDelta;
        private Coroutine _volumeCoroutine;

        /// <summary>
        /// Process the Meta Button Event
        /// </summary>
        /// <param name="button">Button Message</param>
        public void OnMetaButtonEvent(IMetaButton button)
        {
            if (button.Type == ButtonType.ButtonCamera)
            {
                return;
            }
            if (!this.enabled)
            {
                Debug.LogWarning("Script is not enabled");
                return;
            }
            if (button.Type == ButtonType.ButtonVolumeUp)
            {
                _currentDelta = _delta;
            }
            if (button.Type == ButtonType.ButtonVolumeDown)
            {
                _currentDelta = _delta * -1f;
            }


            switch (button.State)
            {
                case ButtonState.ButtonShortPress:
                    UpdateVolume();
                    break;
                case ButtonState.ButtonLongPress:
                    _volumeCoroutine = StartCoroutine(UpdateRoutine());
                    break;
                case ButtonState.ButtonRelease:
                    if (_volumeCoroutine != null)
                    {
                        StopCoroutine(_volumeCoroutine);
                        _volumeCoroutine = null;
                    }
                    break;
            }
        }

        /// <summary>
        /// Loop for lowering the volume
        /// </summary>
        private IEnumerator UpdateRoutine()
        {
            while (true)
            {
                UpdateVolume();
                yield return new WaitForSeconds(_targetTime);
            }
        }

        /// <summary>
        /// Lower the volume
        /// </summary>
        private void UpdateVolume()
        {
            AudioListener.volume = Mathf.Clamp01(AudioListener.volume + _currentDelta);
        }
    }
}
