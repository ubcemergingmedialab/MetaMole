using UnityEngine;
using System.Collections;

namespace Meta
{
    /// <summary>
    /// Used for Lightpainting. Completely clears and resets a particle system using the ResetParticles component.
    /// </summary>
    public class ResetParticlesAnim : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var p = animator.GetComponent<ResetParticles>();
            if (p)
            {
                p.Reset();
            }
        }
    }
}