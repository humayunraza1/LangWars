using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainSettings : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Button sfxButton;
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;
    public Slider sfxSlider;
    public Button musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Slider musicSlider;
    public Button gameButton;
    public Sprite gameOnSprite;
    public Sprite gameOffSprite;
    public Slider gameSlider;

    private AudioSource audioSource;

    private bool isSfxOn = true;
    private bool isMusicOn = true;
    private bool isGameOn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set initial values for sliders
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        gameSlider.value = PlayerPrefs.GetFloat("GameVolume", 1f);

        // Apply initial volumes
        SetSFXVolume(sfxSlider.value);
        SetMusicVolume(musicSlider.value);
        SetGameVolume(gameSlider.value);

        // Add listeners to the sliders
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        gameSlider.onValueChanged.AddListener(SetGameVolume);

        // Add listeners to the buttons
        sfxButton.onClick.AddListener(OnSfxButtonClicked);
        musicButton.onClick.AddListener(OnMusicButtonClicked);
        gameButton.onClick.AddListener(OnGameButtonClicked);

        // Initialize button sprites
        UpdateButtonSprite(sfxButton, sfxOnSprite, sfxOffSprite, isSfxOn);
        UpdateButtonSprite(musicButton, musicOnSprite, musicOffSprite, isMusicOn);
        UpdateButtonSprite(gameButton, gameOnSprite, gameOffSprite, isGameOn);
    }

    void OnSfxButtonClicked()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }else{
            Debug.Log("No Audio Source");
        }

        isSfxOn = !isSfxOn;
        UpdateButtonSprite(sfxButton, sfxOnSprite, sfxOffSprite, isSfxOn);
        sfxSlider.value = isSfxOn ? 1.0f : 0.0f;
    }

    void OnMusicButtonClicked()
    {
                if (audioSource != null)
        {
            audioSource.Play();
        }else{
            Debug.Log("No Audio Source");
        }

        isMusicOn = !isMusicOn;
        UpdateButtonSprite(musicButton, musicOnSprite, musicOffSprite, isMusicOn);
        musicSlider.value = isMusicOn ? 1.0f : 0.0f;
    }

    void OnGameButtonClicked()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }else{
            Debug.Log("No Audio Source");
        }

        isGameOn = !isGameOn;
        UpdateButtonSprite(gameButton, gameOnSprite, gameOffSprite, isGameOn);
        gameSlider.value = isGameOn ? 1.0f : 0.0f;
    }

    public void SetSFXVolume(float volume)
    {
        if (volume == 0)
        {
            isSfxOn = false;
            UpdateButtonSprite(sfxButton, sfxOnSprite, sfxOffSprite, isSfxOn);
            masterMixer.SetFloat("SFX", -80f);
        }
        else
        {
            isSfxOn = true;
            UpdateButtonSprite(sfxButton, sfxOnSprite, sfxOffSprite, isSfxOn);
            masterMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == 0)
        {
            isMusicOn = false;
            UpdateButtonSprite(musicButton, musicOnSprite, musicOffSprite, isMusicOn);
            masterMixer.SetFloat("MUSIC", -80f);
        }
        else
        {
            isMusicOn = true;
            UpdateButtonSprite(musicButton, musicOnSprite, musicOffSprite, isMusicOn);
            masterMixer.SetFloat("MUSIC", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetGameVolume(float volume)
    {
        if (volume == 0)
        {
            isGameOn = false;
            UpdateButtonSprite(gameButton, gameOnSprite, gameOffSprite, isGameOn);
            masterMixer.SetFloat("GAME", -80f);
        }
        else
        {
            isGameOn = true;
            UpdateButtonSprite(gameButton, gameOnSprite, gameOffSprite, isGameOn);
            masterMixer.SetFloat("GAME", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("GameVolume", volume);
    }

    private void UpdateButtonSprite(Button button, Sprite onSprite, Sprite offSprite, bool isOn)
    {
        button.image.sprite = isOn ? onSprite : offSprite;
    }

    
}
