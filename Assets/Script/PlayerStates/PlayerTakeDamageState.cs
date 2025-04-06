using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTakeDamageState : PlayerIdleState
{
    
    private static float t = 0.0f;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered PlayerTakeDamageState");
        player.scoreAmount -= 20;
    }

    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        if (player.currentSpeed < 10f)
        {
            player.SwitchState(new PlayerForwardState());
        }
        player.currentSpeed = Mathf.Lerp(player.currentSpeed, 9f, 2f * Time.deltaTime);
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
