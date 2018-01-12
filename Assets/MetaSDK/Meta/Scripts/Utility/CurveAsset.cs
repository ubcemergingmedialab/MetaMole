using UnityEngine;
using System.Collections;

namespace Meta
{
    /// <summary>
    /// Allows you to create a curve as a .asset file
    /// </summary>
    [CreateAssetMenu(fileName = "Curve", menuName = "Curve")]
    public class CurveAsset : ScriptableObject
    {
        /// <summary>
        /// Animation curve
        /// </summary>
        [SerializeField]
        private AnimationCurve _animationCurve = new AnimationCurve();

        /// <summary>
        /// Animation curve
        /// </summary>
        public AnimationCurve Curve
        {
            get { return _animationCurve; }
        }

        /// <summary>
        /// Evaluate the animation curve at the specified time
        /// </summary>
        /// <param name="time"></param>
        /// <returns>Value of the animation curve at the specified time</returns>
        public float Evaluate(float time)
        {
            return _animationCurve.Evaluate(time);
        }
    }
}