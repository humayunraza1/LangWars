using UnityEngine;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{
    public GameObject winUI;  // Reference to the result UI
    public GameObject loseUI;  // Reference to the result UI

     public GameManager gameManager; // Reference to the GameManager
    public Button loseButton;

    void Start()
    {
        loseButton.onClick.AddListener(OnOKButtonClicked);
        // Ensure the result UI is not active at the start
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    // Function to handle the win condition
    public void HandleWin()
    {
        winUI.SetActive(true);
        // Optionally, pause the game
        Time.timeScale = 0f;
    }

    // Function to handle the lose condition
    public void HandleLose()
    {
        loseUI.SetActive(true);
        // Optionally, pause the game
        Time.timeScale = 0f;
    }

    // Function to be called when the "OK" button is clicked
    public void OnOKButtonClicked()
    {
        // Resume the game if it was paused
        Time.timeScale = 1f;
        // Load the level selection scene
        loseUI.SetActive(false);
        gameManager.ChangeBackCamera();
    }
}
