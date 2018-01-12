using UnityEngine;
using System.Collections;

namespace Meta
{
    public class LerpTowards : MonoBehaviour
    {

        public Transform target;
        public float lerpSpeed = 2;

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * lerpSpeed);
        }
    }

}
