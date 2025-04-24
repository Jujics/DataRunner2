using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyState currentState;
    [SerializeField] private float gunAttackChance;
    [SerializeField] private float chargeRange;
    [SerializeField] private float dashSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    private bool inAttack;
    private Transform target;
    public Transform Target { set => target = value; }

    private Transform health;
    public Transform Health { set => health = value; }
    
    private NavMeshAgent agent; 
    private NavMeshPath navMeshPath;
    
    
    void Start()
    {
        currentState = EnemyState.Waiting;
        agent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Waiting:
                break;
            case EnemyState.GettingCloser:
                if (CanReachArea() == true)
                {
                    agent.SetDestination(target.position);
                }
                
                if (Vector3.Distance(transform.position, target.position) <= chargeRange)
                {
                    SwitchState(EnemyState.ShortAttack);
                }
                
                else if (Vector3.Distance(transform.position, target.position) > chargeRange &&
                         gunAttackChance > Random.value)
                {
                    SwitchState(EnemyState.LongAttack);
                }
                
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
                StartCoroutine(LongAttack());
                break;
            case EnemyState.ShortAttack:
                StartCoroutine(ShortAttack());
                break;
            case EnemyState.Dead:
                break;
        }
    }
    public bool CanReachArea() {
        return agent.CalculatePath(target.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;
    }

    private IEnumerator ShortAttack()
    {
        Vector3 direction = target.transform.position ;
        while (direction != transform.position)
        {
            transform.position += direction.normalized * dashSpeed * Time.deltaTime;
        }
        yield return new WaitForSeconds(0.2f);
        SwitchState(EnemyState.GettingCloser);
    }

    private IEnumerator LongAttack()
    {
        Vector3 direction = target.transform.position ;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().linearVelocity = direction.normalized * bulletSpeed;
        yield return new WaitForSeconds(0.2f);
        SwitchState(EnemyState.GettingCloser);
        yield return new WaitForSeconds(8f);
        Destroy(bullet);
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
