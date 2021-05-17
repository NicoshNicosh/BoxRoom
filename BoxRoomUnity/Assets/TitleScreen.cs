using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    public void OnStartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnRestart()
    {
        if(MusicManager.Instance) Destroy(MusicManager.Instance.gameObject);
        if(SpawnableManager.Instance) Destroy(SpawnableManager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
