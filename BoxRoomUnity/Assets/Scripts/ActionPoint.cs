using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ActionPoint : BaseEntity
{
    
    [Header("3D Only")]
    public UnityEvent OnInteract;
    public List<Transform> RootPoints = new List<Transform>();
    
    public void CharInteract()
    {   
        OnInteract.Invoke();
        PlaySound();
    }

    public void EnterPlay() => (LastCharacter as CharacterReal)?.EnterState(CharacterStates.Play);
    public void EnterBed()=> (LastCharacter as CharacterReal)?.EnterState(CharacterStates.Bed);
    public void EnterPoop()=> (LastCharacter as CharacterReal)?.EnterState(CharacterStates.Poop);



}
