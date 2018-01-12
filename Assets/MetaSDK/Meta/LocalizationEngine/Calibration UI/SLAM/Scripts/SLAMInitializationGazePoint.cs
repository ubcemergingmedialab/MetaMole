using UnityEngine;
using System.Collections;
using System;

namespace Meta
{
    /// <summary>
    /// This object counts for how many seconds the user is looking at it. It disables itself automatically if the user is done.
    /// Controls color change/animation for visual purposes.
    /// TODO: This is mostly not needed if we go with the Lightband UI instead of the Arrows UI. TBD.
    /// </summary>
    public class SLAMInitializationGazePoint : MonoBehaviour
    {
        [HideInInspector]
        public SLAMUIManager slamUI;

        // public Transform gazeCursor;
        public Transform EyeCamera;
        public float maxViewAngle;
        public float normalizedDistance;
        public float lookAtTime = 0;
        public bool isGazedAt = false;

        public float leftTargetAngle = -100, rightTargetAngle = 100;
        public float slamPercentage = 0;

        public bool isDone;
        public bool allowGazing = true;

        public Transform r;

        // [HideInInspector]
        public float time;
        public int number = 0;

        MaterialPropertyBlock block;

        void Start()
        {
            allowGazing = false;
            lookAtTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (allowGazing)
                GazeAtPoint();

            ChangeColor();
        }
        
        public bool isActive = false;
        public float requiredLookAtTime = 2.0f;
        public int direction = 1;

        private float _internalTime = 0;

        void ChangeColor()
        {
            if(block == null) block = new MaterialPropertyBlock();
            
            _internalTime += Time.unscaledDeltaTime * slamUI.blinkSpeed;
            var blink = Mathf.Abs(((_internalTime - slamUI.direction * number * slamUI.blinkShift)) % 1f);
            
            var blinkDirectional = slamUI.fadingCurve.Evaluate(blink);
            
            var clampedLookAtFactor = 1 - Mathf.Clamp01(lookAtTime / requiredLookAtTime);

            block.SetColor("_Color", Color.Lerp(new Color(1,1,1,0), new Color(1,1,1,1), blinkDirectional * clampedLookAtFactor));
            r.GetComponent<Renderer>().SetPropertyBlock(block);
        }

        public void Init()
        {
            lookAtTime = 0;
            time = 0;
            isDone = false;
        }
        
        private void GazeAtPoint()
        {
            var eyeForwardVector = (EyeCamera.transform.forward).normalized;
            var objectForwardVector = (r.transform.position - EyeCamera.transform.position).normalized;

            eyeForwardVector.y = 0;
            objectForwardVector.y = 0;

            // check if we are looking at this
            normalizedDistance = Vector3.Angle(eyeForwardVector, objectForwardVector) / maxViewAngle;

            isGazedAt = normalizedDistance < 1;
            if (isGazedAt)
                // count how long we were looking at it
                lookAtTime += Time.unscaledDeltaTime;
            else
                lookAtTime = 0;

            var percentageDone = lookAtTime / requiredLookAtTime;

            if (percentageDone > 1)
                isDone = true;
            
            if (isDone)
                Activate(false);
        }


        public void Activate(bool v)
        {
            isActive = v;
            if (!isActive) allowGazing = false;
            if (!isActive) time = 0;
        }

        /*
        float Map(float val, float srcMin, float srcMax, float dstMin, float dstMax)
        {
            return (val - srcMin) / (srcMax - srcMin) * (dstMax - dstMin) + dstMin;
        }

        float MapClamp(float val, float srcMin, float srcMax, float dstMin, float dstMax)
        {
            return Mathf.Clamp(Map(val, srcMin, srcMax, dstMin, dstMax), Mathf.Min(dstMin, dstMax), Mathf.Max(dstMin, dstMax));
        }
        */
    }

}