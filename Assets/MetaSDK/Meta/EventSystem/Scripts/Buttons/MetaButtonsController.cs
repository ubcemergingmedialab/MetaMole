using UnityEngine;

namespace Meta.Buttons
{
    /// <summary>
    /// Controls the behaviour of the buttons by default
    /// </summary>
    public class MetaButtonsController : MonoBehaviour
    {
        private IMetaButtonEventProvider _broadcaster;
        private IOnMetaButtonEvent[] _controllers;

        /// <summary>
        /// Connect all the components in the childrens to the main Event Provider
        /// </summary>
        private void OnEnable()
        {
            if (_broadcaster == null)
            {
                _broadcaster = GetComponent<IMetaButtonEventProvider>();
            }
            if (_controllers == null)
            {
                _controllers = GetComponentsInChildren<IOnMetaButtonEvent>();
            }

            for (int i = 0; i < _controllers.Length; ++i)
            {
                _broadcaster.Subscribe(_controllers[i].OnMetaButtonEvent);
            }
        }

        /// <summary>
        /// Disconnects all the components in the childrens to the main Event Provider
        /// </summary>
        private void OnDisable()
        {
            if (_broadcaster == null)
            {
                return;
            }
            if (_controllers == null)
            {
                return;
            }

            for (int i = 0; i < _controllers.Length; ++i)
            {
                _broadcaster.Unsubscribe(_controllers[i].OnMetaButtonEvent);
            }
        }
    }
}
