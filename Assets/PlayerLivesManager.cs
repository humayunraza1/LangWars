using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesManager : MonoBehaviour
{
    public Image[] hearts;  // Array of heart images
    public Sprite fullHeart;  // Sprite for a full heart
    public Sprite emptyHeart;  // Sprite for an empty heart
    public GameResultManager gameResultManager;  // Reference to the GameResultManager script

    private int lives = 3;  // Number of player lives

    void Start()
    {
        // Initialize hearts to full on level start
        ResetLives();
    }

    public void ResetLives()
    {
        lives = 3;
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = fullHeart;
        }
        // Ensure the game result UI is inactive at the start
        gameResultManager.loseUI.SetActive(false);
        gameResultManager.winUI.SetActive(false);
    }

    // Function to reduce lives and update heart sprites
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            hearts[lives].sprite = emptyHeart;
            Debug.Log("Lives left " + lives);
            if (lives == 0)
            {
                gameResultManager.HandleLose();
            }
        }
    }

    // Function to be called when the player wins
    public void PlayerWins()
    {
        gameResultManager.HandleWin();
    }
}
