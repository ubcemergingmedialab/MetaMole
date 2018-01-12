using Meta.HandInput;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Interaction to scale model by placing two hands into the model and grabbing.
    /// </summary>
    [AddComponentMenu("Meta/Interaction/TwoHandScaleInteraction")]
    public class TwoHandScaleInteraction : Interaction //TODO make use TwoHandInteraction
    {
        private const float LimitResizeDamp = .1f;


        /// <summary>
        /// Minimum scale
        /// </summary>
        [SerializeField]
        private float _minSize = .2f;

        /// <summary>
        /// Maximum scale
        /// </summary>
        [SerializeField]
        private float _maxSize = 2f;
        
        private float _priorDistance;
        private Vector3 _limitResizeVelocity;

        /// <summary>
        /// First hand to grab the interaction object
        /// </summary>
        protected HandFeature FirstGrabbingHand
        {
            get { return GrabbingHands[0].Hand.Palm; }
        }

        /// <summary>
        /// Second hand to grab the interaction object
        /// </summary>
        protected HandFeature SecondGrabbingHand
        {
            get { return GrabbingHands[1].Hand.Palm; }
        }

        /// <summary>
        /// Maximum scale
        /// </summary>
        public float MaxSize
        {
            get { return _maxSize; }
        }

        /// <summary>
        /// Minimum scale
        /// </summary>
        public float MinSize
        {
            get { return _minSize; }
        }

        protected override void Update()
        {
            //resize model if past limits
            if (TargetTransform.localScale.x > _maxSize)
            {
                TargetTransform.localScale = new Vector3(_maxSize, _maxSize, _maxSize);
            }
            if (State != InteractionState.On)
            {
                if (TargetTransform.localScale.x < _minSize)
                {
                    TargetTransform.localScale = Vector3.SmoothDamp(TargetTransform.localScale,
                        new Vector3(_minSize, _minSize, _minSize), ref _limitResizeVelocity, LimitResizeDamp);
                }
            }
        }

        protected override bool CanEngage(Hand hand)
        {
            //if two hands are grabbing the object
            return GrabbingHands.Count == 2;
        }

        protected override void Engage()
        {
            _priorDistance = Vector3.Distance(FirstGrabbingHand.Position,
                                                    SecondGrabbingHand.Position);
        }

        protected override bool CanDisengage(Hand hand)
        {
            return GrabbingHands.Count > 1 && GrabbingHands.Contains(hand.Palm);
        }

        protected override void Disengage()
        { }

        protected override void Manipulate()
        {
            float currentDistance = Vector3.Distance(FirstGrabbingHand.Position,
                                                        SecondGrabbingHand.Position);
            float multiplier = currentDistance / _priorDistance;
            if (multiplier < 1.5f && multiplier > .5f)
            {
                TargetTransform.localScale *= multiplier;
            }
            _priorDistance = currentDistance;
        }
    }
}