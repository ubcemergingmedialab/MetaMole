

namespace Meta
{

    /// <summary>   Interface for point cloud source. </summary>
    /// <typeparam name="TPoint">   Type of the point. </typeparam>

    public interface IPointCloudSource <TPoint> where TPoint : PointXYZ, new()
    {
        /// <summary>   Initialises the point cloud source. </summary>
        void InitPointCloudSource();

    
        /// <summary>   Sets point cloud generator options. </summary>
        /// <param name="cloudGeneratorOptions">    Options for controlling the cloud generator. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
    
        bool SetPointCloudGeneratorOptions(CloudGeneratorOptions cloudGeneratorOptions);

    
        /// <summary>   Gets point cloud meta data. </summary>
        /// <param name="pointCloudMetaData">   Information describing the point cloud meta. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
    
        bool GetPointCloudMetaData(ref PointCloudMetaData pointCloudMetaData);


        /// <summary>   The new point cloud data event handler. </summary>
        OnNewFrameData<TPoint> OnNewFrameData { get; set; }


        /// <summary>   Gets point cloud data. </summary>
        /// <param name="pointCloudData">   Information describing the point cloud. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>

        bool GetPointCloudData(ref PointCloudData<TPoint> pointCloudData);

        /// <summary>   Deinitialize the point cloud source. </summary>
        void DeinitPointCloudSource();

        void SetPointCloudDataFromInteropData(PointCloudData<TPoint> pcd, PointCloudMetaData metadata);
    }
}
