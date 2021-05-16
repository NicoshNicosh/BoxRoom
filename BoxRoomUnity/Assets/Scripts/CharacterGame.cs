using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class CharacterGame : BaseCharacter
{
    private static readonly int DirectionAnim = Animator.StringToHash("Direction");
    
    [Header("References")]
    [SerializeField]
    public Rigidbody2D Rigidbody;
    
    [Header("Settings")]
    public float WalkSpeed;
    [FormerlySerializedAs("OnEscPressed")] public UnityEvent OnInteract;
    protected override void HandleInteraction()
    {
        var interactPressed = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
        if(!interactPressed || !ModeActive) return;
        this.OnInteract.Invoke();
    }

    protected override void HandleRotation() => 
        CharacterAnimator.SetInteger(DirectionAnim, CurrentDirection.ToWasdNumer());

    protected override void HandleMovement(DirectionFlags inputDirection) => 
        Rigidbody.MovePosition(Rigidbody.position + inputDirection.ToVector2() * (WalkSpeed * Time.deltaTime));

    public override bool ModeActive => EnvironmentManager.Instance.Mode == CharacterModes.GameMode;

	private void OnTriggerEnter2D(Collider2D other)=> EntityEnter(other);
    private void OnTriggerExit2D(Collider2D other) => EntityExit(other);
    private void OnCollisionEnter2D(Collision2D other) => EntityEnter(other.collider);
    private void OnCollisionExit2D(Collision2D other)=>EntityExit(other.collider);
}