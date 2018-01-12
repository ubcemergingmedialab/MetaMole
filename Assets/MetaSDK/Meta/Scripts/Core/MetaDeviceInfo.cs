
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Meta
{

    ///Information about the meta device.
    public class MetaDeviceInfo
    {

        /// <summary>
        /// Will retrive device serial number and calibration xml.
        /// <paramref name="serial"/>
        /// <paramref name="xml"/>
        /// </summary>
        [DllImport("MetaVisionDLL", EntryPoint = "getSerialNumberAndCalibration")]
        internal static extern bool GetSerialNumberAndCalibration([MarshalAs(UnmanagedType.BStr), Out] out string serial, [MarshalAs(UnmanagedType.BStr), Out] out string xml);


    }

}