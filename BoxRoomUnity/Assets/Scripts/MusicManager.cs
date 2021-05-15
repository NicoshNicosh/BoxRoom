using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public EnvironmentManager environmentManager;
    public AudioSource roomMusic, gameMusic, dreamMusic;
    private Dictionary<CharacterModes, AudioSource> audioSources = new Dictionary<CharacterModes, AudioSource>();
    private AudioSource currentSource;
    // Start is called before the first frame update
    void Start()
    {
        InitializeAudioSources();

        OnModeChanged(environmentManager.Mode);
        environmentManager.OnModeChanged += OnModeChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
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
