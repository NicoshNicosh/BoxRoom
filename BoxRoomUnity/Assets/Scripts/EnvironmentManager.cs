﻿using System;
using UnityEngine;

public enum CharacterModes
{
    RoomMode = 1,
    GameMode = 2,
    DreamMode = 3,
    UiMode = 4,
}

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager _instance;

    public static EnvironmentManager Instance =>
        _instance ? _instance : _instance = FindObjectOfType<EnvironmentManager>();

    private static readonly int ModeAnim = Animator.StringToHash("Mode");
    private static readonly int DayProgressAnim = Animator.StringToHash("DayProgress");

    [Header("References")]
    public Animator EnvironmentAnimator;

    [Header("State")]
    [ReadOnly] public float DayStartTime;
    [ReadOnly] public float DayProgress;

    [Header("Settings")]
    public float DayDuration;

    [SerializeField] private CharacterModes _mode = CharacterModes.RoomMode;

    private int DebounceFrame = 0;

    public CharacterModes Mode => _mode;

    public event Action<CharacterModes> OnModeChanged;


    public void EnterGame()
    {
        if (Time.frameCount == DebounceFrame) return;
        DebounceFrame = Time.frameCount;
        _mode = CharacterModes.GameMode;
        if (OnModeChanged != null) OnModeChanged(_mode);
    }

    public void EnterRoom()
    {
        if (Time.frameCount == DebounceFrame) return;
        DebounceFrame = Time.frameCount;
        _mode = CharacterModes.RoomMode;
        if (OnModeChanged != null) OnModeChanged(_mode);
    }


    public void EnterDream()
    {
        if (Time.frameCount == DebounceFrame) return;
        DebounceFrame = Time.frameCount;
        _mode = CharacterModes.DreamMode;
        if (OnModeChanged != null) OnModeChanged(_mode);
    }
    
    private void Update()
    {
        DayProgress = Mathf.Clamp01((Time.time - DayStartTime) / DayDuration);
        EnvironmentAnimator.SetInteger(ModeAnim, (int)Mode);
        EnvironmentAnimator.SetFloat(DayProgressAnim, DayProgress);
    }
}