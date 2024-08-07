using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCustomization : MonoBehaviour
{
    [SerializeField] private GameObject[] playerSkins; // Array of player skins
    [SerializeField] private Button prevSkinButton;
    [SerializeField] private Button nextSkinButton;
    [SerializeField] private Button customizeBackButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private TMP_Text skinNameText; // Text to display skin name
    [SerializeField] private TMP_Text totalCoins; // Text to display skin name
    [SerializeField] private Animator animator; // Animator component

    private int currentSkinIndex = 0;
    private int equippedSkinIndex = 0;

    private string[] skinNames = { "Casual 1", "Casual 2", "School", "Jersey", "Blazer" };

    private string[] animationTriggers = { "anim1", "anim2", "anim3", "anim4" };

    private void Start()
    {
        // Add listeners to the buttons
        prevSkinButton.onClick.AddListener(() => ChangeSkin(-1));
        nextSkinButton.onClick.AddListener(() => ChangeSkin(1));
        customizeBackButton.onClick.AddListener(BackToPreviousPanel);
        equipButton.onClick.AddListener(ToggleEquip);
        animator.SetTrigger("Idle");
        // Initialize the skins
        InitializeSkins();
        UpdateSkinDisplay();
    }

    private void InitializeSkins()
    {
        for (int i = 0; i < playerSkins.Length; i++)
        {
            playerSkins[i].SetActive(i == equippedSkinIndex);
        }

        currentSkinIndex = equippedSkinIndex;
    }

    private void ChangeSkin(int change)
    {
        playerSkins[currentSkinIndex].SetActive(false);
        currentSkinIndex += change;

        if (currentSkinIndex < 0)
        {
            currentSkinIndex = playerSkins.Length - 1;
        }
        else if (currentSkinIndex >= playerSkins.Length)
        {
            currentSkinIndex = 0;
        }

        UpdateSkinDisplay();
    }

    private void UpdateSkinDisplay()
    {
        playerSkins[currentSkinIndex].SetActive(true);
        skinNameText.text = skinNames[currentSkinIndex]; // Update the skin name display

        if (currentSkinIndex == equippedSkinIndex)
        {
            equipButton.GetComponentInChildren<TMP_Text>().text = "Unequip";
            equipButton.gameObject.SetActive(currentSkinIndex != 0);
        }
        else
        {
            equipButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            equipButton.gameObject.SetActive(true);
        }
    }

    private void ToggleEquip()
    {
        if (currentSkinIndex == equippedSkinIndex)
        {
            // Unequip the current skin and revert to default
            equippedSkinIndex = 0;
        }
        else
        {
            // Equip the selected skin
            equippedSkinIndex = currentSkinIndex;
            TriggerRandomAnimation();
        }

        UpdateSkinDisplay();
    }

    private void TriggerRandomAnimation()
    {
        // Trigger a random animation
        int randomIndex = Random.Range(0, animationTriggers.Length);
        animator.SetTrigger(animationTriggers[randomIndex]);
    }

    private void BackToPreviousPanel()
    {
        playerSkins[currentSkinIndex].SetActive(false);
        currentSkinIndex = equippedSkinIndex;
        playerSkins[currentSkinIndex].SetActive(true);

        UpdateSkinDisplay();
    }
}
