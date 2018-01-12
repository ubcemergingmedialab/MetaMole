using UnityEngine;
using System.Collections;

namespace Meta
{
    public class AlignToHeadset : MonoBehaviour
    {

        public Transform stereoCameras;
        public float xAngle = 10f;

        public Vector3 offset = new Vector3(0, -.2f, .5f);

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (stereoCameras == null)
                stereoCameras = GameObject.Find("StereoCameras").transform;

            var forward = Vector3.Cross(Vector3.up, -stereoCameras.right).normalized;
            transform.position = stereoCameras.position + forward * offset.z + Vector3.up * offset.y;
            transform.eulerAngles = new Vector3(xAngle, stereoCameras.eulerAngles.y, 0);
        }
    }

}
