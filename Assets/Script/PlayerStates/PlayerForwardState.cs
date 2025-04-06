using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerForwardState : PlayerIdleState
{
    private float maxSpeed = 20f;
    private Quaternion lastFrameRotation;
    static float t = 0.0f;
    
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered PlayerForwardState");
        lastFrameRotation = player.transform.rotation;
    }

    
    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
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
            player.currentSpeed = Mathf.Lerp(player.currentSpeed, 0f, 5f * Time.deltaTime);
            player.GetComponent<Rigidbody>().linearVelocity = player.transform.forward * player.currentSpeed;
        }

        if (playerInput.Player.Boost.ReadValue<float>() > 0.1f && player.boostAmount > 0)
        {
            player.SwitchState(new PlayerBoostState());
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
        else
        {
            if (player.GetComponent<Transform>().rotation != lastFrameRotation);
            {
                player.GetComponent<Transform>().rotation = lastFrameRotation;
            }
        }
        lastFrameRotation = player.GetComponent<Transform>().rotation;
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
        if (other.tag == "Damage")
        {
            player.SwitchState(new PlayerTakeDamageState());
        }

        if (other.tag == "BoostPylone")
        {
            player.SwitchState(new PlayerBoostPadState());
            player.boostAmount += 10;
        }
        if (other.tag == "BoostPad")
        {
            player.SwitchState(new PlayerBoostPadState());
        }
        if (other.tag == "Collectible")
        {
            other.gameObject.SetActive(false);
            player.comboManager.ComboCount();
            player.scoreAmount += 10 * player.comboManager.CurrentCombo;
        }
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider other)
    {
        return;
    }
}
