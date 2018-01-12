using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Information about a point cloud
    /// </summary>
    public struct PointCloudInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly Vector3 Centroid;
        //TODO THIS MUST BE AN ORIENTED BOUNDS
        /// <summary>
        /// 
        /// </summary>
        public readonly Bounds Bounds;
        /// <summary>
        /// Distance from front of MetaEventVolume
        /// </summary>
        public readonly float FrontDistance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centroid"></param>
        /// <param name="bounds"></param>
        /// <param name="frontDistance"></param>
        public PointCloudInfo(Vector3 centroid, Bounds bounds, float frontDistance)
        {
            Centroid = centroid;
            Bounds = bounds;
            FrontDistance = frontDistance;
        }
    }
}