using UnityEngine;
using UnityEngine.Events;
using System;

namespace Meta
{
    /// <summary>
    /// Handles setup and execution of localization for the MetaWorld prefab.
    /// </summary>
    internal class MetaLocalization : IEventReceiver
    {
        private UnityEvent _localizationWillReset = new UnityEvent();
        private UnityEvent _localizationReset = new UnityEvent();
        private GameObject _targetGO;
        private ILocalizer _currentLocalizer;
        private KeyCode _resetShortcut = KeyCode.F4;

        /// <summary>
        /// Occurs before the localization reset.
        /// </summary>
        public UnityEvent LocalizationWillReset
        {
            get { return _localizationWillReset; }
        }

        /// <summary>
        /// Occurs after the localization reset.
        /// </summary>
        public UnityEvent LocalizationReset
        {
            get { return _localizationReset; }
        }

        /// <summary>
        /// Constructor for the localization module.
        /// </summary>
        /// <param name="targetGO">The object to be updated with values from the localizer.</param>
        /// <param name="currentLocalizer">The localization method to be used.</param>
        internal MetaLocalization(GameObject targetGO)
        {
            _targetGO = targetGO;
        }

        /// <summary>
        /// Initializes the events for the module.
        /// </summary>
        /// <param name="eventHandlers"></param>
        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnUpdate(Update);
        }

        /// <summary>
        /// Sets the localizer to be used for position and rotation tracking.
        /// </summary>
        /// <param name="localizerType">The type of localizer to be used.</param>
        public void SetLocalizer(Type localizerType)
        {
            if (_currentLocalizer != null && localizerType == _currentLocalizer.GetType())
            {
                return; //The same type of localizer is already assigned
            }

            //Get the components that are ILocalizers from the target object
            var oldComponents = _targetGO.GetComponents<ILocalizer>();
            if (oldComponents != null && oldComponents.Length > 0 && oldComponents[0].GetType() == localizerType)
            {
                SetLocalizer(oldComponents[0]);
                return; //Avoid reassignment and loss of set script values in editor
            }
            if (!(typeof(ILocalizer).IsAssignableFrom(localizerType)))
            {
                return; // type was not a ILocalizer
            }

            if (oldComponents != null)
            {
                foreach (var component in oldComponents)
                {
                    if (component != null)
                    {
                        GameObject.DestroyImmediate((UnityEngine.Object) component);
                    }
                }
            }
            SetLocalizer(_targetGO.AddComponent(localizerType) as ILocalizer);
        }

        /// <summary>
        /// Gets the current localizer.
        /// </summary>
        /// <returns></returns>
        public ILocalizer GetLocalizer()
        {
            return _currentLocalizer;
        }

        /// <summary>
        /// Resets the currently enabled localizer.
        /// </summary>
        public void ResetLocalization()
        {
            if (_currentLocalizer != null)
            {
                _localizationWillReset.Invoke();
                _currentLocalizer.ResetLocalizer();
                _localizationReset.Invoke();
            }
        }

        /// <summary>
        /// Calls the update loop to get new values from the localizer.
        /// </summary>
        private void Update()
        {
            if (_currentLocalizer != null)
            {
                if (Input.GetKeyDown(_resetShortcut))
                {
                    ResetLocalization();
                }
                _currentLocalizer.UpdateLocalizer();
            }
        }

        private void SetLocalizer(ILocalizer localizer)
        {
            _currentLocalizer = localizer;
            _currentLocalizer.SetTargetGameObject(_targetGO);
        }
    }
}