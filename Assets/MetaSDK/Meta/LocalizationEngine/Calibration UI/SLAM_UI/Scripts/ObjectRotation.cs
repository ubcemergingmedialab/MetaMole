using System;
using UnityEngine;

namespace Meta.SlamUI
{
    /// <summary>
    /// Helper to control the rotation of objects with a 0 to 1 value that respects configured angle restrictions
    /// </summary>
    public class ObjectRotation : MonoBehaviour
    {
        /// <summary>
        /// Rotation axis.
        /// </summary>
        private enum Axis
        {
            X,
            Y,
            Z
        }

        [Tooltip("Axis that is going to have the rotation angle restrictions")]
        [SerializeField]
        private Axis _axis;

        [Tooltip("Minumum angle when rotation is equals to 0")]
        [SerializeField]
        private float _minAngle;

        [Tooltip("Maximum angle when rotation is equals to 1")]
        [SerializeField]
        private float _maxAngle;
        
        [Tooltip("Rotation value between 0 and 1 that performs a rotation between the minimum and maximum angle restrictions")]
        [SerializeField]
        [Range(0, 1)]
        private float _rotation = 0.5f;

        private float _lastRotation = 0.5f;
        private Vector3 _eulerRotation;
        private Vector3 _initialRotation;

        /// <summary>
        /// Rotation value between 0 and 1 that performs a rotation between the minimum and maximum angle restrictions
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = Mathf.Clamp01(value); }
        }

        private void Start()
        {
            _initialRotation = transform.localRotation.eulerAngles;
        }

        private void Update()
        {
            if (_lastRotation != _rotation)
            {
                switch (_axis)
                {
                    case Axis.X:
                        _eulerRotation.x = Mathf.Lerp(_minAngle, _maxAngle, _rotation);
                        _eulerRotation.y = _initialRotation.y;
                        _eulerRotation.z = _initialRotation.z;
                        break;
                    case Axis.Y:
                        _eulerRotation.x = _initialRotation.x;
                        _eulerRotation.y = Mathf.Lerp(_minAngle, _maxAngle, _rotation);
                        _eulerRotation.z = _initialRotation.z;
                        break;
                    case Axis.Z:
                        _eulerRotation.x = _initialRotation.x;
                        _eulerRotation.y = _initialRotation.y;
                        _eulerRotation.z = Mathf.Lerp(_minAngle, _maxAngle, _rotation);
                        break;
                    default:
                        throw new Exception("Not supported axis: " + _axis);
                }

                transform.localRotation = Quaternion.Euler(_eulerRotation);
                _lastRotation = _rotation;
            }
        }
    }
}