using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCheck : MonoBehaviour
{
    
    public bool forward;
    public bool left;
    public bool right;
    public bool boost;
    public bool drift;
    public bool leftDrift;
    public bool rightDrift;
    
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction boostAction;
    private InputAction turnAction;

    void Start()
    {
        moveAction = playerInput.actions.FindAction("Forward");
        boostAction = playerInput.actions.FindAction("Boost");
        turnAction = playerInput.actions.FindAction("Turn");
    }
    void Update()
    {
        ForwardCheck();
        TurnCheck();
        BoostCheck();
    }

    private void ForwardCheck()
    {
        float moveInput = moveAction.ReadValue<float>();
        forward = moveInput switch
        {
            1f => true,
            0f => false,
            _ => forward
        };
    }

    private void TurnCheck()
    {
        float turnInput = turnAction.ReadValue<float>();
        switch (turnInput)
        {
            case 1f:
            {
                left = true;
                right = false;
                break;
            }
            case 0f:
            {
                left = false;
                right = false;
                break;
            }
            case -1f:
            {
                left = false;
                right = true;
                break;
            }
        }
    }

    private void BoostCheck()
    {
        float boostInput = boostAction.ReadValue<float>();
        boost = boostInput switch
        {
            1f => true,
            0f => false,
            _ => boost
        };
    }

    
}
