using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBoostPadState : PlayerIdleState
{
    
    private float maxSpeed = 70f;
    private static float t = 3f;
    private Quaternion lastFrameRotation;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered PlayerBoostPadState");
        lastFrameRotation = player.transform.rotation;
    }

    public override void UpdateState(PlayerStateManager player, PlayerInput playerInput)
    {
        float forwardInput = playerInput.Player.Forward.ReadValue<float>();
        if (player.currentSpeed < maxSpeed - 5f)
        {
            Vector3 forwardMomentum = player.transform.forward * forwardInput;
            player.GetComponent<Rigidbody>().linearVelocity = forwardMomentum * player.currentSpeed;
            if (player.currentSpeed < maxSpeed)
            {
                player.currentSpeed = Mathf.Lerp(player.currentSpeed, maxSpeed, t * Time.deltaTime); 
            }
        }
        else
        {
            player.SwitchState(new PlayerForwardState());
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
