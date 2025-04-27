using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyState currentState;
    [SerializeField] private float gunAttackChance;
    [SerializeField] private float chargeRange;
    [SerializeField] private float dashSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;


    private bool canShortAttack = true;
    private bool inShortAttack = false;
    private bool canLongAttack = true;
    private bool inAttack;
    private Transform target;
    public Transform Target { set => target = value; }
    
    private Transform health;
    public Transform Health { set => health = value; }
    
    private NavMeshAgent agent; 
    private NavMeshPath navMeshPath;
    
    [NonSerialized]
    public FightManager fightManager;
    
    void Awake()
    {
        navMeshPath = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
    }

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
                agent.SetDestination(target.position);
                
                if (Vector3.Distance(transform.position, target.position) <= chargeRange && canShortAttack)
                {
                    SwitchState(EnemyState.ShortAttack);
                }
                
                else if (Vector3.Distance(transform.position, target.position) > chargeRange &&
                         gunAttackChance > Random.value && canLongAttack)
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
    

    private IEnumerator ShortAttack()
    {
        inShortAttack = true;
        canShortAttack = false;
        agent.isStopped = true;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        float duration = distance / dashSpeed;
        float elapsed = 0f;
    
        while (elapsed < duration)
        {
            transform.position += direction * dashSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        inShortAttack = false;
        yield return new WaitForSeconds(0.2f);
        agent.isStopped = false;
        SwitchState(EnemyState.GettingCloser);
        yield return new WaitForSeconds(6f);
        canShortAttack = true;
    }

    private IEnumerator LongAttack()
    {
        canLongAttack = false;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.forward = direction;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = direction * bulletSpeed;
        yield return new WaitForSeconds(0.2f);
        SwitchState(EnemyState.GettingCloser);
        Destroy(bullet, 8f);
        yield return new WaitForSeconds(6f);
        canLongAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && inShortAttack && fightManager != null)
        {
            fightManager.PlayerLooseHealth(1);
        }

        if (other.CompareTag("Bullet"))
        {
            fightManager.EnemyLooseHealth(1);
        }
    }

    public void SwitchState(EnemyState newState)
    {
        currentState = newState;
        StartState();
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
