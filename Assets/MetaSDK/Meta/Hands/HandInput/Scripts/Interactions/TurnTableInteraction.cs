using Meta.HandInput;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Meta
{
    /// <summary>
    /// Interaction to rotate model only on Y axis.
    /// </summary>
    [AddComponentMenu("Meta/Interaction/TurnTableInteraction")]
    [RequireComponent(typeof(Rigidbody))]
    public class TurnTableInteraction : MonoBehaviour
    {
        [SerializeField]
        private HandTrigger[] _handTriggers;

        /// <summary>
        /// How much to damp rotation
        /// </summary>
        [SerializeField]
        private float _damp = .1f;

        private HandFeature _handFeature;
        private float _deltaAngle;
        private float _priorHandFeatureAngle;
        private float _velocity;

        /// <summary>
        /// How much to damp rotation
        /// </summary>
        public float Damp
        {
            get { return _damp; }
            set { _damp = value; }
        }

        void Start()
        {
            if (_handTriggers == null || _handTriggers.Length == 0 || _handTriggers.Contains(null))
            {
                Debug.LogError("HandTriggers have not been configured. Please link one or more HandVolumes.");
            }
            else
            {
                foreach (var handTrigger in _handTriggers)
                {
                    handTrigger.HandFeatureEnterEvent.AddListener(OnHandFeatureEnter);
                    handTrigger.HandFeatureExitEvent.AddListener(OnHandFeatureExit);
                }
            }

        }

        void Update()
        {
            transform.Rotate(0f, _deltaAngle, 0f);
            if (_handFeature == null)
            {
                _deltaAngle = Mathf.SmoothStep(_deltaAngle, 0f, .1f);

                Manipulate();
            }
        }

        public void OnHandFeatureEnter(HandFeature handFeature)
        {
            if (handFeature is TopHandFeature && _handFeature == null)
            {
                _handFeature = handFeature;
                Engage();
            }
        }


        public void OnHandFeatureExit<T>(T handFeature) where T : HandFeature
        {
            if (handFeature is TopHandFeature && _handFeature == handFeature)
            {
                _handFeature = null;
                Disengage();
            }
        }


        public void Engage()
        {
            _priorHandFeatureAngle = HandFeatureAngle();
        }

        public void Disengage()
        { }

        public void Manipulate()
        {
            //only update if non-buffered is isValid from Data is valid so that it does not take into account
            //times when the hand is sitting still right after going off screen from the buffered GrabbingFeature.IsValid
            //returning true while the hand is not actually updating.
            float currentHandFeatureAngle = Mathf.SmoothDampAngle(_priorHandFeatureAngle, HandFeatureAngle(),
                ref _velocity, _damp);
            _deltaAngle = Mathf.DeltaAngle(_priorHandFeatureAngle, currentHandFeatureAngle);
            _priorHandFeatureAngle = currentHandFeatureAngle;
        }

        private void OnDisable()
        {
            _deltaAngle = 0;
        }

        private float HandFeatureAngle()
        {
            return Mathf.Atan2(transform.position.x - _handFeature.transform.position.x,
                transform.position.z - _handFeature.transform.position.z) * Mathf.Rad2Deg;
        }
    }
}
