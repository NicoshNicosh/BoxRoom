using UnityEngine;

public class CharacterSMB : StateMachineBehaviour
{
    public bool CanAttack = false;
    public bool CanMove = false;
    public bool CanInteract = true;
    public bool IsLongState = false;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var animControl = animator.GetComponent<AnimationControl>();
        if(!animControl) return;
        animControl.Character.SMBs.Add(this);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var animControl = animator.GetComponent<AnimationControl>();
        if(!animControl) return;
        animControl.Character.SMBs.Remove(this);
    }
}
