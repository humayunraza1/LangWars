using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons; // Array to store level buttons
    public int[] requiredScores; // Array to store required scores to unlock the next level
    public GameManager gameManager; // Reference to the GameManager
    public GameObject levelsPanel; // Reference to the levels panel
    public TMP_Text countdownText; // Reference to the countdown text
    [SerializeField] private GameObject player;
    public GameObject GameUIPanel;
    private int currentLevelIndex;

    private void Start()
    {
        UpdateLevelButtons();
        // Ensure countdown text is initially hidden
        countdownText.gameObject.SetActive(false);
        GameUIPanel.SetActive(false);
    }

    private void UpdateLevelButtons()
    {
        // Load player progress
        int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1); // Default to 1 if no value exists

        // Initialize level buttons
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < levelsUnlocked)
            {
                levelButtons[i].interactable = true; // Unlock the level
                int levelIndex = i; // Local copy for the closure
                levelButtons[i].onClick.AddListener(() => StartLevel(levelIndex));
            }
            else
            {
                levelButtons[i].interactable = false; // Lock the level
            }
        }
    }

    private void StartLevel(int levelIndex)
    {   
        currentLevelIndex = levelIndex;
        Debug.Log("StartLevel called with index: " + levelIndex);

        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is not set!");
            return;
        }

        // Hide levels panel
        levelsPanel.SetActive(false);

        // Start the countdown coroutine
        StartCoroutine(CountdownAndStartLevel());
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }

    private IEnumerator CountdownAndStartLevel()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.gameObject.SetActive(false);
        GameUIPanel.SetActive(true);
        // Call the StartLevel method in GameManager
        gameManager.StartLevel();
    }

    public void CompleteLevel(int levelIndex, int score)
    {
        // Check if the score is sufficient to unlock the next level
        GameUIPanel.SetActive(false);
        if (score >= requiredScores[levelIndex])
        {
            Debug.Log("Scores Matched " + requiredScores[levelIndex] + ". Unlocking next level");
            int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

            if (levelIndex + 1 >= levelsUnlocked)
            {
                PlayerPrefs.SetInt("LevelsUnlocked", levelIndex + 2); // Unlock the next level
                PlayerPrefs.Save();
            }
        }
        player.transform.position = new Vector3(14.32f,8.62f,-25.1f);
        UpdateLevelButtons();
        gameManager.ChangeBackCamera();
    }
}
