using UnityEngine.Events;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Event that passes an <see cref="EnvironmentSelectionResultType"/>.
    /// </summary>
    public class EnvironmentSelectionResultTypeEvent : UnityEvent<EnvironmentSelectionResultTypeEvent.EnvironmentSelectionResultType>
    {
        /// <summary>
        /// Result type of the environment selection process.
        /// </summary>
        public enum EnvironmentSelectionResultType
        {
            SelectedEnvironment,
            NewEnvironment,
            None
        }
    }
}