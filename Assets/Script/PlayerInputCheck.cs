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
    private InputAction driftAction;

    void Start()
    {
        moveAction = playerInput.actions.FindAction("Forward");
        boostAction = playerInput.actions.FindAction("Boost");
        turnAction = playerInput.actions.FindAction("Turn");
        driftAction = playerInput.actions.FindAction("Drift");
    }
    void Update()
    {
        ForwardCheck();
        TurnCheck();
        BoostCheck();
        DriftCheck();
    }

    private void ForwardCheck()
    {
        float moveInput = moveAction.ReadValue<float>();
        switch (moveInput)
        {
            case 1f:
            {
                forward = true;
                break;
            }
            case 0f:
            {
                forward = false;
                break;
            }
        }
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
        switch (boostInput)
        {
            case 1f:
            {
                boost = true;
                break;
            }
            case 0f:
            {
                boost = false;
                break;
            }
        }
    }

    private void DriftCheck()
    {
        float driftInput = driftAction.ReadValue<float>();
        switch (driftInput)
        {
            case 1f:
            {
                drift = true;
                if (left == true && right == false)
                {
                    leftDrift = true;
                }
                else if (left == false && right == true)
                {
                    rightDrift = true;
                }
                else
                {
                    leftDrift = false;
                    rightDrift = false;
                }
                break;
            }
            case 0f:
            {
                drift = false;
                leftDrift = false;
                rightDrift = false;
                break;
            }
        }

        
    }
}
