using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    private float playerSpeed;
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private float boostSpeed;
    [Header("Player Turn")]
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float driftTurnSpeed;
    [SerializeField]
    private PlayerInputCheck playerInput;

    
    
    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Boost();
        Forward();
        Turn();
    }

    private void Boost()
    {
        if (playerInput.boost == true)
        {
            playerSpeed = boostSpeed;
        }
        else if (playerInput.boost == false)
        {
            playerSpeed = normalSpeed;
        }
    }

    private void Forward()
    {
        if (playerInput.forward == true)
        {
            Vector3 moveDirection = transform.forward * 1f * playerSpeed;
            rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z); 
        }
    }

    private void Turn()
    {
        if (playerInput.leftDrift == true || playerInput.rightDrift == true)
        {
            if (playerInput.left == true && playerInput.right == false)
            {
                Quaternion turnRotation = Quaternion.Euler(0, -driftTurnSpeed, 0);
                rb.MoveRotation(rb.rotation * turnRotation);
            }

            if (playerInput.right == true && playerInput.left == false)
            {
                Quaternion turnRotation = Quaternion.Euler(0, driftTurnSpeed, 0);
                rb.MoveRotation(rb.rotation * turnRotation);
            }
        }
        else
        {
            if (playerInput.left == true && playerInput.right == false)
            {
                Quaternion turnRotation = Quaternion.Euler(0, -turnSpeed, 0);
                rb.MoveRotation(rb.rotation * turnRotation);
            }

            if (playerInput.right == true && playerInput.left == false)
            {
                Quaternion turnRotation = Quaternion.Euler(0, turnSpeed, 0);
                rb.MoveRotation(rb.rotation * turnRotation);
            }
        }
    }
}