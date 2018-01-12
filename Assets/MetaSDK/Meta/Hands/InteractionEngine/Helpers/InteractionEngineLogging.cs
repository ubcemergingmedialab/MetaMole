using Meta.Internal;
using UnityEngine;

namespace Meta
{

    /// <summary>   A class for logging the data from interaction engine . </summary>
    ///
    /// <seealso cref="T:Meta.MetaBehaviour"/>

    internal class InteractionEngineLogging : MetaBehaviour
    {
        /// <summary>   The point cloud data logging class. </summary>
        private PointCloudDataLogging _pointCloudDataLogging;

        /// <summary>   The sensor data recorder. </summary>
        private RecordSensorData _sensorDataRecorder;

        /// <summary>   true to log in record sesnor data folder. </summary>
        public bool m_logInRecordSesnorDataFolder = false;

        /// <summary>   Pathname of the logging folder. </summary>
        public string m_loggingFolder;

        /// <summary>   Information describing the point cloud. </summary>
        private PointCloudData<PointXYZConfidence> _pointCloudData;

        /// <summary>   Information describing the point cloud meta. </summary>
        private PointCloudMetaData _pointCloudMetaData;

        /// <summary>   The interaction engine. </summary>
        private InteractionEngine _interactionEngine;

        public void Start()
        {

            if (m_logInRecordSesnorDataFolder)
            {
                _sensorDataRecorder = GameObject.Find("MetaCameraRig").GetComponent<RecordSensorData>();
                if (_sensorDataRecorder != null)
                {
                    UnityEngine.Debug.LogError("cant find RecordsensorData");
                }
                m_loggingFolder = _sensorDataRecorder.GetRecordingPath();
            }
            if (m_loggingFolder == null)
            {
                UnityEngine.Debug.LogError("Logging Folder is null");
                return;
            }
            _pointCloudMetaData = new PointCloudMetaData();
            _pointCloudDataLogging = new PointCloudDataLogging(m_loggingFolder);
            _interactionEngine = metaContext.Get<InteractionEngine>();

        }

        public void Update()
        {
            if (_pointCloudData == null)
            {

                if (_interactionEngine.GetCloudMetaData(ref _pointCloudMetaData))
                {
                    _pointCloudData = new PointCloudData<PointXYZConfidence>(_pointCloudMetaData.maxSize);
                }
                else
                {
                    return;
                }
            }
            _interactionEngine.GetCloudData(ref _pointCloudData);
            _pointCloudDataLogging.Update(_pointCloudData);
        }
    }
}