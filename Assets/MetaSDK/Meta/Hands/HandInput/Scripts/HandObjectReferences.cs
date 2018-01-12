using UnityEngine;
using Meta.HandInput;

namespace Meta
{ 
    /// <summary>
    /// Notifies subscribers of events to GameObjects from Meta Hands.
    /// </summary>
    internal class HandObjectReferences : IHandObjectReferences
    {

        private event HandObjectTransition _onStateTransition;

        /// <summary>
        /// Lets subscribers know of interaction-related events happening to GameObjects.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="fromState"></param>
        /// <param name="toState"></param>
        public void AcceptStateTransitionForObject(GameObject target, PalmState fromState, PalmState toState)
        {
            _onStateTransition.Invoke(target, fromState, toState);
        }

        public void AddListener(HandObjectTransition action)
        {
            _onStateTransition += action;
        }

        public void RemoveListener(HandObjectTransition action)
        {
            _onStateTransition -= action;
        }
    }
}
