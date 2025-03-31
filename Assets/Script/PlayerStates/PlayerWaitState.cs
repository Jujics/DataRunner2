using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWaitState : PlayerIdleState
{
    public override void EnterState(PlayerStateManager player, float currentSpeed)
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
            player.SwitchState(new PlayerForwardState(), 0f);
        }
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionExit(PlayerStateManager player, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider other)
    {
        if (other.tag == "Collectible")
        {
            other.gameObject.SetActive(false);
            player.comboManager.ComboCount();
            player.scoreAmount += 10 * player.comboManager._currentCombo;
        }
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
