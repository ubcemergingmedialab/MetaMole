using UnityEngine;
using System.Collections;

namespace Meta
{
    public class RandomRotation : MonoBehaviour
    {

        public Vector3 rotationPerSecond;
        public float lerp = 0;
        public Vector3 rotationPerSecond2;
        
        /// <summary>
        /// Whether to use timescale or unscaled time
        /// </summary>
        public bool unscaledTime = true;

        [ContextMenu("Randomize")]
        void Randomize()
        {
            rotationPerSecond = new Vector3(Random.value, Random.value, Random.value) * 90;
        }

        [ContextMenu("Randomize2")]
        void Randomize2()
        {
            rotationPerSecond2 = new Vector3(Random.value, Random.value, Random.value) * 270;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(UnclampedLerp(rotationPerSecond, rotationPerSecond2, lerp) * (unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime));
        }

        // TODO: Move to some utility class
        public static Vector3 UnclampedLerp(Vector3 v1, Vector3 v2, float value)
        {
            return new Vector3(v1.x + (v2.x - v1.x) * value,
                                v1.y + (v2.y - v1.y) * value,
                                v1.z + (v2.z - v1.z) * value);
        }
    }

}
