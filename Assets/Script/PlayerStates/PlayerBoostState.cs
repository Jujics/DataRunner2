using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBoostState : PlayerIdleState
{
    private float maxSpeed = 50f;
    private float currentSpeed;
    private Quaternion lastFrameRotation;
    static float t = 0.0f;
    
    public override void EnterState(PlayerStateManager player, float _currentSpeed)
    {
        Debug.Log("Entered PlayerBoostState");
        currentSpeed = _currentSpeed;
        lastFrameRotation = player.transform.rotation;
    }

    
    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        float forwardInput = playerInput.Player.Forward.ReadValue<float>();
        if (forwardInput > 0.1f || player.boostAmount > 0)  
        {
            Vector3 forwardMomentum = player.transform.forward * forwardInput;
            player.GetComponent<Rigidbody>().linearVelocity = forwardMomentum * currentSpeed;
            if (currentSpeed < maxSpeed)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, 5f * Time.deltaTime); 
            }
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, 5f * Time.deltaTime);
        }

        if (playerInput.Player.Boost.ReadValue<float>() == 0f || player.boostAmount <= 0)
        {
            player.SwitchState(new PlayerForwardState(), currentSpeed);
        }
        player.boostAmount--;
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
            player.SwitchState(new PlayerTakeDamageState(), currentSpeed);
        }

        if (other.tag == "Boost")
        {
            player.SwitchState(new PlayerBoostPadState(), currentSpeed);
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
