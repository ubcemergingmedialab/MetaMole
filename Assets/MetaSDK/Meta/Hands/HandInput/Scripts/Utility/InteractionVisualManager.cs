using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Meta.HandInput.Utility
{
    public class InteractionVisualManager : MetaBehaviour
    {
        /// <summary>
        /// The Interaction script to listen to.
        /// </summary>
        public Interaction Interaction;

        /// <summary>
        /// Color to apply when object is idle
        /// </summary>
        [SerializeField]
        private Color IdleColor = Color.grey;

        /// <summary>
        /// Color to apply when object is highlighted
        /// </summary>
        [SerializeField]
        private Color HighlightColor = Color.cyan;

        /// <summary>
        /// Color to apply when object is engaged
        /// </summary>
        [SerializeField]
        private Color EngagedColor = Color.green * 2f;

        private Color _targetColor;
        private Color _previousTargetColor;

        private float _interpAmount;
        private float _interpAmountVelocity;

        private List<Renderer> _renderersToUpdate = new List<Renderer>();

        private const float SmoothTime = 0.075f;
        private const float TargetInterpAmount = 1f;

        private void Start()
        {
            if (Interaction == null) { Interaction = GetComponent<Interaction>(); }
            if (Interaction == null)
            {
                Debug.LogWarning("ToggleVisualCube's Interaction Object has not been configured. MetaCubeStateVisualsManager won't execute.");
                return;
            }


            // -- Find and record all materials to affect
            var colliders = Interaction.GetAffectingColliders();
            foreach (var col in colliders)
            {
                var ren = col.GetComponent<Renderer>();
                if (ren)
                {
                    ren.material = Instantiate(ren.material);
                    _renderersToUpdate.Add(ren);
                }
            }


            // -- Initialize variables
            _previousTargetColor = IdleColor;
            _targetColor = IdleColor;

            // -- Subscribe to the Interaction script's events
            Interaction.Events.HoverStart.AddListener(OnHoverStart);
            Interaction.Events.HoverEnd.AddListener(OnHoverEnd);
            Interaction.Events.Engaged.AddListener(OnGrabStart);
            Interaction.Events.Disengaged.AddListener(OnGrabEnd);

        }

        private void Update()
        {
            UpdateMaterials();
        }

        private void OnHoverStart(MetaInteractionData data)
        {
            UpdateStateVisuals(PalmState.Hovering);
        }

        private void OnHoverEnd(MetaInteractionData data)
        {
            UpdateStateVisuals(PalmState.Idle);
        }

        private void OnGrabStart(MetaInteractionData data)
        {
            UpdateStateVisuals(PalmState.Grabbing);
        }

        private void OnGrabEnd(MetaInteractionData data)
        {
            UpdateStateVisuals(PalmState.Hovering);
        }

        private void UpdateMaterials()
        {
            _interpAmount = Mathf.Clamp01(Mathf.SmoothDamp(_interpAmount, TargetInterpAmount, ref _interpAmountVelocity, SmoothTime));

            foreach (var ren in _renderersToUpdate)
            {
                ren.material.color = Color.Lerp(_previousTargetColor, _targetColor, _interpAmount);
            }
        }

        private void UpdateStateVisuals(PalmState newState)
        {
            // -- Keep record of previous target color
            _previousTargetColor = _targetColor;

            // -- Update target color
            switch (newState)
            {
                case PalmState.Idle:
                    _targetColor = IdleColor;
                    break;
                case PalmState.Hovering:
                    _targetColor = HighlightColor;
                    break;
                case PalmState.Grabbing:
                    _targetColor = EngagedColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("newState", newState, null);
            }

            // -- Reset interpolation variables
            _interpAmount = 0f;
            _interpAmountVelocity = 0f;
        }
    }
}