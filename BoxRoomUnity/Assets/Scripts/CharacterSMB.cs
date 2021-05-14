using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSMB : StateMachineBehaviour
{
    internal BaseCharacter Character;

    public bool CanAttack = false;
    public bool CanMove = false;

    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Character.CurrentSMB = this;
        Debug.Log("Cheem");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Character.CurrentSMB == this) Character.CurrentSMB = null;
    }
}
