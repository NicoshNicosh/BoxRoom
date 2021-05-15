using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCharacter : MonoBehaviour
{

    private static readonly int AttackAnim = Animator.StringToHash("Attack");

    public abstract bool ModeActive { get; }
    public Animator CharacterAnimator;
    public DirectionFlags InputDirection;
    public DirectionFlags CurrentDirection = DirectionFlags.Up;
    [ReadOnly] public ActionPoint CurrentAp;
    private List<ActionPoint> ActionPoints = new List<ActionPoint>();
    [HideInInspector] public CharacterSMB CurrentSMB;
    public UnityEvent OnEscPressed;

    protected void HandleActionPoints()
    {
        var pt = ActionPoints
            .Where(it => it.IsCharValid(this))
            .OrderBy(it => Vector2.Distance(it.transform.position, transform.position))
            .FirstOrDefault();

        if (pt != CurrentAp)
        {
            if (CurrentAp) CurrentAp.CharExit(this);
            CurrentAp = pt;
            if (CurrentAp) CurrentAp.CharEnter(this);
        }
    }

    protected void HandleEscPressed()
    {
        if (ModeActive && Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscPressed.Invoke();
        }
    }

    protected void HandleInteraction()
    {
        if (ModeActive && CurrentAp && Input.GetKeyDown(KeyCode.Return))
        {
            CurrentAp.CharInteract();
        }
    }

    protected void HandleAttack()
    {
        var ShiftPressed = Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift);
        if (ModeActive && ShiftPressed)
        {
            CharacterAnimator.SetTrigger(AttackAnim); //Todo: Prevent Double attacks 
        }
    }

    protected void TriggerEntered(Component other)
    {
        var ap = other.GetComponent<ActionPoint>();
        ActionPoints.Add(ap);
    }

    protected void TriggerExited(Component other)
    {
        var ap = other.GetComponent<ActionPoint>();
        ActionPoints.Remove(ap);
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