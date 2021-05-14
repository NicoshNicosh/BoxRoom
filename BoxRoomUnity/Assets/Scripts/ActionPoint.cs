using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionPoint : MonoBehaviour
{
    
    private CharacterReal currentCharacter;
    [Range(0,1)] float AngleSensitivity;
    [EnumFlag] public DirectionFlags ValidDirections = DirectionFlags.Everything;

    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnInteract;

    public void CharEnter(CharacterReal characterReal)
    {
        currentCharacter = characterReal;
        OnEnter.Invoke();
    }
    
    public void CharExit(CharacterReal characterReal)
    {
        currentCharacter = null;

        OnExit.Invoke();

    }

    public void CharInteract()
    {   
        OnInteract.Invoke();
    }

    public bool IsCharValid(CharacterReal characterReal)
    {
        return (ValidDirections & characterReal.CurrentDirection) != 0;
    }

}
