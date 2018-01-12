using Meta.HandInput;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Produces outline decorators and provides them to the outline-rendering camera.
    /// </summary>
    internal class InteractionObjectOutlineFactory
    {

        private StereoCameraObjectOutline _stereoCameraObjectOutline;

        internal void SubscribeToHandObjectReferences(IHandObjectReferences referenes)
        {
            referenes.AddListener(OnHandInteractionStateChanged);
        }

        private void OnHandInteractionStateChanged(GameObject target, PalmState fromState, PalmState toState)
        {
            
            if (!_stereoCameraObjectOutline)
            {
                _stereoCameraObjectOutline = GameObject.FindObjectOfType<StereoCameraObjectOutline>();
                if(!_stereoCameraObjectOutline)
                return;
            }
            
            InteractionObjectOutlineSettings settings = target.GetComponent<InteractionObjectOutlineSettings>();

            if (settings)
            {
                OutlineObjectVisualDecorator decorator = target.GetComponent<OutlineObjectVisualDecorator>();
                if (!decorator)
                {
                    decorator = target.AddComponent<OutlineObjectVisualDecorator>();
                    decorator.hideFlags = HideFlags.HideInInspector;

                }

                //Add or remove based on the event
                decorator.ChangeColorBasedOnState(toState);
                if (toState == PalmState.Idle)
                {
                    _stereoCameraObjectOutline.RemoveOutlinedObject(decorator);
                }
                else if ((toState == PalmState.Hovering || toState == PalmState.Grabbing ) && fromState == PalmState.Idle)
                {
                    _stereoCameraObjectOutline.AddOutlinedObject(decorator);
                }

            }
        }
    }
}
