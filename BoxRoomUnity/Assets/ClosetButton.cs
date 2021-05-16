using UnityEngine;
using UnityEngine.Events;

public abstract class ClosetButton : MonoBehaviour
{
    protected readonly EnvironmentManager Manager = EnvironmentManager.Instance;
    public UnityEvent OnBadPurchase;
    public UnityEvent OnGoodPurchase;
    public int Cost;

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

    public abstract void OnPurchase();

    public void TriggerOnCharacter(string triggerName)
    {
        FindObjectOfType<CharacterReal>().CharacterAnimator.SetTrigger(triggerName);
    }

}