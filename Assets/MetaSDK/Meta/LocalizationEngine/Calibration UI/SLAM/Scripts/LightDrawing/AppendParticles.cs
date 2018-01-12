using UnityEngine;
using System.Collections;

namespace Meta
{
    /// <summary>
    /// Used for Lightpainting. Tells a particle system's ResetParticles script to start a new light line.
    /// </summary>
    public class AppendParticles : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<ResetParticles>().StartNewLine();
        }
    }

}
