using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerForwardState : PlayerIdleState
{
    private float turnSpeed = 30f;       
    private float maxSpeed = 50f;
    private Quaternion lastFrameRotation;
    static float t = 0.0f;
    
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered PlayerForwardState");
        lastFrameRotation = player.transform.rotation;
    }

    
    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        if (player.canMove == true)
        {
            float forwardInput = playerInput.Player.Forward.ReadValue<float>();
            
            if (forwardInput > 0.1f)  
            {
                player.currentSpeed = Mathf.Lerp(player.currentSpeed, maxSpeed, 5f * Time.deltaTime);
                Vector3 forwardMomentum = player.transform.forward * forwardInput;
                player.GetComponent<Rigidbody>().linearVelocity = forwardMomentum * player.currentSpeed;
            }
            else 
            {
                player.currentSpeed = Mathf.Lerp(player.currentSpeed, 0f, 0.5f * Time.deltaTime);
                player.GetComponent<Rigidbody>().linearVelocity = player.transform.forward * player.currentSpeed;
            }
    
            if (playerInput.Player.Boost.ReadValue<float>() > 0.1f && player.boostAmount > 0)
            {
                player.SwitchState(new PlayerBoostState());
            }
            
            Turn(player, playerInput);
        }
    }
    
    private void Turn(PlayerStateManager player, PlayerInput playerInput)
    {
        float turnInput = playerInput.Player.Turn.ReadValue<float>();
    
        if (Mathf.Abs(turnInput) > 0.1f) 
        {
            player.currentTurnSpeed = (turnInput > 0) ? -turnSpeed : turnSpeed;
            player.transform.Rotate(0, player.currentTurnSpeed * Time.deltaTime, 0);
        }
        else 
        {
            player.currentTurnSpeed = 0f;
        }
    
        if (Mathf.Abs(player.currentTurnSpeed) > 0.01f)
        {
            player.transform.Rotate(0, player.currentTurnSpeed * Time.deltaTime, 0);
            lastFrameRotation = player.transform.rotation;
        }
        else
        {
            player.transform.rotation = lastFrameRotation;
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
        if (other.CompareTag("Damage"))
        {
            player.SwitchState(new PlayerTakeDamageState());
        }

        if (other.CompareTag("BoostPylone"))
        {
            player.SwitchState(new PlayerBoostPadState());
            player.boostAmount += 10;
        }
        if (other.CompareTag("BoostPad"))
        {
            player.SwitchState(new PlayerBoostPadState());
        }
        if (other.CompareTag("Collectible"))
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
