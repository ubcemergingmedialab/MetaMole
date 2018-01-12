using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Loads and configures the Slam Tracking UI
    /// </summary>
    internal class SlamTrackingUILoader : MonoBehaviour
    {
        [SerializeField]
        private bool _setupOnStart = true;
        private BaseSlamTrackingCanvasConfigurer _loadedObject;
        private string _prefabPath = "Prefabs/SlamTrackingUICanvas";

        /// <summary>
        /// Gets or sets the path of the prefab to instanciate relative to the resource folder
        /// </summary>
        public string PrefabResourcePath
        {
            get { return _prefabPath; }
            set { _prefabPath = value; }
        }

        /// <summary>
        /// Gets or sets the value that indicate if the setup should be run on Start
        /// </summary>
        public bool SetupOnStart
        {
            get { return _setupOnStart; }
            set { _setupOnStart = value; }
        }

        /// <summary>
        /// Calls the Setup on awake
        /// </summary>
        private void Start()
        {
            if (_setupOnStart)
            {
                Setup();
            }
        }

        /// <summary>
        /// Setup the Tracking UI.
        /// After the setup, this script is destroyed.
        /// </summary>
        public void Setup()
        {
            // Load Prefab
            if (_loadedObject == null)
            {
                var loadedPrefab = Resources.Load(_prefabPath) as GameObject;
                if (loadedPrefab == null)
                {
                    Debug.LogErrorFormat("Could not load Prefab: {0}", _prefabPath);
                    return;
                }
                _loadedObject = loadedPrefab.GetComponent<BaseSlamTrackingCanvasConfigurer>();
            }

            // Attach Controller
            var controller = gameObject.AddComponent<SlamTrackingUIController>();
            // Set Prefab
            controller.Prefab = _loadedObject;

            // Listen to Slam
            var localizer = gameObject.GetComponent<SlamLocalizer>();
            controller.ListenToSlamLocalizer(localizer);

            // Destroy
            if (Application.isEditor && !Application.isPlaying)
            {
                DestroyImmediate(this);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
