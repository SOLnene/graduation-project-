using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float turnSpeed = 5f;

    float nextAttackAllowed;

    public bool isAttack;
    public bool isMoving;
    Rigidbody rig;
    EnemyStats emenyStats;
    [SerializeField]
    Transform attackPosition;
    [SerializeField]
    Transform target;
    NavMeshAgent agent;

    [SerializeField]
    Projectile emenyProjectile;

    public System.Action OnAttack;
    void Start()
    {
        isMoving = false;
        isAttack = false;
        rig = GetComponent<Rigidbody>();
        target = GameManager.Instance.LocalPlayer.transform;
        agent = GetComponent<NavMeshAgent>();
        emenyStats = GetComponent<EnemyStats>();
        attackPosition = transform.Find("AttackPosition");
        agent.speed = emenyStats.speed.GetEnemyValue();
        emenyProjectile = emenyStats.projectile;
        emenyProjectile.damage = emenyStats.damage.GetEnemyValue();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                Attack();
                
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
          
    }

    void Attack()
    {
        if (Time.time < nextAttackAllowed)
        {
            return;
        }
        nextAttackAllowed = Time.time + emenyStats.attackInterval;
        Instantiate(emenyProjectile, attackPosition.position, attackPosition.rotation);
        if(OnAttack != null)
        {
            OnAttack();
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public float getSpeed()
    {
        return (agent.velocity.magnitude);
    }
}
