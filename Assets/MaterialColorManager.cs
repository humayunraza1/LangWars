using UnityEngine;
using UnityEngine.UI;

public class MaterialColorManager : MonoBehaviour
{
    public Toggle skinToggle;
    public Toggle armorToggle;
    public Material bodyMaterial;
    public Material armorMaterial;
    public FlexibleColorPicker colorPicker;
    public Button applyButton;

    private Material currentMaterial;

    void Start()
    {
        // Ensure only one tab is active at the start
        skinToggle.isOn = true;
        armorToggle.isOn = false;

        // Set initial color picker color from the body material
        SetColorPickerColor(bodyMaterial);
        currentMaterial = bodyMaterial; // Set current material to bodyMaterial initially

        // Add listeners to the toggles and the apply button
        skinToggle.onValueChanged.AddListener(OnSkinToggleChanged);
        armorToggle.onValueChanged.AddListener(OnArmorToggleChanged);
        applyButton.onClick.AddListener(OnApplyButtonClicked);
    }

    void OnSkinToggleChanged(bool isOn)
    {
        if (isOn)
        {
            armorToggle.isOn = false; // Ensure only one toggle is active
            SetColorPickerColor(bodyMaterial); // Set color picker color from body material
            currentMaterial = bodyMaterial; // Set current material to body material
        }
    }

    void OnArmorToggleChanged(bool isOn)
    {
        if (isOn)
        {
            skinToggle.isOn = false; // Ensure only one toggle is active
            SetColorPickerColor(armorMaterial); // Set color picker color from armor material
            currentMaterial = armorMaterial; // Set current material to armor material
        }
    }

    void SetColorPickerColor(Material material)
    {
        if (material != null)
        {
            colorPicker.color = material.color; // Set color picker to material's color
        }
    }

    void OnApplyButtonClicked()
    {
        if (skinToggle.isOn && bodyMaterial != null)
        {
            bodyMaterial.color = colorPicker.color; // Apply color picker color to body material
        }
        else if (armorToggle.isOn && armorMaterial != null)
        {
            armorMaterial.color = colorPicker.color; // Apply color picker color to armor material
        }
    }
}
