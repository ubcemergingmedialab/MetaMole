using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Allows panel events to be called on a MetaPanel object.
    /// </summary>
    public interface IPanelClickEventHandler
    {
        /// <summary>
        ///     Called on touching a panel surface.
        /// </summary>
        /// <param name="id">The id of the cursor which touched the panel.</param>
        /// <param name="pos">The world position where the cursor touched the panel.</param>
        void OnPanelPointerDown(int id, Vector3 pos);

        /// <summary>
        ///     Called on exiting a panel surface.
        /// </summary>
        /// <param name="id">The id of the cursor which exited the panel.</param>
        /// <param name="pos">The world position where the cursor exited the panel.</param>
        void OnPanelPointerUp(int id, Vector3 pos);
    }
}
