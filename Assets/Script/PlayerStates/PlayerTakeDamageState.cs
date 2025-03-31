using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTakeDamageState : PlayerIdleState
{
    float currentSpeed;
    private static float t = 0.0f;
    public override void EnterState(PlayerStateManager player,float _currentSpeed)
    {
        Debug.Log("Entered PlayerTakeDamageState");
        player.scoreAmount -= 20;
        currentSpeed = _currentSpeed;
    }

    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        if (currentSpeed < 10f)
        {
            player.SwitchState(new PlayerForwardState(), currentSpeed);
        }
        currentSpeed = Mathf.Lerp(currentSpeed, 9f, 2f * Time.deltaTime);
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
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
