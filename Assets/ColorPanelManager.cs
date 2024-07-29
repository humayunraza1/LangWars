using UnityEngine;
using UnityEngine.UI;

public class ColorPanelManager : MonoBehaviour
{
    public GameObject colorPanel; // The color panel that needs to be shown/hidden
    public Button changeColorButton; // The button to show the color panel

    void Start()
    {
        // Hide the color panel initially
        colorPanel.SetActive(false);

        // Add listeners to the buttons
        changeColorButton.onClick.AddListener(ShowColorPanel);
    }

    void ShowColorPanel()
    {
        colorPanel.SetActive(true);
    }

}
