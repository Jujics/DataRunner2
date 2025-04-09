using System;
using TMPro;
using UnityEngine;

public class PlayerCollectibleManager : MonoBehaviour
{

    private int coinNumber, speItemNumber;
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private TMP_Text speItemText;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                coinNumber++;
                other.gameObject.SetActive(false);
                break;
            case "SpeItem":
                speItemNumber++;
                other.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
