using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerIdleState 
{
    public abstract void EnterState(PlayerStateManager player, float currentSpeed);

    public abstract void UpdateState(PlayerStateManager player, PlayerInput playerInput);
    
    public abstract void OnCollisionEnter(PlayerStateManager player, Collision collision);
    
    public abstract void OnCollisionExit(PlayerStateManager player, Collision collision);
    
    public abstract void OnTriggerEnter(PlayerStateManager player, Collider other);
    
    public abstract void OnTriggerExit(PlayerStateManager player, Collider other);
}
