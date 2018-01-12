using UnityEngine;
using System.Collections;

namespace Meta
{
    public class SLAMParticles : MonoBehaviour
    {

        [Header("Object References")]
        public SLAMUIManager slamManager;
        public ParticleSystem[] systems;
        public RandomRotation[] chain;
        public GameObject emitterSpecificGO;
        Transform emitter { get { return this.transform; } }

        [Header("Smoothness and Scale")]
        public float lerpSpeed = 8;
        public float minScale = 0.5f, maxScale = 1.0f;
        public float crazyFactor = 2f;

        // internal values

        [HideInInspector]
        public Transform emitterTarget;
        Vector3 emitterTargetPosition = Vector3.zero;
        Quaternion emitterTargetRotation = Quaternion.identity;

        bool stopped = false;
        bool needsFirstValue = false;
        float scaleFactor = 0;
        float lerpFactor = 0;

        public void DoStart()
        {
            gameObject.SetActive(true);
            
            StartCoroutine(_DoStart());
        }
        IEnumerator _DoStart()
        {
            foreach (var ps in systems)
            {
                ps.Stop();
            }

            stopped = true;


            yield return null;

            crazyMode = false;
            needsFirstValue = true;
            stopped = false;
            lerpFactor = 0;
        }

        public void DoStop()
        {
            stopped = true;
        }

        bool crazyMode = false;
        public void GoCrazy()
        {
            crazyMode = true;
        }

        // Update is called once per frame
        void Update()
        {
            /*
            foreach(var ps in systems)
            {
                // timescale-independent particle system!
                if(ps.isPlaying)
                    ps.Simulate(Time.unscaledDeltaTime, true, false);
            }
            */

            if (stopped)
            {
                foreach (var ps in systems)
                {
                    if (ps.isPlaying)
                        ps.Stop();
                }

                return;
            }

            emitterTarget = slamManager.currentTarget;

            if (emitterTarget != null)
            {
                emitterTargetPosition = emitterTarget.position;
                emitterTargetRotation = emitterTarget.rotation;
            }

            if (needsFirstValue)
            {
                emitter.position = emitterTargetPosition;
                emitter.rotation = emitterTargetRotation;
                scaleFactor = minScale;
                lerpFactor = 1;
                needsFirstValue = false;
            }

            if (emitterSpecificGO)
                emitterSpecificGO.SetActive(emitterTarget != null);

            foreach (var ps in systems)
            {
                if ((slamManager.timeSinceLastPoint < 1) && (emitterTarget != null))
                {
                    if (!ps.isPlaying)
                        ps.Play();
                }
            }

            emitter.position = Vector3.Lerp(emitter.position, emitterTargetPosition, lerpSpeed * Time.deltaTime);
            emitter.rotation = Quaternion.Lerp(emitter.rotation, emitterTargetRotation, lerpSpeed);

            scaleFactor = Mathf.Lerp(scaleFactor, MapClamp(slamManager.timeSinceLastPoint, 0, 2, maxScale, minScale), Time.deltaTime * 2);
            transform.GetChild(0).localScale = Vector3.one * scaleFactor;

            // scale the star in the opposite direction to make it still visible
            emitterSpecificGO.transform.localScale = Vector3.one * 1000f / scaleFactor;

            lerpFactor = Mathf.Lerp(lerpFactor, !crazyMode ? MapClamp(slamManager.timeSinceLastPoint, 1, 2, 0, 1) : crazyFactor, Time.deltaTime * 6);
            if (chain != null)
            {
                foreach (var randomRotator in chain)
                {
                    randomRotator.lerp = lerpFactor;
                }
            }
        }


        // TODO: Move to some utility class
        float Map(float val, float srcMin, float srcMax, float dstMin, float dstMax)
        {
            return (val - srcMin) / (srcMax - srcMin) * (dstMax - dstMin) + dstMin;
        }

        // TODO: Move to some utility class
        float MapClamp(float val, float srcMin, float srcMax, float dstMin, float dstMax)
        {
            return Mathf.Clamp(Map(val, srcMin, srcMax, dstMin, dstMax), Mathf.Min(dstMin, dstMax), Mathf.Max(dstMin, dstMax));
        }
    }


}