using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class ResetAnimationaFlag : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("shouldPlayAnimation", false);
        }

    }
}
