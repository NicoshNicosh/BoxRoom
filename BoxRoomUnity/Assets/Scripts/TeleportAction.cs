using System;
using UnityEngine;

public class TeleportAction : StateMachineBehaviour
{
    public int RootNumber;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var animControl = animator.GetComponent<AnimationControl>();
        if(!animControl) return;
        animControl.RootPoint(RootNumber);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var animControl = animator.GetComponent<AnimationControl>();
        if(!animControl) return;
        animControl.RootPoint(null);
    }
}