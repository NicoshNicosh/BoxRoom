using System;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterReal : BaseCharacter
{
    private static readonly int IsWalkingAnim = Animator.StringToHash("IsWalking");

    public CharacterGame GameCharacter;

    private Rigidbody Rigidbody;

    public float WalkSpeed;
    public float TurnSpeed;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        InputDirection = GetInputDirection();
        if (InputDirection != DirectionFlags.None) CurrentDirection = InputDirection;
        
        HandleMovement(InputDirection);
        HandleRotation();
        HandleEscPressed();
        HandleActionPoints();
        HandleInteraction();
        HandleAttack();
    }

    private void HandleRotation()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            CurrentDirection.ToRotation(), 
            TurnSpeed * Time.deltaTime);
    }

    private void HandleMovement(DirectionFlags inputDirection)
    {
        if(CurrentSMB && !CurrentSMB.CanMove) return;
        if (inputDirection == DirectionFlags.None) return;
        
        Vector3 dir =  inputDirection.ToVector();

        Rigidbody.MovePosition(Rigidbody.position + dir.normalized * (WalkSpeed * Time.deltaTime));
        CharacterAnimator.SetBool(IsWalkingAnim, true);
    
    }

    public override bool ModeActive => EnvironmentManager.Instance.Mode == CharacterModes.RoomMode;

    private void OnTriggerEnter(Collider other) => TriggerEntered(other);
    private void OnTriggerExit(Collider other) => TriggerExited(other);
}
