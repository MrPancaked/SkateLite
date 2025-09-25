using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    #region Variables

    [Header("Music")]
    public AudioSource musicSource; // Music source (has the "drop section.wav" that is set to "loop" but not "play on awake")
    public AudioClip musicStart; // Intro section

    // Private stuff
    public static MusicLoop Instance { get; private set; } // Singleton instance

    #endregion

    #region Unity Methods

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; } // Singleton
        Instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    void Start() // Plays the intro, schedule the drop to loop after
    {
        musicSource.PlayOneShot(musicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }

    #endregion
}
