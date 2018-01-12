using System.Collections.Generic;
using Meta.HandInput;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// The decorator put onto GameObjects which should be outlined.
    /// </summary>
    internal class OutlineObjectVisualDecorator : GameObjectVisualDecorator
    {

        private const string OutlineShaderPath = "Unlit/UnlitOutlineOnlyTestShader";

        private const string OutlineShaderColorAttributeName = "_GlowColor";

        private Material _outlineMaterial;

        private InteractionObjectOutlineSettings _settings;

        internal override List<GameObject> GetObjectsToDecorate()
        {
            List<GameObject> returnObjects = new List<GameObject>();
            returnObjects.Add(gameObject);
            return returnObjects;
        }

        /// <summary>
        /// The material used to outline the GameObject
        /// </summary>
        internal Material OutlineMaterial
        {
            get
            {
                if (!_outlineMaterial)
                {
                    Shader outlineShader = Shader.Find(OutlineObjectVisualDecorator.OutlineShaderPath);
                    _outlineMaterial = new Material(outlineShader);
                }
                return _outlineMaterial;
            }
        }

        internal void Start()
        {
            _settings = GetComponent<InteractionObjectOutlineSettings>();
            OutlineMaterial.SetColor(OutlineObjectVisualDecorator.OutlineShaderColorAttributeName, _settings.ObjectHoverColor);
        }

        /// <summary>
        /// Changes thw ourlining color based on PalmState.
        /// </summary>
        /// <param name="state"></param>
        internal void ChangeColorBasedOnState(PalmState state)
        {
            if (_settings)
            {
                switch (state)
                {
                    case PalmState.Idle:
                        OutlineMaterial.SetColor(OutlineObjectVisualDecorator.OutlineShaderColorAttributeName, _settings.ObjectIdleColor);
                        break;
                    case PalmState.Hovering:
                        OutlineMaterial.SetColor(OutlineObjectVisualDecorator.OutlineShaderColorAttributeName, _settings.ObjectHoverColor);
                        break;
                    case PalmState.Grabbing:
                        OutlineMaterial.SetColor(OutlineObjectVisualDecorator.OutlineShaderColorAttributeName, _settings.ObjectGrabbedColor);
                        break;
                }

            }
        }
    }
}
