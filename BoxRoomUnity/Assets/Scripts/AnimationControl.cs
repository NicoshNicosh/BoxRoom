using UnityEngine;


public class AnimationControl : MonoBehaviour
{

    public BaseCharacter Character;

    public void TurnSouth(DirectionFlags direction)
    {
        Character.CurrentDirection = direction;
    }

    public void RootPoint(int ptNumber)
    {

        var target = ((ActionPoint)Character.CurrentEntity).RootPoints[ptNumber];
        var t = transform;
        t.position = target.position;
        t.rotation = target.rotation;
    }
}
