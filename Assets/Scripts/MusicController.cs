using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioMixerGroup musicGroup;
    [Range(0f, 1f)] public float volume = 0.8f;

    void Start()
    {
        // Auto create Audio Source if missing
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.outputAudioMixerGroup = musicGroup;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();

        // Keep music playing between scenes
        DontDestroyOnLoad(gameObject);
    }

    // Call this to change music anytime
    public void ChangeMusic(AudioClip newClip)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.clip = newClip;
        audio.Play();
    }
}