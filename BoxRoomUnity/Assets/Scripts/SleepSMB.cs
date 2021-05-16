using UnityEngine;

public class SleepSMB : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var animControl = animator.GetComponent<AnimationControl>();
        if (animControl) {EnvironmentManager.Instance.EndScene();}
       
    }


}