using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class WinPanelController : MonoBehaviour
{
   [SerializeField] private GameObject winPanel; // Reference to the WinPanel GameObject
   [SerializeField] private Button okButton;
   [SerializeField] private TMP_Text coinNumber;
   [SerializeField] private TMP_Text scoreNum;
   [SerializeField] private TMP_Text winText;
   public LevelManager lManager;
   private int lIndex;
   private int pscore;
    void Start()
    {
        // Ensure the win panel is initially hidden
        winPanel.SetActive(false);
        okButton.onClick.AddListener(NextLevel);
        // Add a listener to the button to call ShowWinPanel method when clicked
    }

    public void ShowWinPanel(int numCoins,int score,int LevelIndex){
        pscore = score;
        lIndex = LevelIndex;
        winText.text = $"Level {LevelIndex+1} Completed";
        coinNumber.text = $"{numCoins}";
        scoreNum.text = $"{score}";
        winPanel.SetActive(true);
    }

    void NextLevel(){
        winPanel.SetActive(false);
        lManager.CompleteLevel(lIndex,pscore);
    }
}
