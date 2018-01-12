using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Controls when to display the Slam Tracking UI
    /// </summary>
    internal class SlamTrackingUIController : MonoBehaviour
    {
        [SerializeField]
        private BaseSlamTrackingCanvasConfigurer _prefab;
        private BaseSlamTrackingCanvasConfigurer _prefabInstance;

        /// <summary>
        /// Gets or sets the Prefab containg the UI to show
        /// </summary>
        public BaseSlamTrackingCanvasConfigurer Prefab
        {
            get { return _prefab; }
            set { _prefab = value; }
        }

        /// <summary>
        /// Subscribe to the events of SlamLocalizer.
        /// </summary>
        /// <param name="localizer">Slam localizer</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool ListenToSlamLocalizer(SlamLocalizer localizer)
        {
            if (localizer == null)
            {
                Debug.LogError("Given Slam Localizer is null");
                return false;
            }

            localizer.onSlamTrackingLost.AddListener(OnSlamTrackingLost);
            localizer.onSlamTrackingRelocalized.AddListener(OnSlamTrackingRecovered);
            return true;
        }

        /// <summary>
        /// Executed when Slam Tracking is lost
        /// </summary>
        private void OnSlamTrackingLost()
        {
            // Create UI
            if (_prefabInstance == null)
            {
                if (_prefab == null)
                {
                    Debug.LogError("Prefab is null, cannot instantiate UI");
                    return;
                }
                _prefabInstance = Instantiate(_prefab);
                _prefabInstance.AutoConfigure();
            }
        }

        /// <summary>
        /// Executed when Slam tracking is recovered
        /// </summary>
        private void OnSlamTrackingRecovered()
        {
            // Destroy UI
            if (_prefabInstance != null)
            {
                var messageController = _prefabInstance.GetComponentInChildren<BaseSlamTrackingMessageController>();
                if (messageController == null)
                {
                    Debug.LogWarning("Instance does not contain SlamTrackingMessageController. Destroying instance.");
                    Destroy(_prefabInstance.gameObject);
                    return;
                }

                // Hide and then destroy
                messageController.Hide(() =>
                {
                    Destroy(_prefabInstance.gameObject);
                    _prefabInstance = null;
                });
            }
        }
    }
}
