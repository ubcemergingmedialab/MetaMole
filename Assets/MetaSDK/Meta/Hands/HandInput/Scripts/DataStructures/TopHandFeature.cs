using UnityEngine;

namespace Meta.HandInput
{
    /// <summary>
    /// Class containing information about the hand's top feature.
    /// </summary>
    public class TopHandFeature : HandFeature
    {

        /// <summary>
        /// This feature's position
        /// </summary>
        public override Vector3 Position
        {
            get { return HandData.Top; }
        }
    }
}