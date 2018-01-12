using System;
using System.Collections;
using UnityEngine;

namespace Meta.Tween
{
    /// <summary>
    /// Class that provides tween animations for TextMesh
    /// </summary>
	public static class TextMeshTweens
	{
        /// <summary>
        /// Coroutine that plays an animation to change the color of a text
        /// </summary>
        /// <param name="textMesh"></param>
        /// <param name="targetColor"></param>
        /// <param name="multiplier"></param>
        /// <param name="curve"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
	    public static IEnumerator ToColor(TextMesh textMesh, Color targetColor, float multiplier, AnimationCurve curve, Action onFinish)
        {
            float realTime = 0;
            float time = 0;
            float easedTime = 0;
            Color initialColor = textMesh.color;

            while (time < 1)
            {
                yield return null;
                time += Time.deltaTime * multiplier;
                realTime += Time.deltaTime;
                if (curve == null)
                {
                    textMesh.color = Color.Lerp(initialColor, targetColor, time);
                }
                else
                {
                    easedTime = curve.Evaluate(time);
                    textMesh.color = Color.Lerp(initialColor, targetColor, easedTime);
                }
            }

            textMesh.color = targetColor;

            if (onFinish != null)
            {
                onFinish.Invoke();
            }
        }

        /// <summary>
        /// Coroutine that plays an fade animation of a text
        /// </summary>
        /// <param name="textMesh"></param>
        /// <param name="targetAlpha"></param>
        /// <param name="multiplier"></param>
        /// <param name="curve"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator Fade(TextMesh textMesh, float targetAlpha, float multiplier, AnimationCurve curve, Action onFinish)
        {
            Color targetColor = textMesh.color;
            targetColor.a = targetAlpha;
            return ToColor(textMesh, targetColor, multiplier, curve, onFinish);
        }
    }
}