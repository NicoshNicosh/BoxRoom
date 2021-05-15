using UnityEngine;


public class AnimationControl : MonoBehaviour
{

    public BaseCharacter Character;
    public Transform DefaultRootPosition;

    public void TurnSouth(DirectionFlags direction)
    {
        Character.CurrentDirection = direction;
    }

    public void RootPoint(int? ptNumber)
    {

        var target = ptNumber.HasValue ? ((ActionPoint)Character.CurrentEntity).RootPoints[ptNumber.Value] : DefaultRootPosition;
        var t = transform;
        t.position = target.position;
        t.rotation = target.rotation;
    }
}
