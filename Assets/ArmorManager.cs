using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmorManager : MonoBehaviour
{
    [SerializeField] private GameObject armor; // The armor GameObject
    [SerializeField] private GameObject PlayerArmor; // The armor GameObject
    [SerializeField] private Button prevButton; // The previous button
    [SerializeField] private Button nextButton; // The next button
    [SerializeField] private Button equipButton; // The equip/unequip button
    [SerializeField] private TMP_Text statusText; // The status Text field

    private bool isArmorPermanentlyAttached = false;
    private bool isOnArmorPage = false;

    void Start()
    {
        // Initially disable the armor and buttons
        armor.SetActive(false);
        PlayerArmor.SetActive(false);
        prevButton.interactable = false;
        equipButton.gameObject.SetActive(false);

        // Add listeners to the buttons
        nextButton.onClick.AddListener(ShowArmorPage);
        prevButton.onClick.AddListener(HideArmorPage);
        equipButton.onClick.AddListener(ToggleEquipArmor);

        UpdateStatusText();
    }

    void ShowArmorPage()
    {
        isOnArmorPage = true;
        armor.SetActive(true);
        nextButton.interactable = false;
        prevButton.interactable = true;
        equipButton.gameObject.SetActive(true);
        UpdateEquipButton();
        UpdateStatusText(); // Update the status text when showing the armor page
    }

    void HideArmorPage()
    {
        isOnArmorPage = false;
        armor.SetActive(isArmorPermanentlyAttached);
        nextButton.interactable = true;
        prevButton.interactable = false;
        equipButton.gameObject.SetActive(false);
        UpdateStatusText(); // Update the status text when hiding the armor page
    }

    void ToggleEquipArmor()
    {
        isArmorPermanentlyAttached = !isArmorPermanentlyAttached;
        UpdateEquipButton();
        PlayerArmor.SetActive(isArmorPermanentlyAttached);
        armor.SetActive(isOnArmorPage);
    }

    void UpdateEquipButton()
    {
        // Check if equipButton is assigned
        if (equipButton == null)
        {
            Debug.LogError("Equip Button is not assigned.");
            return;
        }

        // Get the Text component of the equipButton
        TMP_Text equipButtonText = equipButton.GetComponentInChildren<TMP_Text>();
        if (equipButtonText == null)
        {
            Debug.LogError("Equip Button Text component is not found.");
            return;
        }

        if (isArmorPermanentlyAttached)
        {
            equipButtonText.text = "Unequip";
        }
        else
        {
            equipButtonText.text = "Equip";
        }
    }

    void UpdateStatusText()
    {
        if (statusText == null)
        {
            Debug.LogError("Status Text is not assigned.");
            return;
        }

        if (isOnArmorPage)
        {
            statusText.text = "Armored";
        }
        else
        {
            statusText.text = "Default";
        }
    }

    public void BackOut()
    {
        if (!isArmorPermanentlyAttached)
        {
            armor.SetActive(false);
        isOnArmorPage = false;
        nextButton.interactable = true;
        prevButton.interactable = false;
        equipButton.gameObject.SetActive(false);
        UpdateStatusText(); // Update the status text when backing out
        Debug.Log("Close Customization Panel");
        }else{
        isOnArmorPage = true;
        nextButton.interactable = false;
        prevButton.interactable = true;
        equipButton.gameObject.SetActive(true);
        UpdateStatusText(); // Update the status text when backing out
        Debug.Log("Close Customization Panel");
        }
    }
}
