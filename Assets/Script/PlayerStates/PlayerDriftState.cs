using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDriftState : PlayerIdleState
{
    public override void EnterState(PlayerStateManager player, float currentSpeed)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        throw new System.NotImplementedException();
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
            player.comboManager.ComboCount();
            player.scoreAmount += 10 * player.comboManager._currentCombo;
        }
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
