using UnityEngine;
using System.Collections;

namespace Meta
{
    public class LookAt : MonoBehaviour
    {

        public Transform target;

        // Update is called once per frame
        void LateUpdate()
        {
            if (!target)
            {
                var sc = GameObject.Find("StereoCameras");
                if (sc) target = sc.transform;
            }
            if (!target)
            {
                var mc = Camera.main;
                if (mc) target = Camera.main.transform;
            }

            transform.LookAt(target);
            transform.rotation *= Quaternion.Euler(180, 0, 0);
        }
    }

}
