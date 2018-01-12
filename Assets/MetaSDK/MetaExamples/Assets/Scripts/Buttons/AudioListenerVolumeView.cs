using UnityEngine;
using UnityEngine.UI;

namespace Meta.Buttons
{
    /// <summary>
    /// Script that updates the text of a UI when a volume event raises
    /// </summary>
    public class AudioListenerVolumeView : MonoBehaviour
    {
        [SerializeField]
        private Text _uiText;
        private bool _subscribed;
        private bool _update;

        private void OnEnable()
        {
            if (_subscribed)
                return;
            var broadcaster = gameObject.GetComponentInParent<MetaButtonEventBroadcaster>();
            broadcaster.Subscribe(ProcessButtonEvent);
            _subscribed = true;
        }

        private void OnDisable()
        {
            if (!_subscribed)
                return;
            var broadcaster = gameObject.GetComponentInParent<MetaButtonEventBroadcaster>();
            if (broadcaster == null)
            {
                _subscribed = false;
                return;
            }

            broadcaster.Unsubscribe(ProcessButtonEvent);
            _subscribed = false;
        }

        public void ProcessButtonEvent(IMetaButton button)
        {
            if (button.Type == ButtonType.ButtonCamera)
            {
                return;
            }

            switch (button.State)
            {
                case ButtonState.ButtonRelease:
                    _update = false;
                    break;
                case ButtonState.ButtonLongPress:
                    _update = true;
                    break;
            }
            _uiText.text = string.Format("Volume: {0:0.00}", AudioListener.volume);
        }

        private void Update()
        {
            if (!_update)
                return;
            _uiText.text = string.Format("Volume: {0:0.00}", AudioListener.volume);
        }
    }
}
