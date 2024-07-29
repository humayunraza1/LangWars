using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; // The pause menu panel
    public Button pauseButton; // The pause button
    public Button resumeButton; // The resume button
    public Button settingsButton; // The settings button
    public Button quitButton; // The quit button
    public TMP_Text resumeTimerText; // The resume timer text

    public SettingsButtonHandler settingsButtonHandler; // Reference to SettingsButtonHandler script

    private float resumeDelay = 3f;

    void Start()
    {
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        pauseMenuPanel.SetActive(true); // Show the pause menu
    }

    void ResumeGame()
    {
        StartCoroutine(ResumeGameCountdown());
    }

    void OpenSettings()
    {
        if (settingsButtonHandler != null)
        {
            settingsButtonHandler.OpenSettingsFromPauseMenu();
        }
    }

    void QuitGame()
    {
        // Implement quit game logic here
        Debug.Log("Quit Game");
        // Example:
        // Application.Quit();
    }

    IEnumerator ResumeGameCountdown()
    {
        pauseMenuPanel.SetActive(false); // Hide the pause menu
        float timeRemaining = resumeDelay;

        resumeTimerText.gameObject.SetActive(true);
        while (timeRemaining > 0)
        {
            resumeTimerText.text = timeRemaining.ToString("0");
            yield return new WaitForSecondsRealtime(1f);
            timeRemaining--;
        }
        resumeTimerText.gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}
