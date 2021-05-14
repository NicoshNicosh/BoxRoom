using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public bool ModeActive;
    public Animator CharacterAnimator;
    public DirectionFlags InputDirection;
    public DirectionFlags CurrentDirection = DirectionFlags.Up;
    public List<ActionPoint> actionPoints;
    public ActionPoint CurrentAp;
    public CharacterSMB CurrentSMB;


    protected void HandleActionPoints()
    {
        var pt = actionPoints
            .Where(it => it.IsCharValid(this))
            .OrderBy(it => Vector2.Distance(it.transform.position, transform.position))
            .FirstOrDefault();

        if (pt != CurrentAp)
        {
            if (CurrentAp) CurrentAp.CharExit(this);
            CurrentAp = pt;
            if(CurrentAp) CurrentAp.CharEnter(this);
        }

    }
    
    protected void TriggerEntered(Component other)
    {
        var ap = other.GetComponent<ActionPoint>();
        actionPoints.Add(ap);
    }

    protected void TriggerExited(Component other)
    {
        var ap = other.GetComponent<ActionPoint>();
        actionPoints.Remove(ap);
    }

    protected DirectionFlags GetInputDirection()
    {
        if (!ModeActive) return DirectionFlags.None;
        var up = Input.GetKey(KeyCode.W);
        var left = Input.GetKey(KeyCode.A);
        var down = Input.GetKey(KeyCode.S);
        var right = Input.GetKey(KeyCode.D);

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