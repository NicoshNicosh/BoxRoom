using System;
using UnityEngine;

[Serializable]
public class PotentialPoop
{
    [ReadOnly]
    public float EatTime;
    [ReadOnly]
    public float DigestedAmount;

    public float FoodGain;

    public float TimeToDigest;
    public float TotalPoop;
    public AnimationCurve DigestionProcess;

    public PotentialPoop Clone() => (PotentialPoop)MemberwiseClone();
}