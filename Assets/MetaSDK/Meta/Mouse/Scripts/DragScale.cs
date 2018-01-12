using Meta.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Meta.Mouse
{
    /// <summary>
    /// Handles scaling of an object using the mouse scrollwheel.
    /// </summary>
    public class DragScale : MonoBehaviour, IDragHandler
    {
        /// <summary>
        /// Transform to scale
        /// </summary>
        [SerializeField]
        private Transform _scaleTransform;

        [SerializeField]
        private float _scaleMultiplier = .05f;

        [SerializeField]
        private float _maxScale = 5;

        [SerializeField]
        private float _minScale = .2f;

        /// <summary>
        /// Input Button that activates the interaction
        /// </summary>
        [SerializeField]
        private PointerEventData.InputButton _button = PointerEventData.InputButton.Middle;
        /// <summary>
        /// Input Button that activates the interaction when a modifier key is used
        /// </summary>
        [SerializeField]
        private PointerEventData.InputButton _altButton = PointerEventData.InputButton.Left;
        /// <summary>
        /// Keys that activate the interaction
        /// </summary>
        [SerializeField]
        private KeySet _keyCodes;

        private void Awake()
        {
            if (_scaleTransform == null)
            {
                _scaleTransform = transform;
            }

            if (_keyCodes == null)
            {
                Debug.LogWarningFormat("No KeySet specified on {0}! This is fine if you don't want the Alt Button to require a modifier key.", name);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == _button || (eventData.button == _altButton && (_keyCodes == null || _keyCodes.IsPressed())))
            {
                float scaleValue = eventData.delta.y * _scaleMultiplier;
                Vector3 newScale = _scaleTransform.localScale.Add(scaleValue);
                if (newScale.LargestComponent() < _maxScale && newScale.SmallestComponent() > _minScale)
                {
                    _scaleTransform.localScale = newScale;
                }
            }
        }
    }
}