using UnityEngine;

public class CharacterGame : BaseCharacter
{
    public CharacterReal RealCharacter;
    private static readonly int AttackAnim = Animator.StringToHash("Attack");
    private static readonly int CollectAnim = Animator.StringToHash("Collect");
    private static readonly int IsWalkingAnim = Animator.StringToHash("IsWalking");

    private Rigidbody2D Rigidbody;
    public float WalkSpeed;
    private static readonly int DirectionAnim = Animator.StringToHash("Direction");

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
        HandleEscape();
    }

    private void HandleRotation()
    {
        CharacterAnimator.SetInteger(DirectionAnim, CurrentDirection.ToWasdNumer());
    }

    private void HandleAttack()
    {

        if (ModeActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentAp) CurrentAp.CharInteract();
            else CharacterAnimator.SetTrigger(AttackAnim);
        }
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ModeActive = false;
            RealCharacter.ModeActive = true;
        }
    }


    private void HandleCollection(Collectible collectible)
    {
        CharacterAnimator.SetTrigger(CollectAnim);
    }

    private void HandleMovement(DirectionFlags inputDirection)
    {

        var blockedMove = CurrentSMB && !CurrentSMB.CanMove;
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
        TriggerEntered(other);
    }


    private void OnTriggerExit2D(Collider2D other) => TriggerExited(other);

}