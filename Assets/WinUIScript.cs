using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WinUIScript : MonoBehaviour
{
    public TMP_Text winText; // Reference to the Text component that displays the win message
    public CoinCollector coinCollector; // Reference to the CoinCollector script

    private void Start()
    {
        // Ensure the win UI is hidden at the start
        gameObject.SetActive(false);
    }

    public void ShowWinUI()
    {
        int levelCoins = coinCollector.GetTotalCoinsCollected();
        winText.text = $"<color=yellow>{levelCoins}</color>";
        gameObject.SetActive(true); // Show the win UI
    }
}
