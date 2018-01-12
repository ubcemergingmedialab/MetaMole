using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Meta
{
    using System;

    /// <summary>   A class to describe the meta data of a point cloud. </summary>
    public class PointCloudMetaData
    {
        /// <summary>   The view point position of the point cloud. </summary>
        private float[] _viewPointPosition = new float[3];

        /// <summary>   The view point rotation of the point cloud. </summary>
        private float[] _viewPointRotation = new float[4];
        /// <summary>   Point Cloud DataType field. </summary>
        public PointCloudDataType field = PointCloudDataType.UNDEFINED;

        /// <summary>   Number of fields. </summary>
        public int[] fieldCount;

        /// <summary>   Names of the fields. </summary>
        public string[] fieldName;

        /// <summary>   Size of the fields. </summary>
        public int[] fieldSize;

        /// <summary>   Types of the fields. </summary>
        public char[] fieldType;

        /// <summary>   The height of the depth data from which the point cloud is generated. </summary>
        public int height;

        /// <summary>   Maximum size of the point cloud. </summary>
        public int maxSize;

        /// <summary>   The number of fields. </summary>
        public int numFields;

        /// <summary>   Size of a datapoint in the pointcloud. </summary>
        public int pointSize;

        /// <summary>   The width of the depth data from which the point cloud is generated. </summary>
        public int width;

    
        /// <summary>   Gets or sets the view point position. </summary>
        /// <value> The view point position. </value>
        public float[] viewPointPosition
        {
            get { return _viewPointPosition; }
            set { _viewPointPosition = value; }
        }

    
        /// <summary>   Gets or sets the view point rotation. </summary>
        /// <value> The view point rotation. </value>
        public float[] viewPointRotation
        {
            get { return _viewPointRotation; }
            set { _viewPointRotation = value; }
        }

        /// <summary>   Initializes a new instance of the Meta.PointCloudMetaData class. </summary>
        public PointCloudMetaData()
        {
            ResetFields();
        }


        /// <summary>   Initializes a new instance of the Meta.PointCloudMetaData class. </summary>
        /// <param name="pointCloudInteropMetaData">
        ///     Information describing the point cloud interop
        ///     meta.
        /// </param>
        public PointCloudMetaData(PointCloudInteropMetaData pointCloudInteropMetaData)
        {
            fieldType = new char[pointCloudInteropMetaData.fieldLength];
            fieldSize = new int[pointCloudInteropMetaData.fieldLength];
            fieldCount = new int[pointCloudInteropMetaData.fieldLength];
            numFields = pointCloudInteropMetaData.fieldLength;
            maxSize = pointCloudInteropMetaData.maxSize;
            Array.Copy(pointCloudInteropMetaData.fieldType, fieldType, pointCloudInteropMetaData.fieldLength);
            Array.Copy(pointCloudInteropMetaData.fieldSize, fieldSize, pointCloudInteropMetaData.fieldLength);
            Array.Copy(pointCloudInteropMetaData.fieldCount, fieldCount, pointCloudInteropMetaData.fieldLength);
            string input = new string(pointCloudInteropMetaData.fieldName);
            fieldName = Regex.Split(input, " ");

            //hack
            field = PointCloudDataType.XYZCONFIDENCE; //todo: actually make it generic 

            //end hack

            //todo: Make this a generic funtion
            for (int i = 0; i < numFields; i++)
            {
                pointSize += fieldCount[i] * fieldSize[i];
            }
        }

    
        /// <summary>   Initializes a new instance of the Meta.PointCloudMetaData class. </summary>
        /// <param name="pointCloudMetaData">   Information describing the point cloud meta. </param>
        public PointCloudMetaData(PointCloudMetaData pointCloudMetaData)
        {
            ResetFields();
            numFields = pointCloudMetaData.numFields;
            InitializeDataFields();
        }

        /// <summary>   Resets the fields. </summary>
        private void ResetFields()
        {
            numFields = 0;
            fieldName = null;
            fieldType = null;
            fieldCount = null;
            fieldSize = null;

            maxSize = 0;
            pointSize = 0;

            height = 0;
            width = 0;

            _viewPointPosition[0] = _viewPointPosition[1] = _viewPointPosition[2] = 0;
            _viewPointRotation[0] = _viewPointRotation[1] = _viewPointRotation[2] = _viewPointRotation[3] = 0;
        }

    
        /// <summary>   Manual checking if data is valid. </summary>
        /// <returns>   True, if PCD metadata is well formed. </returns>
        public bool IsValid()
        {
            bool result = true;

            if ((fieldName.Length != numFields) || (fieldCount.Length != numFields) || (fieldType.Length != numFields))
            {
                result = false;
            }

            if ((field == PointCloudDataType.UNDEFINED) || (numFields == 0) || (pointSize == 0))
            {
                result = false;
            }

            return result;
        }

    
        /// <summary>   Sizeofs the given field. </summary>
        /// <param name="field">    Point Cloud DataType field. </param>
        /// <returns>   An int. </returns>
        public int Sizeof(char field)
        {
            throw new NotImplementedException();
        }

    
        /// <summary>
        ///     Initializes arrays for metadata based on the fields.
        ///     Used by the point cloud reader.
        /// </summary>
        public void InitializeDataFields()
        {
            fieldName = new string[numFields];
            fieldType = new char[numFields];
            fieldCount = new int[numFields];
            fieldSize = new int[numFields];
        }

    
        /// <summary>   Convert View point to string. </summary>
        /// <returns>   A string. </returns>
        public string ConvertViewPointToString()
        {
            return _viewPointPosition[0] + " " + _viewPointPosition[1] + " " + _viewPointPosition[2] + " " + viewPointRotation[0] + " " + viewPointRotation[1] + " " + viewPointRotation[2] + " " +
                   viewPointRotation[3];
        }

    
        /// <summary>   Copies to described by pointCloudMetaData. </summary>
        /// <param name="pointCloudMetaData">   Information describing the point cloud meta. </param>
        public void CopyTo(ref PointCloudMetaData pointCloudMetaData)
        {
            pointCloudMetaData.maxSize = maxSize;
            pointCloudMetaData.pointSize = pointSize;
            pointCloudMetaData.field = field;
            pointCloudMetaData.height = height;
            pointCloudMetaData.width = width;     // This should be the number of points in the file
            pointCloudMetaData.numFields = numFields;
            pointCloudMetaData.field = field;
            if (pointCloudMetaData.fieldCount == null)
            {
                pointCloudMetaData.InitializeDataFields();

                //todo: fix this scenario. These field should never be un initialized (or should be initializer more elegantly)
                //Removed warning thrown - YG
                //UnityEngine.Debug.LogWarning("This data is not initialized.");
            }

            Array.Copy(fieldType, pointCloudMetaData.fieldType, numFields);
            Array.Copy(fieldSize, pointCloudMetaData.fieldSize, numFields);
            Array.Copy(fieldCount, pointCloudMetaData.fieldCount, numFields);
            Array.Copy(fieldName, pointCloudMetaData.fieldName, numFields);
        }

        public void SetViewpointPosition(float x, float y, float z)
        {
            _viewPointPosition[0] = x;
            _viewPointPosition[1] = y;
            _viewPointPosition[2] = z;
        }

        public void SetViewpointRotation(float x, float y, float z, float w)
        {
            _viewPointRotation[0] = x;
            _viewPointRotation[1] = y;
            _viewPointRotation[2] = z;
            _viewPointRotation[3] = w;
        }
    }
}
