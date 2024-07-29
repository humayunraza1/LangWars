using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonHandler : MonoBehaviour
{
    public GameObject homeCanvas;
    public GameObject settingsCanvas;
    public GameObject customizeCanvas;
    public GameObject levelsSelectorCanvas;
    public GameObject pauseMenuCanvas; // Reference to the pause menu canvas
    public Button settingsButton;
    public Button closeButton;
    public Button customizeButton;
    public Button customizeBackButton;
    public Button playButton;
    public Button levelsCloseButton;

    private AudioSource audioSource;
    private GameObject previousCanvas; // Variable to store the previous panel

    void Start()
    {
        // Ensure the canvases are set correctly
        audioSource = GetComponent<AudioSource>();

        if (homeCanvas == null)
        {
            homeCanvas = GameObject.FindWithTag("Home");
        }
        if (settingsCanvas == null)
        {
            settingsCanvas = GameObject.FindWithTag("Settings");
        }
        if (customizeCanvas == null)
        {
            customizeCanvas = GameObject.FindWithTag("Customize");
        }
        if (levelsSelectorCanvas == null)
        {
            levelsSelectorCanvas = GameObject.FindWithTag("LevelsSelector");
        }
        if (pauseMenuCanvas == null)
        {
            pauseMenuCanvas = GameObject.FindWithTag("PauseMenu");
        }

        // Ensure the settings, customize, and levels selector canvases are initially hidden
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }
        if (customizeCanvas != null)
        {
            customizeCanvas.SetActive(false);
        }
        if (levelsSelectorCanvas != null)
        {
            levelsSelectorCanvas.SetActive(false);
        }
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }

        // Add listeners to the buttons
        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        if (customizeButton != null)
        {
            customizeButton.onClick.AddListener(OnCustomizeButtonClicked);
        }

        if (customizeBackButton != null)
        {
            customizeBackButton.onClick.AddListener(OnCustomizeBackButtonClicked);
        }

        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        if (levelsCloseButton != null)
        {
            levelsCloseButton.onClick.AddListener(OnLevelsCloseButtonClicked);
        }
    }

    void OnSettingsButtonClicked()
    {
        if (homeCanvas != null && settingsCanvas != null)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.Log("No Audio Source");
            }

            previousCanvas = homeCanvas; // Track the previous canvas
            homeCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
        }
    }

    void DeactivateSettingsCanvas()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }

        if (previousCanvas != null)
        {
            previousCanvas.SetActive(true);
        }
    }

    void OnCloseButtonClicked()
    {
        if (settingsCanvas != null)
        {
            DeactivateSettingsCanvas();
        }
    }

    void OnCustomizeButtonClicked()
    {
        if (homeCanvas != null && customizeCanvas != null)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.Log("No Audio Source");
            }

            previousCanvas = homeCanvas; // Track the previous canvas
            homeCanvas.SetActive(false);
            customizeCanvas.SetActive(true);
        }
    }

    void OnCustomizeBackButtonClicked()
    {
        if (customizeCanvas != null && homeCanvas != null)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.Log("No Audio Source");
            }

            customizeCanvas.SetActive(false);
            homeCanvas.SetActive(true);
        }
    }

    void OnPlayButtonClicked()
    {
        if (homeCanvas != null && levelsSelectorCanvas != null)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.Log("No Audio Source");
            }

            previousCanvas = homeCanvas; // Track the previous canvas
            homeCanvas.SetActive(false);
            levelsSelectorCanvas.SetActive(true);
        }
    }

    void OnLevelsCloseButtonClicked()
    {
        if (levelsSelectorCanvas != null && homeCanvas != null)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.Log("No Audio Source");
            }

            levelsSelectorCanvas.SetActive(false);
            homeCanvas.SetActive(true);
        }
    }

    public void OpenSettingsFromPauseMenu()
    {
        if (pauseMenuCanvas != null && settingsCanvas != null)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.Log("No Audio Source");
            }

            previousCanvas = pauseMenuCanvas; // Track the previous canvas
            settingsCanvas.SetActive(true);
        }
    }
}
