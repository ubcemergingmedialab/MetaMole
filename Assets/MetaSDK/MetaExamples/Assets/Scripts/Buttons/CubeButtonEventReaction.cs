using UnityEngine;

namespace Meta.Buttons
{
    /// <summary>
    /// Script that changes the color of a material when a button is pressed
    /// </summary>
    public class CubeButtonEventReaction : BaseMetaButtonInteractionObject
    {
        [SerializeField]
        [Tooltip("Default Color Value")]
        private Color _defaultColor;
        private Material[] _materials;

        private void Awake()
        {
            var renderer = GetComponent<Renderer>();
            _materials = renderer.materials;
        }

        /// <summary>
        /// Process the Meta Button Event
        /// </summary>
        /// <param name="button">Button Message</param>
        public override void OnMetaButtonEvent(IMetaButton button)
        {
            Color targetColor = Color.white;
            switch (button.State)
            {
                case ButtonState.ButtonRelease:
                    targetColor = _defaultColor;
                    break;
                case ButtonState.ButtonShortPress:
                    targetColor = Color.green;
                    break;
                case ButtonState.ButtonLongPress:
                    targetColor = Color.yellow;
                    break;
            }

            for (int i = 0; i < _materials.Length; ++i)
            {
                var material = _materials[i];
                material.color = targetColor;
            }
        }
    }
}
