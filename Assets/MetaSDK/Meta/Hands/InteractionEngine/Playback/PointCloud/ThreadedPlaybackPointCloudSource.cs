using System;


namespace Meta.Internal.Playback
{
    /// <summary>
    /// Point cloud source using threaded parsing of XYZ Confidence PCD files.
    /// </summary>
    internal class ThreadedPlaybackPointCloudSource : ThreadedDirectoryPlayback<PointCloudData<PointXYZConfidence>>, IPointCloudSource<PointXYZConfidence>
    {
        private readonly object _cloudLock;

        public ThreadedPlaybackPointCloudSource() : base()
        {
            _cloudLock = new object();
            _parser = new PCDParserXYZC();
        }

        public ThreadedPlaybackPointCloudSource(string playbackFolder) : base(playbackFolder, "*.pcd")
        {
            _cloudLock = new object();
            _parser = new PCDParserXYZC();
        }

        #region Interaction Engine API

        public void DeinitPointCloudSource()
        {
        }

        public void InitPointCloudSource()
        {
            if (_playbackFolder != null)
            {
                LoadFrameFiles();
            }
        }

        public bool SetPointCloudGeneratorOptions(CloudGeneratorOptions cloudGeneratorOptions)
        {
            return true;
        }

        public bool GetPointCloudMetaData(ref PointCloudMetaData pointCloudMetaData)
        {
            lock (_cloudLock)
            {
                if (_currFrame != null)
                {
                    if (pointCloudMetaData == null)
                    {
                        pointCloudMetaData = new PointCloudMetaData();
                    }
                    _currFrame.metaData.CopyTo(ref pointCloudMetaData);
                    return true;
                }
                return false;
            }
        }

        public bool GetPointCloudData(ref PointCloudData<PointXYZConfidence> pointCloudData)
        {
            lock (_cloudLock)
            {
                if (_currFrame != null && pointCloudData != null)
                {
                    _currFrame.CopyTo(ref pointCloudData);
                    return true;
                }
                return false;
            }
        }

        public void SetPointCloudDataFromInteropData(PointCloudData<PointXYZConfidence> pcd, PointCloudMetaData metadata)
        {
            throw new NotImplementedException();
        }

        public OnNewFrameData<PointXYZConfidence> OnNewFrameData { get; set; }

        #endregion
    }
}