using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public Canvas homeCanvas; // Assign the home canvas in the Unity Editor
    public Canvas customizeCanvas; // Assign the customization canvas in the Unity Editor
    public Button backButton; // Assign the back button in the Unity Editor
     [SerializeField] private GameObject armorManagerObject;
     
      private ArmorManager armorManager;
      void Start()
    {
        // Initialize UI button listener
        backButton.onClick.AddListener(OnBackButtonClicked);

        // Ensure only one canvas is visible at the start
        ShowHomeCanvas();

        if (armorManagerObject != null)
        {
            armorManager = armorManagerObject.GetComponent<ArmorManager>();
            if (armorManager == null)
            {
                Debug.LogError("ArmorManager component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with the specified name not found.");
        }

    }

    void OnBackButtonClicked()
    {
        // Show the home canvas and hide the customization canvas
    if (armorManager != null)
        {
            armorManager.BackOut();
        }
        ShowHomeCanvas();
    }

    void ShowHomeCanvas()
    {
        homeCanvas.gameObject.SetActive(true);
        customizeCanvas.gameObject.SetActive(false);
    }

}
