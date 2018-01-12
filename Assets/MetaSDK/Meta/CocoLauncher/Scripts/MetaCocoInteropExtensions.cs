using UnityEngine;

namespace Meta
{
    public static class MetaCocoInteropExtensions
    {
        /// <summary>
        /// Convertion util. 
        /// From: meta.types.Vec3T (flatbuffers type)
        /// To: UnityEngine.Vector3
        /// </summary>
        /// <param name="vec">meta.types.Vec3T (flatbuffers type) input </param>
        /// <returns>Converted UnityEngine.Vector3</returns>
        public static Vector3 ToVector3(this meta.types.Vec3T vec)
        {
            return new Vector3((float)vec.X, (float)vec.Y, (float)vec.Z);
        }

        /// <summary>
        /// Convertion util. 
        /// From: meta.types.Quaternion (flatbuffers type)
        /// To: UnityEngine.Quaternion
        /// </summary>
        /// <param name="vec">meta.types.Quaternion (flatbuffers type) input </param>
        /// <returns>Converted UnityEngine.Quaternion</returns>
        public static Quaternion ToQuaternion(this meta.types.Quaternion quat)
        {
            return new Quaternion((float)quat.X, (float)quat.Y, (float)quat.Z, (float)quat.W);
        }
    }
}