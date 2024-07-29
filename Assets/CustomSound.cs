using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSound : MonoBehaviour
{
    // Start is called before the first frame update
    
    private AudioSource audioSource;
    private Button button;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();

         if (button != null && audioSource != null)
        {
            button.onClick.AddListener(PlaySound);
        }
        else
        {
            Debug.LogError("Button or AudioSource component is missing.");
        }

    }

 void PlaySound()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource does not have an audio clip assigned.");
        }
    }

}
