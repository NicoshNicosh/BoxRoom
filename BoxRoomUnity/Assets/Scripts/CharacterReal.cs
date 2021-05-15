﻿using System;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterReal : BaseCharacter
{
    [Header("References")]
    [SerializeField] private Rigidbody Rigidbody;

    [Header("Settings")]
    public float WalkSpeed;
    public float TurnSpeed;

    [Header("State")]
    [ReadOnly] public CharacterStates State = CharacterStates.Stand;

    [ReadOnly] public float LastTimeStanding;
    [ReadOnly] public float TimeInState;
    private static readonly int InBedAnim = Animator.StringToHash("InBed");
    private static readonly int IsPlayAnim = Animator.StringToHash("IsPlay");
    private static readonly int IsPoopingAnim = Animator.StringToHash("IsPooping");
    private static readonly int TimeInStateAnim = Animator.StringToHash("TimeInState");

    protected override void Update()
    {
        base.Update();

        if (State == CharacterStates.Play && EnvironmentManager.Instance.Mode != CharacterModes.GameMode)
            State = CharacterStates.Stand;

        CharacterAnimator.SetBool(InBedAnim, State == CharacterStates.Bed);
        CharacterAnimator.SetBool(IsPlayAnim, State == CharacterStates.Play);
        CharacterAnimator.SetBool(IsPoopingAnim, State == CharacterStates.Poop);

        if (State == CharacterStates.Stand) LastTimeStanding = Time.time;
        TimeInState = Time.time - LastTimeStanding;
        CharacterAnimator.SetFloat(TimeInStateAnim, TimeInState);
    }

    public void EnterState(CharacterStates state)
    {
        State = state;
        if (state == CharacterStates.Play) EnvironmentManager.Instance.EnterGame();
    }

    protected override void HandleInteraction()
    {
        var interactPressed = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
        if (!interactPressed) return;
        if (CurrentSmb && CurrentSmb.IsLongState)
        {
            State = CharacterStates.Stand;
            return;
        }
        
        if (CurrentEntity is ActionPoint point)
        {
            point.CharInteract();
        }
    }
    
    protected override void HandleRotation() =>
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            CurrentDirection.ToRotation(),
            TurnSpeed * Time.deltaTime);
    
    protected override void HandleMovement(DirectionFlags inputDirection)
    {
        if (CurrentSmb && CurrentSmb.IsLongState)
        {
            State = CharacterStates.Stand;
            return;
        }

        Rigidbody.MovePosition(Rigidbody.position + inputDirection.ToVector3() * (WalkSpeed * Time.deltaTime));
    }

    public override bool ModeActive => EnvironmentManager.Instance.Mode == CharacterModes.RoomMode;

    private void OnTriggerEnter(Collider other) => EntityEnter(other);
    private void OnTriggerExit(Collider other) => EntityExit(other);
    private void OnCollisionEnter(Collision other) => EntityEnter(other.collider);
    private void OnCollisionExit(Collision other) => EntityExit(other.collider);

}


public enum CharacterStates
{
    Stand,
    Poop,
    Bed,
    Play,
}
