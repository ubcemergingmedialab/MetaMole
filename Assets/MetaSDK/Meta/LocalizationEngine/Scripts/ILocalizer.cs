using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Interface for localizers which update the position and rotation of a specified object.
    /// </summary>
    public interface ILocalizer
    {
        /// <summary>
        /// Updates the transform values for an object.
        /// </summary>
        void UpdateLocalizer();

        /// <summary>
        /// Resets the state of the localizer.
        /// </summary>
        void ResetLocalizer();

        /// <summary>
        /// Sets the target GameObject for localization
        /// </summary>
        /// <param name="targetGO">The object to be updated by the localizer.</param>
        void SetTargetGameObject(GameObject targetGO);
    }
}