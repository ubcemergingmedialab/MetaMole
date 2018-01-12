using UnityEngine;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Meta-Editor")]
[assembly: InternalsVisibleTo("Assembly-CSharp-Editor")]

namespace Meta
{
    /// <summary>
    /// Handles setup of the Meta scene environment and provides reference to Meta context classes.
    /// </summary>
    internal class MetaManager : MonoBehaviour
    {
        [SerializeField]
        private DataAcquisitionSystem _dataAcquisitionSystem = new DataAcquisitionSystem();

        /// <summary>
        /// The scale mapping of 1 meter to the number of unity units. This affects the simulation parameters. As this value increases, 1 unity unit will become smaller in
        /// real world terms. eg To use a simulation scale of 1 = 1cm, change this variable to 100. To use 1 = 100cm, change
        /// this variable to 1.
        /// </summary>
        [SerializeField]
        internal float globalScaleMetresToUnity = 1f; // TODO: will be moved to a settings class

        /// <summary>
        /// Sensor recording playback directory.
        /// </summary>
        [SerializeField]
        private string sensorPlaybackDir = "";

        /// <summary>
        /// Playback directory
        /// </summary>
        [SerializeField]
        protected string playbackDir;

        /// <summary>
        /// MetaContext MonoBehaviour container
        /// </summary>
        [SerializeField]
        protected MetaContextBridge _contextBridge;

        private EventHandlers _eventHandlers = new EventHandlers();
        
        #region MonoBehaviour Methods
        protected virtual void Awake()
        {
            new MetaPathVariables().AddPathVariables();
            var metaFactory = new MetaFactory(_dataAcquisitionSystem, gameObject, globalScaleMetresToUnity, playbackDir, sensorPlaybackDir);
            var package = metaFactory.ConstructAll();

            // Add Context
            SetContext(package.MetaContext);

            // Initialize events
            foreach (IEventReceiver eventReceiver in package.EventReceivers)
            {
                eventReceiver.Init(_eventHandlers);
            }

            _eventHandlers.RaiseOnAwake();
        }

        private void SetContext(IMetaContextInternal context)
        {
            // Get Context Bridge
            if (_contextBridge == null)
            {
                _contextBridge = gameObject.GetComponent<MetaContextBridge>();
                // If still null, add it
                if (_contextBridge == null)
                    _contextBridge = gameObject.AddComponent<MetaContextBridge>();
            }
            
            _contextBridge.SetContext(context);
        }

        protected virtual void Start()
        {
            _eventHandlers.RaiseOnStart();
        }

        private void Update()
        {
            CheckForShortcuts();
            _eventHandlers.RaiseOnUpdate();
        }

        private void FixedUpdate()
        {
            _eventHandlers.RaiseOnFixedUpdate();
        }

        private void LateUpdate()
        {
            _eventHandlers.RaiseOnLateUpdate();
        }

        private void OnDestroy()
        {
            _eventHandlers.RaiseOnDestroy();
        }

        private void OnApplicationQuit()
        {
            _eventHandlers.RaiseOnApplicationQuit();
        }

        protected virtual void CheckForShortcuts()
        {
            
        }
        #endregion

        [System.Obsolete("metaContext is Obsolete. Please use MetaContextBridge.CurrentContext.")]
        public IMetaContext metaContext
        {
            get { return _contextBridge.CurrentContext; }
        }
    }

}