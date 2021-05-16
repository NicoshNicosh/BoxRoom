using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private static readonly int IsTiredAnim = Animator.StringToHash("IsTired");
    private static readonly int HasToPoopAnim = Animator.StringToHash("HasToPoop");
    private static readonly int ExitSceneAnim = Animator.StringToHash("ExitScene");
    
    [Header("References")]
    public Animator EnvironmentAnimator;

    [Header("State")]
    [ReadOnly] public float DayStartTime;
    [ReadOnly] public float DayProgress;

    [ReadOnly] public float PoopProgress;
    [ReadOnly] public List<PotentialPoop> DigestingPoop;

    [Serializable]
    public class PotentialPoop
    {
        public float EatTime;
        public float TotalPoop;
        public float TimeToDigest;
    }

    [Header("Settings")]
    public float MinPoopToPoop = 0.5f;
    public float MinDurationToSleep = 0.8f;
    public float DayDuration = 60;

    [SerializeField] private CharacterModes _mode = CharacterModes.RoomMode;
    private int DebounceFrame = 0;

    public CharacterModes Mode => _mode;
    public bool HasToPoop => PoopProgress > MinPoopToPoop;
    public bool IsTired => DayProgress > MinDurationToSleep;


    private void Awake()
    {
        _instance = this;
    }

    public void EnterGame()
    {
        if (Time.frameCount == DebounceFrame) return;
        DebounceFrame = Time.frameCount;
        _mode = CharacterModes.GameMode;
    }

    public void EnterRoom()
    {
        if (Time.frameCount == DebounceFrame) return;
        DebounceFrame = Time.frameCount;
        _mode = CharacterModes.RoomMode;
    }
    
    public void EnterDream()
    {
        EnvironmentAnimator.SetTrigger(ExitSceneAnim);
    }
    
    private void Update()
    {
        DayProgress = Mathf.Clamp01((Time.time - DayStartTime) / DayDuration);
        EnvironmentAnimator.SetInteger(ModeAnim, (int)Mode);
        EnvironmentAnimator.SetFloat(DayProgressAnim, DayProgress);

        EnvironmentAnimator.SetBool(IsTiredAnim, IsTired);
        EnvironmentAnimator.SetBool(HasToPoopAnim, HasToPoop);
    }
}