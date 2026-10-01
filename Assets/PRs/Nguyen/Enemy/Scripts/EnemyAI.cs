using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Target & Ranges")]
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;

    [Header("Attack Settings")]
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        // Auto-find player if not assigned in Inspector
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 1. Update Animator "Speed" parameter for walk/idle blend
        if (animator != null)
        {
            // Uses actual velocity magnitude from NavMeshAgent
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        // 2. Detection & Chase Logic
        if (distanceToPlayer <= detectionRange)
        {
            // Within Attack Range
            if (distanceToPlayer <= attackRange)
            {
                agent.isStopped = true;
                RotateTowardsPlayer();

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    PerformAttack();
                    lastAttackTime = Time.time;
                }
            }
            // Chase Player
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }
        else
        {
            // Stop if player gets out of range
            agent.isStopped = true;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep rotation flat on the floor
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

    void PerformAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    // Visualize detection and attack ranges in Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}