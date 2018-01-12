

namespace Meta
{
    public delegate void OnNewFrameData<T>(PointCloudData<T> pointCloudData) where T : PointXYZ, new();
}