using UnityEngine;

namespace Meta
{
    /// <summary>  A data point with xyz data. </summary>
    public class PointXYZ
    {
        /// <summary>   The vertex. </summary>
        protected Vector3 _vertex;
    
        /// <summary>   Gets the vertex. </summary>
        /// <value> The vertex. </value>
        public Vector3 vertex
        {
            get { return _vertex; }
            internal set { _vertex = value; }
        }

        /// <summary>   Initializes a new instance of the PointXYZ class. </summary>
        public PointXYZ() {}

    
        /// <summary>   Initializes a new instance of the PointXYZ class. </summary>
        /// <param name="vert"> The vertex. </param>
        public PointXYZ(Vector3 vert)
        {
            vertex = vert;
        }

    
        /// <summary>   Sets data from raw bytes. </summary>
        /// <param name="data">         The data. </param>
        /// <param name="startIndex">   The start index. </param>
        /// <param name="size">         The size. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
        public virtual bool SetDataFromRawBytes(float[] data, int startIndex, int size)
        {
            vertex.Set(data[size * startIndex + 0], data[size * startIndex], data[size * startIndex]);
            return true;
        }

    
        /// <summary>   Convert this point data into a string representation. </summary>
        /// <returns>   A string that represents this object. </returns>
        public override string ToString()
        {
            return vertex.x + " " + vertex.y + " " + vertex.z;
        }



        public static implicit operator Vector3(PointXYZ point)
        {
            return point.vertex;
        }
    }
}
