using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerForwardState : PlayerIdleState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered PlayerForwardState");
    }

    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        Debug.Log("update PlayerForwardState");
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
