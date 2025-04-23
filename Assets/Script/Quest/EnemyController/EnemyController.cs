using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyState currentState;
    
    private Transform target;
    public Transform Target { set => target = value; }
    
    void Start()
    {
        currentState = EnemyState.Waiting;
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Waiting:
                break;
            case EnemyState.GettingCloser:
                break;
            case EnemyState.LongAttack:
                break;
            case EnemyState.ShortAttack:
                break;
            case EnemyState.Dead:
                break;
        }
    }

    private void StartState()
    {
        switch (currentState)
        {
            case EnemyState.Waiting:
                break;
            case EnemyState.GettingCloser:
                break;
            case EnemyState.LongAttack:
                break;
            case EnemyState.ShortAttack:
                break;
            case EnemyState.Dead:
                break;
        }
    }

    public void SwitchState(EnemyState newState)
    {
        currentState = newState;
        
    }    

    public enum EnemyState
    {
        Waiting,
        LongAttack,
        ShortAttack,
        GettingCloser,
        Dead
    }
}
