using UnityEngine;

namespace Meta.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Get the target Vector3 with the absolute value of both the x and y components
        /// </summary>
        /// <param name="vector3">Target vector</param>
        /// <returns>Vector3 with absolute values for all components</returns>
        public static Vector3 Abs(this Vector3 vector3)
        {
            vector3.x = Mathf.Abs(vector3.x);
            vector3.y = Mathf.Abs(vector3.y);
            vector3.z = Mathf.Abs(vector3.z);
            return vector3;
        }

        /// <summary>
        /// Clamp all components of the vector between the min and max values
        /// </summary>
        /// <param name="vector3">Target vector</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3 Clamp(this Vector3 vector3, float min, float max)
        {
            vector3.x = Mathf.Clamp(vector3.x, min, max);
            vector3.y = Mathf.Clamp(vector3.y, min, max);
            vector3.z = Mathf.Clamp(vector3.z, min, max);
            return vector3;
        }

        /// <summary>
        /// Get the largest component of a Vector3
        /// </summary>
        /// <param name="vector3">Target vector</param>
        /// <returns>The value of x if x is greater than or equal to y and z. The value of y if it is greater than x and greater 
        /// than or equal to z. z otherwise.</returns>
        public static float LargestComponent(this Vector3 vector3)
        {
            float largest = vector3.x;
            if (vector3.y > largest)
            {
                largest = vector3.y;
            }
            if (vector3.z > largest)
            {
                largest = vector3.z;
            }
            return largest;
        }

        /// <summary>
        /// Get the smallest component of a Vector3
        /// </summary>
        /// <param name="vector3">Target vector</param>
        /// <returns>The value of x if x is less than or equal to y and z. The value of y if it is less than x and less 
        /// than or equal to z. z otherwise.</returns>
        public static float SmallestComponent(this Vector3 vector3)
        {
            float smallest = vector3.x;
            if (vector3.y < smallest)
            {
                smallest = vector3.y;
            }
            if (vector3.z < smallest)
            {
                smallest = vector3.z;
            }
            return smallest;
        }

        /// <summary>
        /// Checks if all three components of the two Vector are approximately equal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Approximately(this Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }

        /// <summary>
        /// Get a Vector2 with the x and y components of a Vector3
        /// </summary>
        /// <param name="vector3">Target vector</param>
        /// <returns>A Vector2 with the x and y components of the target vector</returns>
        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 Parse(string x, string y, string z)
        {
            return new Vector3(float.Parse(x), float.Parse(y), float.Parse(z));
        }
		
		public static Vector3 Add(this Vector3 a, float b)
        {
            return new Vector3(a.x + b, a.y + b, a.z + b);
        }

        public static bool IsNaN(this Vector3 vector)
        {
            return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
        }
    }
}