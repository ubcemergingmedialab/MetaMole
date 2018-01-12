using UnityEngine;

namespace Meta.Buttons
{
    /// <summary>
    /// Broadcast each individual button events from the headset via events per button type
    /// </summary>
    public class MetaButtonIndividualEventBroadcaster : BaseMetaButtonEventBroadcaster
    {
        [SerializeField]
        private bool _enableCameraEvents = true;
        [SerializeField]
        private MetaButtonUnityEvent _cameraEvent;
        [SerializeField]
        private bool _enableVolumeUpEvents = true;
        [SerializeField]
        private MetaButtonUnityEvent _volumeUpEvent;
        [SerializeField]
        private bool _enableVolumeDownEvents = true;
        [SerializeField]
        private MetaButtonUnityEvent _volumeDownEvent;

        /// <summary>
        /// Process the button events
        /// </summary>
        /// <param name="button">Button event</param>
        protected override void ProcessButtonEvents(IMetaButton button)
        {
            switch (button.Type)
            {
                case ButtonType.ButtonCamera:
                    RaiseCameraEvent(button);
                    break;
                case ButtonType.ButtonVolumeDown:
                    RaiseVolumeDownEvent(button);
                    break;
                case ButtonType.ButtonVolumeUp:
                    RaiseVolumeUpEvent(button);
                    break;
            }
        }

        /// <summary>
        /// Raise the Camera Button Event
        /// </summary>
        /// <param name="button">Button message</param>
        private void RaiseCameraEvent(IMetaButton button)
        {
            if (!_enableCameraEvents)
            {
                return;
            }
            if (_cameraEvent == null)
            {
                return;
            }
            _cameraEvent.Invoke(button);
        }

        /// <summary>
        /// Raise the Volume Up Button Event
        /// </summary>
        /// <param name="button">Button message</param>
        private void RaiseVolumeUpEvent(IMetaButton button)
        {
            if (!_enableVolumeUpEvents)
            {
                return;
            }
            if (_volumeUpEvent == null)
            {
                return;
            }
            _volumeUpEvent.Invoke(button);
        }

        /// <summary>
        /// Raise the Volume Down Event
        /// </summary>
        /// <param name="button">Button message</param>
        private void RaiseVolumeDownEvent(IMetaButton button)
        {
            if (!_enableVolumeDownEvents)
            {
                return;
            }
            if (_volumeDownEvent == null)
            {
                return;
            }
            _volumeDownEvent.Invoke(button);
        }

        /// <summary>
        /// Enable or Disable the Camera Button Events
        /// </summary>
        public bool EnableCameraEvents
        {
            get { return _enableCameraEvents; }
            set { _enableCameraEvents = value; }
        }

        /// <summary>
        /// Enable or Disable the Volume Up Button Events
        /// </summary>
        public bool EnableVolumeUpEvents
        {
            get { return _enableVolumeUpEvents; }
            set { _enableVolumeUpEvents = value; }
        }

        /// <summary>
        /// Enable or Disable the Volume Down Button Events
        /// </summary>
        public bool EnableVolumeDownEvents
        {
            get { return _enableVolumeDownEvents; }
            set { _enableVolumeDownEvents = value; }
        }
    }
}
