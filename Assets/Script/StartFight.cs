using System;
using UnityEngine;

public class StartFight : MonoBehaviour
{
    [SerializeField]
    private FirstQuestManager firstQuestManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstQuestManager.StartFight();
        }
    }
}
