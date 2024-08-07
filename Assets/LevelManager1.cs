using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager1 : MonoBehaviour
{
    private CoinCollector coinCollector;
    private CoinManager coinsManager;

    [SerializeField] private LevelManager levelM;
    public WinPanelController winPanel;

    private void Start() {
        coinCollector = FindObjectOfType<CoinCollector>();
        coinsManager = FindObjectOfType<CoinManager>();

        if (coinCollector == null) {
            Debug.LogError("CoinCollector script not found in the scene.");
        }

        if (coinsManager == null) {
            Debug.LogError("CoinManager script not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Finish")) {
            if (coinCollector != null && coinsManager != null) {
                int levelCoins = coinCollector.GetTotalCoinsCollected();
                int score = coinCollector.GetTotalScore();
                int currLevelIndex = levelM.GetCurrentLevelIndex();
                coinsManager.AddCoins(levelCoins);
                int totalCoins = coinsManager.GetTotalCoins();
                Debug.Log("Level Completed");
                Debug.Log("Level " + currLevelIndex + " Completed");
                Debug.Log("Coins collected this round: " + levelCoins);
                Debug.Log("Total coins: " + totalCoins);
                Debug.Log("Total Score: " + score);
                winPanel.ShowWinPanel(levelCoins, score, currLevelIndex);
            }
        }
    }
}
