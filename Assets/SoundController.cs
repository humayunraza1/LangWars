using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    [Range(0.1f, 3.0f)]
    public float pitch = 1.0f;
}

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public Sound[] sounds;

    public static SoundController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void PlaySound(int soundIndex)
    {
        if (sounds.Length > soundIndex && sounds[soundIndex].clip != null)
        {
            audioSource.clip = sounds[soundIndex].clip;
            audioSource.pitch = sounds[soundIndex].pitch;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid sound index or audio clip is missing.");
        }
    }
}
