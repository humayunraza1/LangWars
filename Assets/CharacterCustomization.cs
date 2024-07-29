using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMesh Pro namespace

[System.Serializable]
public class ColorOption
{
    public string name;
    public string hexCode;
    public Color color;
}

public class CharacterCustomization : MonoBehaviour
{
    [SerializeField] private Material hairMaterial;
    [SerializeField] private Material beardMaterial;
    [SerializeField] private Material skinMaterial;

    [SerializeField] private Button prevHairButton;
    [SerializeField] private Button nextHairButton;
    [SerializeField] private Button prevBeardButton;
    [SerializeField] private Button nextBeardButton;
    [SerializeField] private Button prevSkinButton;
    [SerializeField] private Button nextSkinButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private GameObject colorPanel;

    [SerializeField] private TMP_Text hairColorText;  // Use TMP_Text instead of Text
    [SerializeField] private TMP_Text beardColorText; // Use TMP_Text instead of Text
    [SerializeField] private TMP_Text skinColorText;  // Use TMP_Text instead of Text

    private int currentHairColorIndex = 0;
    private int currentBeardColorIndex = 0;
    private int currentSkinColorIndex = 0;

    [SerializeField] private ColorOption[] hairColors;
    [SerializeField] private ColorOption[] skinColors;

    private void Awake()
    {
        // Initialize hair colors
        hairColors = new ColorOption[]
        {
            new ColorOption { name = "Black", hexCode = "#000000", color = Color.black },
            new ColorOption { name = "Brown", hexCode = "#8B4513", color = new Color(0.545f, 0.271f, 0.075f) },
            new ColorOption { name = "Dark Brown", hexCode = "#654321", color = new Color(0.396f, 0.259f, 0.129f) },
            new ColorOption { name = "Blonde", hexCode = "#FFFACD", color = new Color(1.0f, 0.980f, 0.803f) },
            new ColorOption { name = "Red", hexCode = "#FF4500", color = new Color(1.0f, 0.271f, 0.0f) },
            new ColorOption { name = "Ash", hexCode = "#B2BEB5", color = new Color(0.698f, 0.745f, 0.710f) }
        };

        // Initialize skin colors
        skinColors = new ColorOption[]
        {
            new ColorOption { name = "Fair", hexCode = "#FFDFC4", color = new Color(1.0f, 0.875f, 0.769f) },
            new ColorOption { name = "Tan", hexCode = "#D2B48C", color = new Color(0.824f, 0.706f, 0.549f) },
            new ColorOption { name = "Brown", hexCode = "#8B4513", color = new Color(0.545f, 0.271f, 0.075f) },
            new ColorOption { name = "Dark", hexCode = "#654321", color = new Color(0.396f, 0.259f, 0.129f) }
        };
    }

    private void Start()
    {
        // Add listeners to the buttons
        prevHairButton.onClick.AddListener(() => ChangeColor(ref currentHairColorIndex, hairColors, -1, hairMaterial, hairColorText));
        nextHairButton.onClick.AddListener(() => ChangeColor(ref currentHairColorIndex, hairColors, 1, hairMaterial, hairColorText));
        prevBeardButton.onClick.AddListener(() => ChangeColor(ref currentBeardColorIndex, hairColors, -1, beardMaterial, beardColorText));
        nextBeardButton.onClick.AddListener(() => ChangeColor(ref currentBeardColorIndex, hairColors, 1, beardMaterial, beardColorText));
        prevSkinButton.onClick.AddListener(() => ChangeColor(ref currentSkinColorIndex, skinColors, -1, skinMaterial, skinColorText));
        nextSkinButton.onClick.AddListener(() => ChangeColor(ref currentSkinColorIndex, skinColors, 1, skinMaterial, skinColorText));
        CloseButton.onClick.AddListener(HideColorPanel);
        // Initialize the colors
        UpdateMaterialColor(hairMaterial, hairColors[currentHairColorIndex]);
        UpdateMaterialColor(beardMaterial, hairColors[currentBeardColorIndex]);
        UpdateMaterialColor(skinMaterial, skinColors[currentSkinColorIndex]);

        UpdateColorText(hairColorText, hairColors[currentHairColorIndex]);
        UpdateColorText(beardColorText, hairColors[currentBeardColorIndex]);
        UpdateColorText(skinColorText, skinColors[currentSkinColorIndex]);
    }

    private void ChangeColor(ref int colorIndex, ColorOption[] colors, int change, Material material, TMP_Text colorText)
    {
        colorIndex += change;

        if (colorIndex < 0)
        {
            colorIndex = colors.Length - 1;
        }
        else if (colorIndex >= colors.Length)
        {
            colorIndex = 0;
        }

        UpdateMaterialColor(material, colors[colorIndex]);
        UpdateColorText(colorText, colors[colorIndex]);
    }

    private void UpdateMaterialColor(Material material, ColorOption colorOption)
    {
        material.color = colorOption.color;
    }

    private void UpdateColorText(TMP_Text textField, ColorOption colorOption)
    {
        textField.text = $"<color={colorOption.hexCode}>{colorOption.name}</color>";
    }
     
        void HideColorPanel()
    {
        colorPanel.SetActive(false);
    }
}
