using Meta.HandInput;
using UnityEngine;


namespace Meta
{
    /// <summary>
    /// The delegate for hands state-transitions for objects. 
    /// </summary>
    /// <param name="target">The GameObject which had the state transition</param>
    /// <param name="fromState">The state before the transition</param>
    /// <param name="toState">The state after the transition</param>
    internal delegate void HandObjectTransition(GameObject target, PalmState fromState , PalmState toState);
   
    /// <summary>
    /// Facade for Meta Hands complex subsystem. 
    /// An implementor defines the criteria for when objects incur major or minor changes
    /// </summary>
    internal interface IHandObjectReferences
    {

        void AddListener(HandObjectTransition action);

        void RemoveListener(HandObjectTransition action);

        void AcceptStateTransitionForObject(GameObject target, PalmState fromState, PalmState toState);
    }

}