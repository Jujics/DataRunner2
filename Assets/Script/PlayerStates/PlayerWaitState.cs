using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWaitState : PlayerIdleState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered PlayerWaitState");
    }

    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        Debug.Log("Player Wait State");
        if (playerInput.Player.Forward.ReadValue<float>() < 0.9f)
        {
            return;
        }
        else
        {
            player.SwitchState(new PlayerForwardState());
        }
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
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider other)
    {
        return;
    }
}
