using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;

    public AudioSource roomMusic, gameMusic, dreamMusic, dream3Music, poopFart;
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

        if (SceneManager.GetActiveScene().name.Contains("Dream2"))
		{
            MessWithMusic();
        }
        else
		{
            CorrectMusic();
        }
    }

    public float pitchWaveFreq = 1f;
    public float pitchWaveAmp = 0.3f;
    void MessWithMusic()
	{
        dreamMusic.pitch = 1f + Mathf.Sin(Time.time * pitchWaveFreq) * pitchWaveAmp;
        // dreamMusic.panStereo = Mathf.Sin(Time.time * pitchWaveFreq);
    }

    void CorrectMusic()
	{
        dreamMusic.pitch = 1f;
        // dreamMusic.panStereo = 0f;
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
        if (!audioSources.ContainsKey(newMode)) return;
        AudioSource nextSource = audioSources[newMode];

        if (SceneManager.GetActiveScene().name.Contains("Dream3"))
        {
            nextSource = dream3Music;
        }

        if (nextSource == currentSource) return;

        if (currentSource != null) currentSource.Pause();

        nextSource.Play();
        currentSource = nextSource;
    }
}
