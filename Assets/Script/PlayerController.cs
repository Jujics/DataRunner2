using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    private float playerSpeed;
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private float boostSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float driftTurnSpeed;

    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction boostAction;
    private InputAction turnAction;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        moveAction = playerInput.actions.FindAction("Forward");
        boostAction = playerInput.actions.FindAction("Boost");
        turnAction = playerInput.actions.FindAction("Turn");
    }

    void FixedUpdate()
    {
        Boost();
        Forward();
        Turn();
    }

    private void Boost()
    {
        float boostInput = boostAction.ReadValue<float>();
        if (boostInput > 0)
        {
            playerSpeed = boostSpeed;
        }
        else if (boostInput <= 0)
        {
            playerSpeed = normalSpeed;
        }
    }

    private void Forward()
    {
        float moveInput = moveAction.ReadValue<float>();
        if (moveInput > 0)
        {
            Vector3 moveDirection = transform.forward * moveInput * playerSpeed;
            rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z); 
        }
    }

    private void Turn()
    {
        if (turnAction.ReadValue<float>() > 0)
        {
            Quaternion turnRotation = Quaternion.Euler(0, -turnSpeed, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        if (turnAction.ReadValue<float>() < 0)
        {
            Quaternion turnRotation = Quaternion.Euler(0, turnSpeed, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}