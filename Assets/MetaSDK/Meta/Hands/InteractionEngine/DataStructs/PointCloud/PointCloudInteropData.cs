using System;
using System.Runtime.InteropServices;

namespace Meta
{
    /// <summary>   A point cloud interop data. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct PointCloudInteropData
    {
        /// <summary>   Size of a single point. </summary>
        /// todo: deprecate this (use from metadata)
        public int pointSize;

        /// <summary>   The size of the point cloud. </summary>
        public int size;

        /// <summary>   The height of the depth image from which the point cloud is made. </summary>
        public int height;

        /// <summary>   The width of the depth image from which the point cloud is made. </summary>
        public int width;

        /// <summary>   The pointer to the data. </summary>
        public IntPtr data;

        /// <summary>   The view point position of the pointcloud. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] viewPointPosition;

        /// <summary>   The view point rotation of the point cloud. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] viewPointRotation;

        /// <summary>   The timestamp for the arrival of clean sensor data in the point cloud generator. </summary>
        public long arrivalOfCleanSensorDataTimeStamp;

        /// <summary>   The timstamp at the completion of point cloud generation. </summary>
        public long completionOfPointCloudGenerationTimeStamp;

        /// <summary>   Identifier for the frame from which the point cloud was made. </summary>
        public int frameID;

        /// <summary>   true if hte point cloud had valid data. </summary>
        public bool valid;

    }
}