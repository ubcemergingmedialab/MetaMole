using Object = UnityEngine.Object;

namespace Meta
{
    /// <summary>
    /// This class bridges Kernel and Coco. It waits for the sensors to come up, 
    /// then instructs Kernel to initialize Coco.
    /// </summary>
    internal class KernelCocoLauncherModule : IEventReceiver
    {
        private bool _sensorsInitialized = false;
        private bool _initializedHandsModule = false;

        private void Update()
        {
            if (!_sensorsInitialized)
            {
                Internal.SensorMetaData sensorMetaData = new Internal.SensorMetaData();
                if (HandKernelInterop.GetSensorMetaData(ref sensorMetaData))
                {
                    OnSensorInitialized();
                    _sensorsInitialized = true;
                }
            }
            else if (!_initializedHandsModule)
            {
                var context = Object.FindObjectOfType<MetaContextBridge>().CurrentContext;
                context.Get<HandsModule>().Initialized = true;
                _initializedHandsModule = true;
            }
        }

        private void OnApplicationQuit()
        {
            if (_sensorsInitialized)
            {
                MetaKernelCocoInterop.Stop();
            }
        }

        private void OnSensorInitialized()
        {
            MetaKernelCocoInterop.Start();
        }


        void IEventReceiver.Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnUpdate(Update);
            eventHandlers.SubscribeOnApplicationQuit(OnApplicationQuit);
        }
    }
}