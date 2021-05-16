using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class BaseCharacter : MonoBehaviour
{

    private static readonly int AttackAnim = Animator.StringToHash("Attack");
    private static readonly int IsWalkingAnim = Animator.StringToHash("IsWalking");

    [Header("Base Character")]
    public Animator CharacterAnimator;
    [ReadOnly] public DirectionFlags InputDirection;
    [ReadOnly] public DirectionFlags CurrentDirection = DirectionFlags.Up;
    [ReadOnly] public BaseEntity CurrentEntity;
    
    private readonly List<BaseEntity> _entities = new List<BaseEntity>();
    protected CharacterSMB CurrentSmb => SMBs.LastOrDefault();
    public readonly List<CharacterSMB> SMBs = new List<CharacterSMB>();

    public abstract bool ModeActive { get; }

    
    protected virtual void Update()
    {
        bool isMoving = false;

        if (ModeActive)
        {
            var canMove = !CurrentSmb || CurrentSmb.CanMove;
            if (canMove)
            {
                InputDirection = GetInputDirection();
                isMoving = InputDirection != DirectionFlags.None;
                HandleMovement(InputDirection);
            }

            if (isMoving) CurrentDirection = InputDirection;
        }

        CharacterAnimator.SetBool(IsWalkingAnim, isMoving);
        HandleRotation();
        HandleActionPoints();

        if (ModeActive)
        {
            var canAttack = !CurrentSmb || CurrentSmb.CanAttack;
            if (canAttack) HandleAttack();

            var canInteract =  !CurrentSmb || CurrentSmb.CanInteract;
            if (canInteract) HandleInteraction();
        }
    }

    protected abstract void HandleInteraction();

    protected abstract void HandleRotation();

    protected abstract void HandleMovement(DirectionFlags inputDirection);

    protected void HandleActionPoints()
    {
        _entities.RemoveAll(it => !it);

        var pt = _entities
            .Where(it => it.IsCharValid(this))
            .OrderBy(it => Vector2.Distance(it.transform.position, transform.position))
            .FirstOrDefault();

        if (pt != CurrentEntity)
        {
            if (CurrentEntity) CurrentEntity.CharExit(this);
            CurrentEntity = pt;
            if (CurrentEntity) CurrentEntity.CharEnter(this);
        }
    }
    

    protected void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("HandleAttack");
            CharacterAnimator.SetTrigger(AttackAnim); //Todo: Prevent Double attacks 
            PlayAttackSound();
        }
    }
    
    protected DirectionFlags GetInputDirection()
    {
        var up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        var left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        var down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        var right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (up && left) return DirectionFlags.UpLeft;
        if (up && right) return DirectionFlags.UpRight;
        if (up) return DirectionFlags.Up;
        if (down && left) return DirectionFlags.DownLeft;
        if (down && right) return DirectionFlags.DownRight;
        if (down) return DirectionFlags.Down;
        if (left) return DirectionFlags.Left;
        if (right) return DirectionFlags.Right;

        return DirectionFlags.None;
    }
    protected void EntityEnter(Component other)
    {
        var ap = other.GetComponent<BaseEntity>();
        _entities.Add(ap);
    }

    protected void EntityExit(Component other)
    {
        var ap = other.GetComponent<BaseEntity>();
        _entities.Remove(ap);
    }

    public AudioSource attackSound;
    int noteCounter = 0;
    int[] majorScale = new int[] { 0, 2, 4, 5, 7, 9, 11 };
    float prevAttackTime = 0;
    float attackComboMaxTimeDiff = 0.5f;
    protected virtual void PlayAttackSound()
    {
        float timeDiff = Time.time - prevAttackTime;
        if (timeDiff > attackComboMaxTimeDiff)
		{
            noteCounter = 0;
		}
        prevAttackTime = Time.time;
        int semitones = majorScale[noteCounter % majorScale.Length] + (noteCounter / majorScale.Length * 12);
        attackSound.pitch = Mathf.Pow(2, (float)semitones / 12);
        noteCounter++;
        attackSound.PlayOneShot(attackSound.clip);
        Debug.Log("PlayAttackSound");
    }
}