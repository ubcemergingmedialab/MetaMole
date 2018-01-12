using Meta.HandInput;

namespace Meta
{
    /// <summary>
    /// Base class to extend to implement TwoHandInteractions
    /// </summary>
    public abstract class TwoHandInteraction : Interaction
    {
        protected HandFeature FirstGrabbingHand
        {
            get { return GrabbingHands[0].Hand.Palm; }
        }

        protected HandFeature SecondGrabbingHand
        {
            get { return GrabbingHands[1].Hand.Palm; }
        }

        protected override bool CanEngage(Hand hand)
        {
            return GrabbingHands.Count == 2;
        }

        protected override void Engage()
        { }

        protected override bool CanDisengage(Hand hand)
        {
            return GrabbingHands.Count > 1 && GrabbingHands.Contains(hand.Palm);
        }

        protected override void Disengage()
        { }

        protected override void Manipulate()
        { }
    }
}