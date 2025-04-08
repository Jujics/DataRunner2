using UnityEngine;

public class EndLineCollider : MonoBehaviour
{
    [SerializeField]
    private QuestManager questManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && questManager.IsRunning)
        {
            switch (questManager._questType)
            {
                case QuestManager.QuestType.BusRoute:
                    questManager.BusPlayerArrived();
                    break;
                case QuestManager.QuestType.OtherStuff:
                    break;
                default:
                    break;
            }
        }
    }
}
