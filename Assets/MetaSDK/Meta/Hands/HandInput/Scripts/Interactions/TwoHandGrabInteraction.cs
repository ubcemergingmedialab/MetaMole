using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Interaction to translate by grabbing with two hands. Position
    /// is averaged between two hands.
    /// </summary>
    [AddComponentMenu("Meta/Interaction/TwoHandGrabInteraction")]
    public class TwoHandGrabInteraction : TwoHandInteraction
    {
        
        protected override void Engage()
        {
            //rigidbody should be kinematic as to not interfere with grab translation
            SetIsKinematic(true);

            //Set offset from center of object
            SetGrabOffset((FirstGrabbingHand.transform.position + SecondGrabbingHand.transform.position) / 2f);
         
            base.Engage();
        }

        protected override void Disengage()
        {
            SetIsKinematic(false);
          
            base.Disengage();
        }

        protected override void Manipulate()
        {
            Move((FirstGrabbingHand.transform.position + SecondGrabbingHand.transform.position) / 2f);
        }
    }
}