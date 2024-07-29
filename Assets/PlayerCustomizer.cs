using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomizer : MonoBehaviour
{
    public GameObject defaultPlayerMesh;
    public GameObject armorMesh;
    public RuntimeAnimatorController animatorControllerToAttach; // Assign the Animator Controller in the Unity Editor

    public Button nextButton;

    private Animator armorAnimator;

    void Start()
    {
        // Initialize UI button listener
        nextButton.onClick.AddListener(NextButtonClicked);

        // Get the Animator component from the armor mesh
        armorAnimator = armorMesh.GetComponent<Animator>();

        // Show default player mesh initially
        ShowDefaultPlayer();
    }

    void ShowDefaultPlayer()
    {
        defaultPlayerMesh.SetActive(true);
        armorMesh.SetActive(false);
    }

    void ShowArmor()
    {
        defaultPlayerMesh.SetActive(false);
        armorMesh.SetActive(true);

        // Attach selected animator controller to the armor mesh if not already attached
        if (armorAnimator == null && animatorControllerToAttach != null)
        {
            armorAnimator = armorMesh.AddComponent<Animator>();
            armorAnimator.runtimeAnimatorController = animatorControllerToAttach;
        }
    }

    void NextButtonClicked()
    {
        // Toggle between default player and armor mesh
        if (defaultPlayerMesh.activeSelf)
        {
            ShowArmor();
        }
        else
        {
            ShowDefaultPlayer();
        }
    }
}
