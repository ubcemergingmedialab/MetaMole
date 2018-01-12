using System;
using System.Collections;
using UnityEngine;

namespace Meta.Tween
{
    /// <summary>
    /// Class that provides tween animations for Transform
    /// </summary>
	public static class TransformTweens
	{
        /// <summary>
        /// Coroutine that plays an animation to change the transform position
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="targetPosition"></param>
        /// <param name="multiplier"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
	    public static IEnumerator ToPosition(Transform transform, Vector3 targetPosition, float multiplier, Action onFinish)
	    {
	        float time = 0;
	        Vector3 initialPosition = transform.position;

	        while (time < 1)
	        {
	            yield return null;
	            time += Time.deltaTime * multiplier;
	            transform.position = Vector3.Lerp(initialPosition, targetPosition, time);
	        }

	        transform.position = targetPosition;

	        if (onFinish != null)
	        {
	            onFinish.Invoke();
	        }
	    }

        /// <summary>
        /// Coroutine that plays an animation to change the transform position and rotation, in relation to another transform
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="targetTransform"></param>
        /// <param name="multiplier"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator ToTransform(Transform transform, Transform targetTransform, float multiplier, Action onFinish)
        {
            float time = 0;
            Vector3 initialPosition = transform.position;
            Quaternion initialRotation = transform.rotation;

            while (time < 1)
            {
                yield return null;
                time += Time.deltaTime * multiplier;
                transform.position = Vector3.Lerp(initialPosition, targetTransform.position, time);
                transform.rotation = Quaternion.Slerp(initialRotation, targetTransform.rotation, time);
            }

            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;

            if (onFinish != null)
            {
                onFinish.Invoke();
            }
        }

        /// <summary>
        /// Coroutine that plays an animation to change the transform scale
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="targetScale"></param>
        /// <param name="multiplier"></param>
        /// <param name="curve"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
	    public static IEnumerator ToScale(Transform transform, Vector3 targetScale, float multiplier, AnimationCurve curve, Action onFinish)
        {
            float time = 0;
            float easedTime = 0;
            Vector3 initialScale = transform.localScale;

            while (time < 1)
            {
                yield return null;
                time += Time.deltaTime * multiplier;
                if (curve == null)
                {
                    transform.localScale = Vector3.Lerp(initialScale, targetScale, time);
                }
                else
                {
                    easedTime = curve.Evaluate(time);
                    transform.localScale = Vector3.Lerp(initialScale, targetScale, easedTime);
                }
            }

            transform.localScale = targetScale;

            if (onFinish != null)
            {
                onFinish.Invoke();
            }
        }
    }
}