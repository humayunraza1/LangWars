using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    private Button button;
    public int soundClipIndex = 0;

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(PlaySound);
        }
        else
        {
            Debug.LogError("Button component is missing.");
        }
    }

    void PlaySound()
    {
        if (SoundController.Instance != null)
        {
            SoundController.Instance.PlaySound(soundClipIndex);
        }
        else
        {
            Debug.LogWarning("SoundController instance is missing." + gameObject.name);
        }
    }
}
