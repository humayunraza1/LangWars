using UnityEngine;
using UnityEngine.UI;

public class CameraCullingMaskChanger : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera in the Unity Editor
    public LayerMask defaultCullingMask; // Assign the default culling mask in the Unity Editor
    public LayerMask newCullingMask; // Assign the new culling mask in the Unity Editor
    public Canvas customizationCanvas; // Assign the customization canvas in the Unity Editor

    public Button nextButton;
    public Button prevButton;

    private bool isDefaultCullingMask = true;

    void Start()
    {
        Debug.Log("This script is attached to the GameObject: " + gameObject.name);
        // Initialize UI button listeners
        nextButton.onClick.AddListener(OnNextButtonClicked);
        prevButton.onClick.AddListener(OnPrevButtonClicked);

        // Set the camera to the default culling mask initially
        mainCamera.cullingMask = defaultCullingMask;
    }

    void OnNextButtonClicked()
    {
        // Change to the new culling mask only if the customization canvas is active
        if (customizationCanvas.gameObject.activeSelf && isDefaultCullingMask)
        {
            mainCamera.cullingMask = newCullingMask;
            isDefaultCullingMask = false;
        }
    }

    void OnPrevButtonClicked()
    {
        // Revert to the default culling mask only if the customization canvas is active
        if (customizationCanvas.gameObject.activeSelf && !isDefaultCullingMask)
        {
            mainCamera.cullingMask = defaultCullingMask;
            isDefaultCullingMask = true;
        }
    }
}
