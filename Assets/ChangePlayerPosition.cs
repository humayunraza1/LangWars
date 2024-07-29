using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerPosition : MonoBehaviour
{

    [SerializeField] private Button button;
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ChangePosition);
    }

    void ChangePosition(){
        Player.transform.position = new Vector3(14.32f,8.62f,5f);
    }
}
