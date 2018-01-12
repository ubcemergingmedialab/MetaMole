using UnityEngine;

namespace Meta.Extensions
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Get the target Vector2 with the absolute value of both the x and y components
        /// </summary>
        /// <param name="vector2">Target vector</param>
        /// <returns>Vector2 with absolute values for both components</returns>
        public static Vector2 Abs(this Vector2 vector2)
        {
            vector2.x = Mathf.Abs(vector2.x);
            vector2.y = Mathf.Abs(vector2.y);
            return vector2;
        }

        /// <summary>
        /// Get the largest component of a Vector2
        /// </summary>
        /// <param name="vector2">Target vector</param>
        /// <returns>The value of x if x is greater than or equal to y. The value of y otherwise.</returns>
        public static float LargestComponent(this Vector2 vector2)
        {
            float largest = vector2.x;
            if (vector2.y > largest)
            {
                largest = vector2.y;
            }
            return largest;
        }

        /// <summary>
        /// Get the smallest component of a Vector2
        /// </summary>
        /// <param name="vector2">Target vector</param>
        /// <returns>The value of x if x is less than or equal to y. The value of y otherwise.</returns>
        public static float SmallestComponent(this Vector2 vector2)
        {
            float smallest = vector2.x;
            if (vector2.y < smallest)
            {
                smallest = vector2.y;
            }
            return smallest;
        }

        /// <summary>
        /// Checks if all three components of the two Vector are approximately equal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Approximately(this Vector2 a, Vector2 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
        }


        public static bool IsNaN(this Vector2 vector)
        {
            return float.IsNaN(vector.x) || float.IsNaN(vector.y);
        }
    }
}