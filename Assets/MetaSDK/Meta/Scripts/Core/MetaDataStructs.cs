using System.Runtime.InteropServices;

namespace Meta
{
    /// <summary>
    /// Basic info from the camera
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal struct DeviceInfo
    {
        public int colorHeight, colorWidth;
        public int depthHeight, depthWidth;
        public bool streamingColor, streamingDepth;
        public float depthFps;
        public float colorFps;
        public CameraModel cameraModel;
        public IMUModel imuModel;
    };

    internal enum CameraModel
    {
        UnknownCamera = -1,
        DS325 = 0,
        DS535 = 1
    };

    internal enum IMUModel
    {
        UnknownIMU = -1,
        MPU9150Serial = 0,
        MPU9150HID = 1
    };

    /// Values that represent data acquisition systems.
    internal enum DataAcquisitionSystem
    {
        /// Unknown data acquisition system
        Playback = 0,
        /// Generic DAQ.  Configured by configuration file
        //Generic = 1,
        /// Meta1 glasses.
        //Meta1 = 2,
        /// Reserved for final sensor configuration for Galileo1.
        //Galileo1 = 3,
        /// Meta1 glasses using new configurable producer ala Galileo.
        //GalileoDS325 = 4,
        /// Legacy DAQ
        //Legacy = 5,
        /// Demo DAQ
        //Demo = 6,
        /// Bob DAQ
        //Bob = 7,
        /// DVT1 DAQ
        //DVT1 = 8,
        /// DVT_Mono DAQ
        //DVT1_Mono = 9,
        /// Bob2 DAQ
        //Bob2 = 10,
        /// DVT2 DAQ
        //DVT2 = 12,
        /// DVT1_Mono_PMDv2 DAQ
        //DVT1_Mono_PMD_V2 = 13,
        /// DVT1 single endpoint devices
        //DVT1Single = 14,
        /// DVT1 mono single endpoint device
        //DVT1_MonoSingle = 15,
        /// DVT1 mono single endpoint with PMD v2 USB
        //DVT1_MonoSinglePMD_V2 = 16,
        /// DVT2_IMU DAQ
        //DVT2_IMU = 17,
        /// DVT3
        DVT3 = 19,
        //Handles the base frequency shift in the PMD depth camera
        DVT351 = 20 
    };

}