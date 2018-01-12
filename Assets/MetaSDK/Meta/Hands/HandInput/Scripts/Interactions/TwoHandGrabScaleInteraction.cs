using Meta.Extensions;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Interaction to scale model by placing two hands into the model and grabbing.
    /// </summary>
    [AddComponentMenu("Meta/Interaction/TwoHandGrabScaleInteraction")]
    public class TwoHandGrabScaleInteraction : TwoHandInteraction
    {
        /// <summary>
        /// Minimum scale
        /// </summary>
        [SerializeField]
        private Vector2 _minSize = new Vector2(.3f, .3f);

        /// <summary>
        /// Maximum scale
        /// </summary>
        [SerializeField]
        private Vector2 _maxSize = new Vector2(2, 2);

        private float _priorDistance;

        /// <summary>
        /// Minimum scale
        /// </summary>
        public Vector2 MinSize
        {
            get { return _minSize; }
            set { _minSize = value; }
        }

        /// <summary>
        /// Maximum scale
        /// </summary>
        public Vector2 MaxSize
        {
            get { return _maxSize; }
            set { _maxSize = value; }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Engage()
        {
            _priorDistance = Vector3.Distance(FirstGrabbingHand.transform.position,
                                                    SecondGrabbingHand.transform.position);
            SetIsKinematic(true);
            base.Engage();
        }

        protected override void Disengage()
        {
            SetIsKinematic(false);
            base.Disengage();
        }

        protected override void Manipulate()
        {
            Vector3 center = (FirstGrabbingHand.transform.position + SecondGrabbingHand.transform.position) / 2f;
            Vector3 offset = TargetTransform.position - center;

            float currentDistance = Vector3.Distance(FirstGrabbingHand.transform.position,
                                                        SecondGrabbingHand.transform.position);
            float multiplier = currentDistance / _priorDistance;
            multiplier = Mathf.Clamp(multiplier, .5f, 1.5f);

            RectTransform rectTransform = TargetTransform as RectTransform;
            if (rectTransform != null)
            {
                Vector2 newSize = rectTransform.sizeDelta * multiplier;
                if (newSize.IsNaN())
                {
                    return;
                }
                if (newSize.x < _maxSize.x && newSize.y < _maxSize.y && newSize.x > _minSize.x && newSize.y > _minSize.y)
                {
                    rectTransform.sizeDelta = newSize;
                    Move(center + (offset * multiplier));
                }
            }
            else
            {
                Vector3 newScale = TargetTransform.localScale * multiplier;
                if (newScale.IsNaN())
                {
                    return;
                }
                if (newScale.x < _maxSize.x && newScale.y < _maxSize.y && newScale.x > _minSize.x && newScale.y > _minSize.y)
                {
                    TargetTransform.localScale = newScale;
                    Move(center + (offset * multiplier));
                }
                TargetTransform.localScale = newScale;
            }

            _priorDistance = currentDistance;
        }
    }
}