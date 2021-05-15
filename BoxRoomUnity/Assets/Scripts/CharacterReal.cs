using System;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterReal : BaseCharacter
{
    private static readonly int IsWalkingAnim = Animator.StringToHash("IsWalking");
    
    [Header("References")]
    private Rigidbody _rigidbody;
    
    [Header("Settings")]
    public float WalkSpeed;
    public float TurnSpeed;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        InputDirection = GetInputDirection();
        if (InputDirection != DirectionFlags.None) CurrentDirection = InputDirection;
        
        HandleMovement(InputDirection);
        HandleRotation();
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


    private void HandleInteraction()
    {
        var interactPressed = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
        if(!interactPressed) return;
        ;
        if (ModeActive && CurrentEntity is ActionPoint point)
        {
            point.CharInteract();
        }
    }

    private void HandleMovement(DirectionFlags inputDirection)
    {
        var blockedMove = CurrentSmb && !CurrentSmb.CanMove;
        var canWalk = inputDirection != DirectionFlags.None && !blockedMove;
        
        CharacterAnimator.SetBool(IsWalkingAnim, canWalk);
        if (!canWalk) return;

        Vector3 dir =  inputDirection.ToVector();

        _rigidbody.MovePosition(_rigidbody.position + dir.normalized * (WalkSpeed * Time.deltaTime));
    
    }

    public override bool ModeActive => EnvironmentManager.Instance.Mode == CharacterModes.RoomMode;

    private void OnTriggerEnter(Collider other) => EntityEnter(other);
    private void OnTriggerExit(Collider other) => EntityExit(other);
}
