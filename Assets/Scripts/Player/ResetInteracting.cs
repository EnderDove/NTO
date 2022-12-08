using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInteracting : StateMachineBehaviour
{
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.SetBool("isInteracting", false);
    }
}
