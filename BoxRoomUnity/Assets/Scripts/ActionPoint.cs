using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionPoint : MonoBehaviour
{
    
    private BaseCharacter currentCharacter;
    [Range(0,1)] float AngleSensitivity;
    [EnumFlag] public DirectionFlags ValidDirections = DirectionFlags.Everything;
    public List<Transform> RootPoints = new List<Transform>();

    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnInteract;

    public void CharEnter(BaseCharacter characterReal)
    {
        currentCharacter = characterReal;
        OnEnter.Invoke();
    }
    
    public void CharExit(BaseCharacter characterReal)
    {
        currentCharacter = null;
        OnExit.Invoke();

    }

    public void CharInteract()
    {   
        OnInteract.Invoke();
    }

    public bool IsCharValid(BaseCharacter characterReal)
    {
        return (ValidDirections & characterReal.CurrentDirection) != 0;
    }

}
