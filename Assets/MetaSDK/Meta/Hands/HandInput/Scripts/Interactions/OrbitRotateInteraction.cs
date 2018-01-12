using System.Linq;
using Meta.HandInput;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Meta
{
    /// <summary>
    /// Interaction to rotate model in an orbit ball manner.
    /// </summary>
    [AddComponentMenu("Meta/Interaction/OrbitRotateInteraction")]
    [RequireComponent(typeof(Rigidbody))]
    public class OrbitRotateInteraction : MonoBehaviour
    {
        [SerializeField]
        private HandTrigger[] _handTriggers;

        private Transform _gizmoTransform;
        private AnimationCurve _slerpCurve;
        private Quaternion _priorGizmoRotation;
        private Quaternion _deltaRotation;
        private Quaternion _priorRotation;
        private HandFeature _handFeature;
        private float _initialHandCenterDistance;
        private float _inertia;
        private Vector3 _priorHandPosition;

        private void Start()
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


            GameObject gizmoGameObject = new GameObject("gizmo");
            _gizmoTransform = gizmoGameObject.transform;
            _slerpCurve = new AnimationCurve();
            _slerpCurve.AddKey(new Keyframe(.5f, 0f, 0f, 0f));
            _slerpCurve.AddKey(new Keyframe(.8f, 1f, 0f, 0f));
        }

        void Update()
        {
            //add inertia on release
            if (_handFeature != null)
            {
                Quaternion targetRotation = _deltaRotation * transform.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _inertia);
                _inertia -= Time.deltaTime * 2f;
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

        private void Engage()
        {
            _gizmoTransform.position = transform.position;
            _gizmoTransform.LookAt(_handFeature.transform.position);
            _priorGizmoRotation = _gizmoTransform.rotation;
            _initialHandCenterDistance = Vector3.Distance(transform.position, _handFeature.transform.position);
            _priorHandPosition = _handFeature.transform.position;
        }



        public void Disengage()
        {
            _inertia = 1f;
            _handFeature = null;
        }

        public void Manipulate()
        {
            _gizmoTransform.position = transform.position;

            _gizmoTransform.rotation = Quaternion.FromToRotation(_priorHandPosition - transform.position, _handFeature.transform.position - transform.position) * _gizmoTransform.rotation;
            Quaternion deltaGizmoRotation = Quaternion.Inverse(_priorGizmoRotation * Quaternion.Inverse(_gizmoTransform.rotation));
            Quaternion targetRotation = deltaGizmoRotation * transform.rotation;
            float centerDistance = Vector3.Distance(transform.position, _handFeature.transform.position);
            float centerRatio = centerDistance / _initialHandCenterDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _slerpCurve.Evaluate(centerRatio));
            _priorGizmoRotation = _gizmoTransform.rotation;

            _deltaRotation = Quaternion.Inverse(_priorRotation * Quaternion.Inverse(transform.rotation));
            _priorRotation = transform.rotation;
            _priorHandPosition = _handFeature.transform.position;
        }
    }
}