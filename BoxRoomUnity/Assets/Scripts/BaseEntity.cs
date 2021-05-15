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

    public AudioSource audioSource;
    

    public void CharEnter(BaseCharacter characterReal)
    {
        LastCharacter = characterReal;
        OnEnter.Invoke();
    }
    
    public void CharExit(BaseCharacter characterReal)
    {
        LastCharacter = characterReal;
        OnExit.Invoke();
    }
    
    public void CharAttack(BaseCharacter characterReal)
    {
        LastCharacter = characterReal;
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

    public void PlaySound()
	{
        if (audioSource != null)
		{
            audioSource.Play();
		}
	}
}