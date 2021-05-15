using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class BaseCharacter : MonoBehaviour
{

    private static readonly int AttackAnim = Animator.StringToHash("Attack");

    [Header("Base Character")]
    public Animator CharacterAnimator;
    [ReadOnly] public DirectionFlags InputDirection;
    [ReadOnly] public DirectionFlags CurrentDirection = DirectionFlags.Up;
    [ReadOnly] public BaseEntity CurrentEntity;
    
    private readonly List<BaseEntity> _entities = new List<BaseEntity>();
    internal CharacterSMB CurrentSmb;
    public abstract bool ModeActive { get; }

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
        var shiftPressed = Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift);
        if (ModeActive && shiftPressed)
        {
            CharacterAnimator.SetTrigger(AttackAnim); //Todo: Prevent Double attacks 
        }
    }

    protected void EntityEnter(Component other)
    {
        Debug.Log("Enter: " + other);
        var ap = other.GetComponent<BaseEntity>();
        _entities.Add(ap);
    }

    protected void EntityExit(Component other)
    {
        Debug.Log("Exit: " + other);
        var ap = other.GetComponent<BaseEntity>();
        _entities.Remove(ap);
    }

    protected DirectionFlags GetInputDirection()
    {
        if (!ModeActive) return DirectionFlags.None;
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

}