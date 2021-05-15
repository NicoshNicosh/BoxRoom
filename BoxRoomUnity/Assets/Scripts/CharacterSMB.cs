using UnityEngine;

public class CharacterSMB : StateMachineBehaviour
{
    internal BaseCharacter Character;

    public bool CanAttack = false;
    public bool CanMove = false;
    public bool CanInteract = true;
    public bool IsLongState = false;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Character.SMBs.Add(this);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Character.SMBs.Remove(this);
    }
}
