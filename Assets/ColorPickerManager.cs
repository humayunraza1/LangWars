using UnityEngine;
using UnityEngine.UI;

public class ColorPickerManager : MonoBehaviour
{
    public FlexibleColorPicker colorPicker;
    public Button changeColorButton;
    public Button closeButton;
    void Start()
    {
        // Initially hide the color picker
        if (colorPicker != null)
        {
            colorPicker.gameObject.SetActive(false);
        }

        // Add listeners to the buttons
        if (changeColorButton != null)
        {
            changeColorButton.onClick.AddListener(OnChangeColorButtonClicked);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
    }

    void DeactivateColorCanvas()
    {
        if (colorPicker != null)
        {
            colorPicker.gameObject.SetActive(false);
        }
    }

    void OnChangeColorButtonClicked()
    {
        if (colorPicker != null)
        {
            colorPicker.gameObject.SetActive(true);
        }
    }

    void OnCloseButtonClicked()
    {
        if (colorPicker != null)
        {
            Invoke("DeactivateColorCanvas", 0.5f); // Adjust the delay to match the animation duration
        }
    }
}
