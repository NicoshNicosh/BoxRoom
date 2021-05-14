using System;
using UnityEngine;

[Flags]
public enum DirectionFlags
{
    None        = 0b0,
    Everything  = 0b11111111,
    UpLeft      = 0b00000001,
    Up          = 0b00000010,
    UpRight     = 0b00000100,
    Left        = 0b00001000,
    Right       = 0b00010000,
    DownLeft    = 0b00100000,
    Down        = 0b01000000,
    DownRight   = 0b10000000,
}

public static class DirectionUtils
{


    public static Vector2 ToVector(this DirectionFlags inputDirection)
    {
        switch (inputDirection)
        {
            case DirectionFlags.None: return Vector2.zero;
            case DirectionFlags.UpLeft: return (Vector2.up + Vector2.left).normalized;
            case DirectionFlags.Up: return Vector2.up;
            case DirectionFlags.UpRight: return (Vector2.up + Vector2.right).normalized;
            case DirectionFlags.Left: return Vector2.left;
            case DirectionFlags.Right: return Vector2.right;
            case DirectionFlags.DownLeft: return (Vector2.down + Vector2.left).normalized;
            case DirectionFlags.Down: return Vector2.down;
            case DirectionFlags.DownRight: return (Vector2.down + Vector2.right).normalized;
            default: throw new ArgumentOutOfRangeException(nameof(inputDirection), inputDirection, null);
        }
    }

    public static DirectionFlags GetInputDirection()
    {
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

    public static Quaternion ToRotation(this DirectionFlags inputDirection)
    {
        switch (inputDirection)
        {
            case DirectionFlags.Up:         return Quaternion.Euler(0,0, 45*0);
            case DirectionFlags.UpLeft:     return Quaternion.Euler(0,0, 45*1);
            case DirectionFlags.Left:       return Quaternion.Euler(0,0, 45*2);
            case DirectionFlags.DownLeft:   return Quaternion.Euler(0,0, 45*3);
            case DirectionFlags.Down:       return Quaternion.Euler(0,0, 45*4);
            case DirectionFlags.DownRight:  return Quaternion.Euler(0,0, 45*5);
            case DirectionFlags.Right:      return Quaternion.Euler(0,0, 45*6);
            case DirectionFlags.UpRight:    return Quaternion.Euler(0,0, 45*7);
            default: throw new ArgumentOutOfRangeException(nameof(inputDirection), inputDirection, null);
        }

    }

}