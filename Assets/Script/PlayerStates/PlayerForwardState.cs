using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerForwardState : PlayerIdleState
{
    private float maxSpeed = 20f;
    private float currentSpeed;
    static float t = 0.0f;
    
    public override void EnterState(PlayerStateManager player, float _currentSpeed)
    {
        Debug.Log("Entered PlayerForwardState");
        currentSpeed = _currentSpeed;
    }

    
    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        float forwardInput = playerInput.Player.Forward.ReadValue<float>();

        if (forwardInput > 0.1f)  
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, 5f * Time.deltaTime);
            Vector3 forwardMomentum = player.transform.forward * forwardInput;
            player.GetComponent<Rigidbody>().linearVelocity = forwardMomentum * currentSpeed;
        }
        else 
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, 5f * Time.deltaTime);
            player.GetComponent<Rigidbody>().linearVelocity = player.transform.forward * currentSpeed;
        }

        if (playerInput.Player.Boost.ReadValue<float>() > 0.1f && player.boostAmount > 0)
        {
            player.SwitchState(new PlayerBoostState(), currentSpeed);
        }
        
        Turn(player, playerInput);
        
    }
    
    public void Turn(PlayerStateManager player , PlayerInput playerInput)
    {
        float turnInput = playerInput.Player.Turn.ReadValue<float>();
        if (turnInput > 0.1f)
        {
            player.GetComponent<Transform>().Rotate(0, -1, 0);
        }
        else if (turnInput < -0.1f)
        {
            player.GetComponent<Transform>().Rotate(0, 1, 0);
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
        if (other.tag == "Damage")
        {
            player.SwitchState(new PlayerTakeDamageState(), currentSpeed);
        }

        if (other.tag == "Boost")
        {
            player.SwitchState(new PlayerBoostPadState(), currentSpeed);
        }
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
