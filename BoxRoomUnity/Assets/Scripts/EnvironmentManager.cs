using System;
using UnityEngine;

public enum CharacterModes
{
    RoomMode = 1,
    GameMode = 2,
    DreamMode = 3,
}

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance = null;
    private static readonly int ModeAnim = Animator.StringToHash("Mode");
    private static readonly int DayProgressAnim = Animator.StringToHash("DayProgress");

    [Header("References")]
    public Animator EnvironmentAnimator;

    [Header("State")]
    [ReadOnly] public float DayStartTime;
    [ReadOnly] public float DayProgress;

    [Header("Settings")]
    public float DayDuration;
    public CharacterModes Mode = CharacterModes.RoomMode;


    public void EnterGame()
    {
        Mode = CharacterModes.GameMode;
    }

    public void EnterRoom()
    {
        Mode = CharacterModes.RoomMode;
    }

    
    public void EnterDream()
    {
        Mode = CharacterModes.DreamMode;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        DayProgress = Mathf.Clamp01((Time.time - DayStartTime) / DayDuration);
        EnvironmentAnimator.SetInteger(ModeAnim, (int)Mode);
        EnvironmentAnimator.SetFloat(DayProgressAnim , DayProgress);
    }
}