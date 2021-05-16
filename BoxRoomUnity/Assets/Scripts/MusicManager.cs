using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;

    public AudioSource roomMusic, gameMusic, dreamMusic;
    private Dictionary<CharacterModes, AudioSource> audioSources = new Dictionary<CharacterModes, AudioSource>();
    private AudioSource currentSource;

    private CharacterModes oldMode;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        InitializeAudioSources();

        oldMode = EnvironmentManager.Instance.Mode;
        OnModeChanged(oldMode);
        DontDestroyOnLoad(this.gameObject);
    }

    
    // Update is called once per frame
    void Update()
    {
        var newMode = EnvironmentManager.Instance.Mode;
        if (newMode != CharacterModes.UiMode && newMode != oldMode)
        {
            OnModeChanged(newMode);
            oldMode = newMode;
        }
    }

    private void InitializeAudioSources()
	{
        audioSources[CharacterModes.RoomMode] = roomMusic;
        audioSources[CharacterModes.GameMode] = gameMusic;
        audioSources[CharacterModes.DreamMode] = dreamMusic;
    }

    private void OnModeChanged(CharacterModes newMode)
	{
        
        Debug.Log("Mode Changed to " + newMode.ToString());
        AudioSource nextSource = audioSources[newMode];
        if (nextSource == currentSource) return;

        if (currentSource != null) currentSource.Pause();

        nextSource.Play();
        currentSource = nextSource;
    }
}
