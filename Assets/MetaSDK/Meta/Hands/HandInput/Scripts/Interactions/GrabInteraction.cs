using Meta.HandInput;
using UnityEngine;

namespace Meta 
{
    /// <summary>
    /// Interaction to grab the model to translate its position.
    /// </summary>
    [AddComponentMenu("Meta/Interaction/GrabInteraction")]
    public class GrabInteraction : Interaction
    {
        private HandFeature _handFeature;

        protected override bool CanEngage(Hand handProxy)
        {
            return GrabbingHands.Count == 1;
        }

        protected override void Engage()
        {
            _handFeature = GrabbingHands[0];

            //rigidbody should be kinematic as to not interfere with grab translation
            SetIsKinematic(true);

            SetGrabOffset(_handFeature.Position);
        }

        protected override bool CanDisengage(Hand handProxy)
        {
            if (_handFeature != null && handProxy.Palm == _handFeature)
            {
                foreach (var hand in GrabbingHands)
                {
                    if (hand != _handFeature)
                    {
                        _handFeature = hand;
                        SetGrabOffset(_handFeature.Position);
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        protected override void Disengage()
        {
            SetIsKinematic(false);
            _handFeature = null;
        }

        protected override void Manipulate()
        {
            Move(_handFeature.Position);
        }
    }
}
