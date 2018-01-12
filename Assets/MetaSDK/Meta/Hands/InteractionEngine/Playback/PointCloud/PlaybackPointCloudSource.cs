using System;


namespace Meta.Internal.Playback
{
    internal class PlaybackPointCloudSource : GenericDirectoryPlayback<PointCloudData<PointXYZ>>, IPointCloudSource<PointXYZ>
    {

        private readonly object _cloudLock;

        public PlaybackPointCloudSource(string playbackFolder) : base(playbackFolder, "*.pcd")
        {
            _cloudLock = new object();
            _parser = new PCDParser<PointXYZ>();
        }

        #region Interaction Engine API

        public bool SetDepthCloudOpions(CloudGeneratorOptions cloudGeneratorOptions)
        {
            return true;
        }

        public void InitPointCloudSource()
        {
            if (HasValidSource())
            {
                LoadFrameFiles();
            }
        }

        public bool GetPointCloudData(ref PointCloudData<PointXYZ> pointCloudData)
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

        public void DeinitPointCloudSource()
        {
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

        public void SetPointCloudDataFromInteropData(PointCloudData<PointXYZ> pcd, PointCloudMetaData metadata)
        {
            throw new NotImplementedException();
        }

        public OnNewFrameData<PointXYZ> OnNewFrameData { get; set; }

        #endregion
    }
}