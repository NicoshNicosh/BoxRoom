using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class CharacterGame : BaseCharacter
{
    private static readonly int CollectAnim = Animator.StringToHash("Collect");
    private static readonly int IsWalkingAnim = Animator.StringToHash("IsWalking");
    private static readonly int DirectionAnim = Animator.StringToHash("Direction");
    
    [Header("References")]
    private Rigidbody2D Rigidbody;
    
    [Header("Settings")]
    public float WalkSpeed;
    [FormerlySerializedAs("OnEscPressed")] public UnityEvent OnInteract;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        foreach (var smb in CharacterAnimator.GetBehaviours<CharacterSMB>())
        {
            smb.Character = this;
        }
    }

    void Update()
    {
        InputDirection = GetInputDirection();
        if (InputDirection != DirectionFlags.None) CurrentDirection = InputDirection;

        HandleMovement(InputDirection);
        HandleRotation();
        HandleActionPoints();
        HandleAttack();
        HandleGameExit();
    }


    private void HandleGameExit()
    {
        var interactPressed = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
        if(!interactPressed || !ModeActive) return;
        this.OnInteract.Invoke();
    }

    private void HandleRotation()
    {
        CharacterAnimator.SetInteger(DirectionAnim, CurrentDirection.ToWasdNumer());
    }
    

    private void HandleCollection(Collectible collectible)
    {
        CharacterAnimator.SetTrigger(CollectAnim);
    }

    private void HandleMovement(DirectionFlags inputDirection)
    {

        var blockedMove = CurrentSmb && !CurrentSmb.CanMove;
        var canWalk = inputDirection != DirectionFlags.None && !blockedMove;

        CharacterAnimator.SetBool(IsWalkingAnim, canWalk);
        if (!canWalk) return;

        var dir = inputDirection.ToVector();
        Rigidbody.MovePosition(Rigidbody.position + dir * (WalkSpeed * Time.deltaTime));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collectible = other.GetComponent<Collectible>();
        if (other.GetComponent<Collectible>()) HandleCollection(collectible);
        EntityEnter(other);
    }


    private void OnTriggerExit2D(Collider2D other) => EntityExit(other);

    public override bool ModeActive => EnvironmentManager.Instance.Mode == CharacterModes.GameMode;
}