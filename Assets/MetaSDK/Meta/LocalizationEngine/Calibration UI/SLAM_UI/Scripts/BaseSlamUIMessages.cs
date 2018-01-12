using UnityEngine;

namespace Meta.SlamUI
{
    /// <summary>
    /// Encapsulate common messages to enable different message controllers
    /// </summary>
    public abstract class BaseSlamUIMessages : MonoBehaviour
    {
        /// <summary>
        /// Current message being displayed
        /// </summary>
        public abstract SLAMUIMessageType CurrentMessage { get; set; }
    }
}