using UnityEngine;
using UnityEngine.EventSystems;

namespace Meta.Mouse
{
    /// <summary>
    /// Rotates an object using mouse click and drag.
    /// </summary>
    public class DragRotate : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        /// <summary>
        /// Transform to rotate
        /// </summary>
        [SerializeField]
        private Transform _rotateTransform;
        /// <summary>
        /// Button that activates rotation
        /// </summary>
        [SerializeField]
        private PointerEventData.InputButton _button = PointerEventData.InputButton.Right;

        private void Awake()
        {
            if (_rotateTransform == null)
            {
                _rotateTransform = transform;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == _button)
            {
                eventData.useDragThreshold = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == _button)
            {
                _rotateTransform.Rotate(0, -eventData.delta.x, 0, Space.World);

                Ray ray = eventData.pressEventCamera.ScreenPointToRay(eventData.position);
                Vector3 direction = _rotateTransform.position - ray.origin;
                Vector3 cross = Vector3.Cross(direction, Vector3.up);
                _rotateTransform.RotateAround(_rotateTransform.position, cross, -eventData.delta.y);
            }
        }
    }
}