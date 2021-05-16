using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSceneSMB : StateMachineBehaviour
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var manager = EnvironmentManager.Instance;
        var isDeath = manager&&EnvironmentManager.Instance.FoodAmount <= 0;
        if(isDeath) SceneManager.LoadScene(manager.DeathScene.buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}