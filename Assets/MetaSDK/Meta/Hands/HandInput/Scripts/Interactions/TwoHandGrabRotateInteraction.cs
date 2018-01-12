using Meta.HandInput;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Manipulation to rotate through average the quaternions of two hands grabbing around a central point.
    /// </summary>
    [AddComponentMenu("Meta/Manipulation/TwoHandGrabRotateInteraction")]
    public class TwoHandGrabRotateInteraction : TwoHandInteraction
    {
        private Vector3 _priorDirection;

        protected override void Engage()
        {
            _priorDirection = SecondGrabbingHand.transform.position - FirstGrabbingHand.transform.position;
            SetIsKinematic(true);
            base.Engage();
        }

        protected override void Disengage()
        {
            SetIsKinematic(false);
            base.Disengage();
        }

        /// <summary>
        /// Continually place gizmo between two hands and average the movement vectors of the two hands
        /// in order to apply to the final object quaternion around the gizmo center point.
        /// </summary>
        protected override void Manipulate()
        {
            Vector3 direction = SecondGrabbingHand.transform.position - FirstGrabbingHand.transform.position;
            Quaternion rotation = Quaternion.FromToRotation(_priorDirection, direction);
            Quaternion newRotation = rotation * TargetTransform.rotation;

            Rotate(newRotation);

            _priorDirection = direction;
        }
    }
}