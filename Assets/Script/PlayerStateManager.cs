using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    PlayerIdleState currentState;
    public PlayerForwardState forwardState = new PlayerForwardState();
    public PlayerBoostState boostState = new PlayerBoostState();
    public PlayerBoostPadState boostPadState = new PlayerBoostPadState();
    public PlayerDriftState driftState = new PlayerDriftState();
    public PlayerTakeDamageState takeDamageState = new PlayerTakeDamageState();
    public PlayerWaitState waitState = new PlayerWaitState();
    public int boostAmount = 100;
    public int scoreAmount = 10;
    public ComboManager comboManager;
    
    [SerializeField] private PlayerInput actionAsset;

    private void Awake()
    {
        actionAsset = new PlayerInput();
    }

    private void OnEnable()
    {
        actionAsset.Enable();
    }
    
    private void OnDisable()
    {
        actionAsset.Disable();
    }
    
    void Start()
    {
        currentState = waitState;
        currentState.EnterState(this, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    private void OnCollisionEnter(Collision other)
    {
        currentState.OnCollisionEnter(this ,other);
    }

    private void OnCollisionExit(Collision other)
    {
        currentState.OnCollisionExit(this, other);
    }

    void FixedUpdate()
    {
        currentState.UpdateState(this, actionAsset);
    }

    public void SwitchState(PlayerIdleState newState, float currentSpeed)
    {
        currentState = newState;
        newState.EnterState(this, currentSpeed);
    }
}
