using UnityEngine;
using System.Collections;

namespace Meta
{
    public static class TransformExtensions
    {
        public static Vector3 LocalTranslatedWorldPosition(this Transform transform, Vector3 localTranslation)
        {
            return transform.position + transform.rotation * localTranslation;
        }

        public static void LookAtY(this Transform transform, Vector3 position)
        {
            float relativeX = transform.position.x - Camera.main.transform.position.x;
            float relativeZ = transform.position.z - Camera.main.transform.position.z;
            float angle = Mathf.Atan2(relativeX, relativeZ) * Mathf.Rad2Deg;

            Vector3 newEuler = transform.eulerAngles;
            newEuler.y = angle;
            newEuler.z = 0;
            transform.eulerAngles = newEuler;
        }
    }
}
