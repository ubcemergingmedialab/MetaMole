using UnityEngine;

namespace Meta
{
    /// <summary>
    /// If hands grab object side by side, object will rotate around Y. If hands grab object
    /// above and below each object, object will rotate around specific _rotateAxis;
    /// </summary>
    public class TwoHandGrabSwitchRotationInteraction : TwoHandInteraction
    {
        [Tooltip("Axis to rotate around when hands are placed above and below each other. Screen will rotate tangential to main camera.")]
        [SerializeField]
        private RotateAxis _rotateAxis;

        private const float SwitchTolerance = 45;

        private Transform _gizmoTransform;
        private float _priorAngle;
        private Axis _axis;
        private IEventCamera _eventCamera;

        private enum RotateAxis
        {
            Screen,
            LocalX,
            LocalZ,
            None,
        }

        private void Start()
        {
            GameObject gizmoGameObject = new GameObject("gizmo");
            gizmoGameObject.hideFlags = HideFlags.HideInHierarchy;
            _gizmoTransform = gizmoGameObject.transform;
            _eventCamera = metaContext.Get<IEventCamera>();
        }

        private void OnDestroy()
        {
            if (_gizmoTransform != null)
            {
                Destroy(_gizmoTransform.gameObject);
            }
        }

        protected override void Engage()
        {
            if (Mathf.Abs(ZAngle(FirstGrabbingHand.transform, SecondGrabbingHand.transform)) < SwitchTolerance)
            {
                _axis = Axis.Y;
                _priorAngle = YAngle();
            }
            else
            {
                _axis = Axis.X;
                _priorAngle = XAngle();
            }
            base.Engage();
        }

        protected override void Manipulate()
        {
            if (_axis == Axis.Y)
            {
                float angle = YAngle();
                float deltaAngle = angle - _priorAngle;
                TargetTransform.Rotate(new Vector3(0, deltaAngle, 0), Space.World);
                _priorAngle = angle;
            }
            else if (_axis == Axis.X)
            {
                float angle = XAngle();
                float deltaAngle = angle - _priorAngle;
                if (_rotateAxis == RotateAxis.LocalX)
                {
                    TargetTransform.Rotate(new Vector3(deltaAngle, 0, 0), Space.Self);
                }
                else if (_rotateAxis == RotateAxis.LocalZ)
                {
                    TargetTransform.Rotate(new Vector3(0, 0, deltaAngle), Space.Self);
                }
                else if (_rotateAxis == RotateAxis.Screen)
                {
                    Vector3 cameraDirection =  _eventCamera.EventCameraRef.transform.position - TargetTransform.position;
                    Vector3 crossDirection = Vector3.Cross(cameraDirection, Vector3.up);
                    TargetTransform.RotateAround(TargetTransform.position, crossDirection, deltaAngle);
                }
                _priorAngle = angle;
            }
        }

        private float ZAngle(Transform handA, Transform handB)
        {
            _gizmoTransform.position = _eventCamera.EventCameraRef.transform.position;
            _gizmoTransform.LookAt((handA.position + handB.position) / 2f);
            Vector3 leftLocalPosition = _gizmoTransform.InverseTransformPoint(handA.position);
            Vector3 rightLocalPosition = _gizmoTransform.InverseTransformPoint(handB.position);

            float xDelta;
            float yDelta;

            if (leftLocalPosition.x > rightLocalPosition.x)
            {
                xDelta = leftLocalPosition.x - rightLocalPosition.x;
                yDelta = leftLocalPosition.y - rightLocalPosition.y;
            }
            else
            {
                xDelta = rightLocalPosition.x - leftLocalPosition.x;
                yDelta = rightLocalPosition.y - leftLocalPosition.y;
            }

            return Mathf.Atan2(yDelta, xDelta) * Mathf.Rad2Deg;
        }

        private float XAngle()
        {
            _gizmoTransform.position = _eventCamera.EventCameraRef.transform.position;
            _gizmoTransform.LookAt((FirstGrabbingHand.transform.position + SecondGrabbingHand.transform.position) / 2f);
            Vector3 leftLocalPosition = _gizmoTransform.InverseTransformPoint(FirstGrabbingHand.transform.position);
            Vector3 rightLocalPosition = _gizmoTransform.InverseTransformPoint(SecondGrabbingHand.transform.position);

            float zDelta = leftLocalPosition.z - rightLocalPosition.z;
            float yDelta = leftLocalPosition.y - rightLocalPosition.y;

            return Mathf.Atan2(zDelta, yDelta) * Mathf.Rad2Deg;
        }

        private float YAngle()
        {
            float zDelta = FirstGrabbingHand.transform.position.z - SecondGrabbingHand.transform.position.z;
            float xDelta = FirstGrabbingHand.transform.position.x - SecondGrabbingHand.transform.position.x;
            return Mathf.Atan2(xDelta, zDelta) * Mathf.Rad2Deg;
        }
    }
}