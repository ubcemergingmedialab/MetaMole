using UnityEngine;
using Meta.GeneralEnum;
using Meta.DisplayMode.ExtendedMode;
using Meta.DisplayMode.DirectMode;

namespace Meta.DisplayMode
{
    /// <summary>
    /// Controls the Display Mode for the Headset
    /// </summary>
    public class MetaDisplayModeController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Allow to auto adjust the display for Meta2")]
        private bool _autoAdjustDisplay = true;
        [SerializeField]
        [Tooltip("If Auto Adjust Display is true, adjust the display on the specific event function")]
        private UnityInitializationEvent _adjustOn;
        [SerializeField]
        [HideInInspector]
        private GameObject _mainCameraObject;
        private IMetaDisplayModeInformationProvider _provider;

        private void Awake()
        {
            if (!_autoAdjustDisplay)
                return;
            if (_adjustOn == UnityInitializationEvent.Awake)
                AdjustDisplay();
        }

        private void OnEnable()
        {
            if (!_autoAdjustDisplay)
                return;
            if (_adjustOn == UnityInitializationEvent.OnEnable)
                AdjustDisplay();
        }

        private void Start()
        {
            if (!_autoAdjustDisplay)
                return;
            if (_adjustOn == UnityInitializationEvent.Start)
                AdjustDisplay();
        }

        /// <summary>
        /// Adjust the display automatically for any Display Mode supported.
        /// </summary>
        public void AdjustDisplay()
        {
            if (_mainCameraObject == null)
            {
                Debug.LogError("Main Camera Object is not set");
                return;
            }

            if (_provider == null)
                _provider = new MetaDisplayModeInformationProvider();

            var mode = _provider.CurrentDisplayMode;
            switch (mode)
            {
                case MetaDisplayMode.None:
                    break;
                case MetaDisplayMode.DirectMode:
                    ActivateDirectMode();
                    break;
                case MetaDisplayMode.ExtendedMode:
                    AdjustMonitor();
                    break;
                default:
                    Debug.LogWarningFormat("Mode [{0}] not supported yet", mode);
                    break;
            }
        }

        /// <summary>
        /// Adjust the Window display if extended mode is the current display mode.
        /// </summary>
        private void AdjustMonitor()
        {
            var controller = _mainCameraObject.GetComponent<MetaExtendedModeController>();
            if (controller == null)
                controller = _mainCameraObject.AddComponent<MetaExtendedModeController>();

            controller.Meta2DisplayInformation = _provider.MetaDisplayInformation;
            controller.SelectMetaDisplay();

#if UNITY_5_6_OR_NEWER
            FlipRender(false);
#else
            FlipRender(true);
#endif
        }

        /// <summary>
        /// Activate Direct Mode if Direct Mode is enabled
        /// </summary>
        private void ActivateDirectMode()
        {
            var directMode = _mainCameraObject.GetComponent<MetaDirectMode>();
            if (directMode == null)
                directMode = _mainCameraObject.AddComponent<MetaDirectMode>();

            directMode.StartDirectMode();

            FlipRender(false);
        }

        /// <summary>
        /// Flip the Render Texture vertically if needed.
        /// </summary>
        /// <param name="flip">Flip or no flip</param>
        private void FlipRender(bool flip)
        {
            var setup = _mainCameraObject.GetComponent<SetupRenderTexturesStereo>();
            if (setup == null)
                return;
            setup.FlipImageVertically = flip;
        }
    }
}
