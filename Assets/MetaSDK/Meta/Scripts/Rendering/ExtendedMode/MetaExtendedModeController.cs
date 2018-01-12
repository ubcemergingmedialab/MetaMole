using System.Collections;
using UnityEngine;
using Meta.DevMode;

namespace Meta.DisplayMode.ExtendedMode
{
    /// <summary>
    /// Controls the Meta Extended Mode
    /// </summary>
    public class MetaExtendedModeController : MonoBehaviour
    {
        IMetaDisplayInfo _displayInfo;

        /// <summary>
        /// Select the Meta Display as target display
        /// </summary>
        public void SelectMetaDisplay()
        {
            if (_displayInfo == null)
                return;

            StartCoroutine(AdjustDisplay());
        }

        /// <summary>
        /// Adjust the display one frame at a time
        /// </summary>
        /// <returns></returns>
        private IEnumerator AdjustDisplay()
        {
            // Efectively change the resolution and then Go to full screen first
            var resX = _displayInfo.ResolutionX / 2;
            var resY = _displayInfo.ResolutionY / 2;
            Screen.SetResolution(resX, resY, true);
            yield return null;

            // Change to the correct display
            PlayerPrefs.SetInt("UnitySelectMonitor", _displayInfo.UnityDisplayIndex);
            yield return null;

            // Update the resolution and go Full Screen again
            Screen.SetResolution(_displayInfo.ResolutionX, _displayInfo.ResolutionY, true);
            yield return null;
        }

        /// <summary>
        /// Gets or sets the Display Information
        /// </summary>
        public IMetaDisplayInfo Meta2DisplayInformation
        {
            get { return _displayInfo; }
            set { _displayInfo = value; }
        }
    }
}
