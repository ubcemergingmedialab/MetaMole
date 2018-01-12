using System;
using System.Globalization;
using UnityEngine;

namespace Meta
{

    /// <summary>   A point xyzrgba. </summary>
    /// <seealso cref="T:Meta.PointXYZ" />

    public class PointXYZRGBA : PointXYZ
    {
        /// <summary>   The color. </summary>
        private Color32 _color;

    
        /// <summary>   Gets the color. </summary>
        /// <value> The color. </value>
        public Color32 color
        {
            get { return _color; }
            internal set { _color = value; }
        }

        /// <summary>   Initializes a new instance of the Meta.PointXYZRGBA class. </summary>
        public PointXYZRGBA() {}

    
        /// <summary>   Initializes a new instance of the Meta.PointXYZRGBA class. </summary>
        /// <param name="new_vertex">   The new vertex data. </param>
        /// <param name="new_color">    The new color data. </param>
        public PointXYZRGBA(Vector3 new_vertex, Color32 new_color) : base(new_vertex)
        {
            _color = new_color;
        }

    
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
            /*todo: Convert last float into rgba data*/
            return false;
        }

    
        /// <summary>   Convert this object into a string representation. </summary>
        /// <returns>   A string that represents this object. </returns>
        /// <seealso cref="M:PointXYZ.ToString()" />
        public override string ToString()
        {
            return vertex.x + " " + vertex.y + " " + vertex.z + " " + ColorToString();
        }

        /// <summary>
        ///     Returns the color of this point as a string for writing.
        /// </summary>
        /// <returns></returns>
        public string ColorToString()
        {
            byte[] bytes = new byte[4];
            bytes[0] = _color.r;
            bytes[1] = _color.g;
            bytes[2] = _color.b;
            bytes[3] = _color.a;
            float packedColor = BitConverter.ToSingle(bytes, 0);

            // G5 notation for packedColor used to match the PCD example here:
            // http://pointclouds.org/documentation/tutorials/pcd_file_format.php
            return packedColor.ToString("G5", CultureInfo.InvariantCulture);
        }
    }
}
