using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    private float playerSpeed;
    [SerializeField]
    private float normalSpeed = 5f;
    [SerializeField]
    private float boostSpeed = 10f;
    [SerializeField]
    private float accelerationTime = 1f; 

    [Header("Player Turn")]
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float driftTurnSpeed;
    [SerializeField]
    private PlayerInputCheck playerInput;

    private bool isBoosting;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSpeed = normalSpeed;
    }

    void FixedUpdate()
    {
        Boost();
        Forward();
        Turn();
    }

    private void Boost()
    {
        if (playerInput.boost && !isBoosting)
        {
           
            StopAllCoroutines(); 
            StartCoroutine(AccelerateToSpeed(boostSpeed));
            isBoosting = true;
        }
        else if (!playerInput.boost && isBoosting)
        {
            StopAllCoroutines(); 
            StartCoroutine(AccelerateToSpeed(normalSpeed));
            isBoosting = false;
        }
    }

    private void Forward()
    {
        if (playerInput.forward)
        {
            Vector3 moveDirection = transform.forward * playerSpeed;
            rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        }
    }

    private void Turn()
    {
        if (playerInput.left && !playerInput.right)
        {
            Quaternion turnRotation = Quaternion.Euler(0, -turnSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        if (playerInput.right && !playerInput.left)
        {
            Quaternion turnRotation = Quaternion.Euler(0, turnSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }

    private IEnumerator AccelerateToSpeed(float targetSpeed)
    {
        float startSpeed = playerSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < accelerationTime)
        {
            playerSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / accelerationTime);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        playerSpeed = targetSpeed;
    }
}