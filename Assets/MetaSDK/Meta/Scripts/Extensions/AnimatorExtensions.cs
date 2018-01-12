using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta
{
    public static class AnimatorExtensions
    {
        public static void CrossFadeToStateIfNotCurrent(this Animator animator, string stateName, float time)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && !animator.IsInTransition(0))
            {
                animator.CrossFade(stateName, time);
            }
        }
    }
}