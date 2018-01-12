using UnityEngine;

namespace Meta.Extensions
{
    public class QuaternionExtensions : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public static Quaternion Parse(string x, string y, string z, string w)
        {
            return new Quaternion(float.Parse(x), float.Parse(y), float.Parse(z), float.Parse(w));
        }
    }
}

