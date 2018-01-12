using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine;

namespace Meta
{
    internal class MetaSensors : IEventReceiver
    {
        /// <summary>
        /// DLL call to start the camera, IMU and the Metavision DLL
        /// </summary>
        [DllImport("MetaVisionDLL", EntryPoint = "initMetaVisionCamera")]
        internal static extern int InitMetaVisionCamera(DataAcquisitionSystem iDAQ, ref DeviceInfo cameraInfo, IMUModel imuModel);

        /// <summary>
        /// DLL call to start the playback using specfied playback directory.
        /// </summary>
        [DllImport("MetaVisionDLL", EntryPoint = "initMetaVisionPlayback")]
        internal static extern int InitMetaVisionPlayback([MarshalAs(UnmanagedType.BStr)]string sensorPlaybackPath);

        ///<summary>
        /// DLL call to stop the Metavision DLL, camera and IMU
        ///</summary>
        [DllImport("MetaVisionDLL", EntryPoint = "deinitMeta")]
        internal static extern void DeinitMeta();

        /// <summary>
        /// Retrieve the device information. This is only for DS325.
        /// </summary>
        /// <param name="cameraInfo">DS325 device information data structure.</param>
        /// <returns>true if camera ready; false if not.</returns>
        [DllImport("MetaVisionDLL", EntryPoint = "GetDeviceInfo")]
        internal static extern bool GetDeviceInfo(ref DeviceInfo cameraInfo);

        /// <summary>
        /// Retrieve the connection information for specified sensor.
        /// </summary>
        /// <param name="iSensorType">Meta2 sensor types.</param>
        /// <param name="iSensorId">sensor id number within type; should be 0 except for left monochrome camera where it is 1.</param>
        /// <param name="oIsConnected">has this sensor been connected by the driver?</param>
        /// <param name="oIsInitialized">is this sensor delivering data?</param>
        [DllImport("MetaVisionDLL", EntryPoint = "getSensorConnectionInfo")]
        internal static extern void GetSensorConnectionInfo(SensorType iSensorType, UInt32 iSensorId, out bool oIsConnected, out bool oIsInitialized);

        /// <summary>
        /// Enables the virtual webcam feed. 
        /// </summary>
        [DllImport("MetaVisionDLL", EntryPoint = "enableVirtualWebcam")]
        internal static extern void EnableVirtualWebcam();

        ///<summary>
        ///Gets RGB data and writes to the given ptr.
        ///</summary>
        ///
        ///<param name="buffer"> The buffer - size should be 1280x720x3 bytes.  Preferably use unmanaged memory allocated by Marshal class.</param>
        [DllImport("MetaVisionDLL", EntryPoint = "getRGB")]
        internal static extern void GetRGB(IntPtr buffer);

        private DeviceInfo _deviceInfo;
        private DataAcquisitionSystem _dataAcquisitionSystem;
        private string _sensorPlaybackPath;

        public MetaSensors(DeviceInfo deviceInfo, DataAcquisitionSystem dataAcquisitionSystem, string sensorPlaybackPath)
        {
            _deviceInfo = deviceInfo;
            _dataAcquisitionSystem = dataAcquisitionSystem;
            _sensorPlaybackPath = sensorPlaybackPath;
        }

        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnAwake(Awake);
            eventHandlers.SubscribeOnDestroy(OnDestroy);
        }

        private void Awake()
        {
            if (_dataAcquisitionSystem == DataAcquisitionSystem.Playback)
            {
                if (_sensorPlaybackPath == null)
                    throw new Exception("No playback path specified!");
                if (!Directory.Exists(_sensorPlaybackPath))
                    throw new Exception("Directory \"" + _sensorPlaybackPath + "\" does not exist!");
                InitMetaVisionPlayback(_sensorPlaybackPath);
            }
            else
            {
                InitMetaVisionCamera(_dataAcquisitionSystem, ref _deviceInfo, IMUModel.UnknownIMU);
            }
            EnableVirtualWebcam();
        }

        public void GetConnectionInfo(SensorType iType, UInt32 iId, out bool oIsConnected, out bool oIsInitialized)
        {
            GetSensorConnectionInfo(iType, iId, out oIsConnected, out oIsInitialized);
        }

        public bool GetDeviceInfoValid()
        {
            return GetDeviceInfo(ref _deviceInfo);
        }

        private void OnDestroy()
        {
            DeinitMeta();
        }
    }
}