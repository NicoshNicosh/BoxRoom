using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEntity : MonoBehaviour
{
    
    [ReadOnly] public BaseCharacter LastCharacter;

    [EnumFlag] public DirectionFlags ValidDirections = DirectionFlags.Everything;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnAttack;
    

    public void CharEnter(BaseCharacter characterReal)
    {
        OnEnter.Invoke();
    }
    
    public void CharExit(BaseCharacter characterReal)
    {
        OnExit.Invoke();
    }
    
    public void CharAttack(BaseCharacter characterReal)
    {
        OnAttack.Invoke();
    }

    public void TriggerOnCharacter(string triggerName)
    {
        LastCharacter.CharacterAnimator.SetTrigger(triggerName);
    }
    
    public bool IsCharValid(BaseCharacter characterReal)
    {
        return (ValidDirections & characterReal.CurrentDirection) != 0;
    }
}