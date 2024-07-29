using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TMP_Text playerScoreText; // Reference to the player score UI text
    [SerializeField] public TMP_Text totalCoinsText; // Reference to the total coins collected UI text

     private int playerScore = 0;
    private int totalCoins = 0;

    void Start()
    {
        UpdatePlayerScoreUI(playerScore); // Ensure the UI is initialized correctly
        UpdateTotalCoinsUI(totalCoins); // Ensure the UI is initialized correctly
    }

    public void UpdatePlayerScoreUI(int totalScore)
    {
        playerScoreText.text = "" + totalScore;
    }

    public void UpdateTotalCoinsUI(int coins)
    {
        totalCoinsText.text = "" + coins;
    }
}
