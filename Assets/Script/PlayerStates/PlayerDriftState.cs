using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDriftState : PlayerIdleState
{
    public override void EnterState(PlayerStateManager player)
    {
        return;
    }

    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        return;
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {
        return;
    }

    public override void OnCollisionExit(PlayerStateManager player, Collision collision)
    {
        return;
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider other)
    {
        if (other.tag == "Collectible")
        {
            other.gameObject.SetActive(false);
            ComboManager.ComboCount();
            player.scoreAmount += 10 * ComboManager.CurrentCombo;
        }
        if (other.CompareTag("StartQuest"))
        {
            string thisQuestName = other.gameObject.GetComponent<QuestManager>().QuestName;
            other.gameObject.GetComponent<QuestManager>().QuestText.text = thisQuestName;
            player.questCanvas.gameObject.SetActive(true);
            player.currentQuestManager = other.gameObject.GetComponent<QuestManager>();
        }
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider other)
    {
        return;
    }
}
