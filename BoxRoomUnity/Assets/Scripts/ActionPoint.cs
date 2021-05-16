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

    public int interactionSoundLimit = 0;
    public int interactCounter = 0;

    public void CharInteract()
    {   
        OnInteract.Invoke();
        PlayInteractionSound();

    }
    public void PlayInteractionSound()
    {
        if( audioSource == null || interactionSoundLimit == 0) return;
        var clipNum = interactCounter % interactionSoundLimit;
        interactCounter++;
        PlaySound(clipNum);
    }
    

    public void EnterPlay() => (LastCharacter as CharacterReal)?.EnterState(CharacterStates.Play);
    public void EnterBed()=> (LastCharacter as CharacterReal)?.EnterState(CharacterStates.Bed);
    public void EnterPoop()=> (LastCharacter as CharacterReal)?.EnterState(CharacterStates.Poop);
    public void EndScene() => EnvironmentManager.Instance.EndScene();


}
