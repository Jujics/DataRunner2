using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float boostSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float driftTurnSpeed;
    private Rigidbody rb;
    
    [Header("Movement")]
    private PlayerInput playerInput;
    private InputActionMap forwardAction;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        forwardAction = playerInput.actions.FindActionMap("forward");
    }

    void FixedUpdate()
    {
        Vector2 forward = forwardAction.ReadValue<Vector2>();
    }
}
