using UnityEngine;

namespace Meta
{

    /// <summary>   A data point with xyz + confidence data. </summary>
    /// <seealso cref="T:PointXYZ" />

    public class PointXYZConfidence : PointXYZ
    {
        /// <summary>   The confidence data. </summary>
        protected float _confidence;

    
        /// <summary>   Gets the confidence. </summary>
        /// <value> The confidence. </value>
    
        public float confidence
        {
            get { return _confidence; }
            internal set { _confidence = value; }
        }

    
        /// <summary>   Initializes a new instance of the Meta.PointXYZConfidence class. </summary>
        /// <param name="vert">         The vertical. </param>
        /// <param name="confidence">   The confidence data. </param>
    
        public PointXYZConfidence(Vector3 vert, float confidence) : base(vert)
        {
            this.confidence = confidence;
        }

        /// <summary>   Initializes a new instance of the Meta.PointXYZConfidence class. </summary>
        public PointXYZConfidence() {}

    
        /// <summary>   Sets data from raw bytes. </summary>
        /// <param name="data">         The data. </param>
        /// <param name="startIndex">   The start index. </param>
        /// <param name="size">         The size. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
        /// <seealso cref="M:PointXYZ.SetDataFromRawBytes(float[],int,int)" />
    
        public override bool SetDataFromRawBytes(float[] data, int startIndex, int size)
        {
            _vertex.x = data[size * startIndex + 0];
            _vertex.y = data[size * startIndex + 1];
            _vertex.z = data[size * startIndex + 2];
            _confidence = data[size * startIndex + 3];
            return true;
        }

    
        /// <summary>   Convert this object into a string representation. </summary>
        /// <returns>   A string that represents this object. </returns>
        /// <seealso cref="M:PointXYZ.ToString()" />
    
        public override string ToString()
        {
            return vertex.x + " " + vertex.y + " " + vertex.z + " " + confidence;
        }
    }
}
