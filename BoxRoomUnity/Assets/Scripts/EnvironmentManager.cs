using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private static readonly int PoopAmountAnim = Animator.StringToHash("PoopAmount");
    private static readonly int FoodAmountAnim = Animator.StringToHash("FoodAmount");
    private static readonly int ExitSceneAnim = Animator.StringToHash("ExitScene");

    [Header("References")]
    public Animator EnvironmentAnimator;

    public Text ScoreText;

    [Header("State")]
    [ReadOnly] public float DayStartTime;
    [ReadOnly] public float DayProgress;

    public float PoopProgress;
    [ReadOnly] public List<PotentialPoop> PotentialPoops;

    public float FoodAmount = 1;
    public float FoodLossPerSecond = .1f;

    public int ScoreAmount;


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
    
    public void EnterCloset()
    {
        if (Time.frameCount == DebounceFrame) return;
        DebounceFrame = Time.frameCount;
        _mode = CharacterModes.UiMode;

    }
    public void EnterRoom()
    {
        if (Time.frameCount == DebounceFrame) return;
        DebounceFrame = Time.frameCount;
        _mode = CharacterModes.RoomMode;
    }

    public void EndScene()
    {
        EnvironmentAnimator.SetTrigger(ExitSceneAnim);
    }

    public void Poop()
    {
        PoopProgress = Mathf.Clamp(PoopProgress - .25f, 0, 0.5f);
    }

    public void Score(int plus)
    {
        ScoreAmount += plus;
    }

    public void Eat(PotentialPoop p)
    {
        PotentialPoops.Add(p.Clone());
        FoodAmount += p.FoodGain;
    }

    private void Update()
    {
        DayProgress = Mathf.Clamp01((Time.time - DayStartTime) / DayDuration);

        foreach (var poop in PotentialPoops)
        {

            var t = (Time.time - poop.EatTime) / poop.TimeToDigest;
            var currentPct = poop.DigestionProcess.Evaluate(t);
            var currentPoop = currentPct * poop.TotalPoop;
            var lastPoop = poop.DigestedAmount;

            var poopDif = currentPoop - lastPoop;
            PoopProgress = Mathf.Clamp01(PoopProgress + poopDif);
        }

        FoodAmount = Mathf.Clamp01(FoodAmount - FoodLossPerSecond * Time.deltaTime);
        ScoreText.text = ScoreAmount.ToString();

        EnvironmentAnimator.SetInteger(ModeAnim, (int)Mode);
        EnvironmentAnimator.SetFloat(DayProgressAnim, DayProgress);
        EnvironmentAnimator.SetFloat(PoopAmountAnim, PoopProgress);
        EnvironmentAnimator.SetFloat(FoodAmountAnim, FoodAmount);

        EnvironmentAnimator.SetBool(IsTiredAnim, IsTired);
        EnvironmentAnimator.SetBool(HasToPoopAnim, HasToPoop);

    }

}