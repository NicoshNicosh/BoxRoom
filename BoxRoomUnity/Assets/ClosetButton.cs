using UnityEngine;
using UnityEngine.Events;

public abstract class ClosetButton : MonoBehaviour
{
    protected  EnvironmentManager Manager => EnvironmentManager.Instance;
    public UnityEvent OnBadPurchase;
    public UnityEvent OnGoodPurchase;
    public int Cost;
    private static readonly int PopsicleNum = Animator.StringToHash("PopsicleNum");

    public void Purchase()
    {

        if (Cost <= Manager.ScoreAmount)
        {
            Manager.ScoreAmount -= Cost;
            OnPurchase();
            OnGoodPurchase.Invoke();
        }
        else
        {
            OnBadPurchase.Invoke();
        }
    }

    protected abstract void OnPurchase();

    public void TriggerOnCharacter(string triggerName)
    {
        CharacterReal.Instance.CharacterAnimator.SetTrigger(triggerName);
    }

    public void SetCharacterPopsicle(int popNum)
    {
        CharacterReal.Instance.CharacterAnimator.SetInteger(PopsicleNum, popNum);
    }
}