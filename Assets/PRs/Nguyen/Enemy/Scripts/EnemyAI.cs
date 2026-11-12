using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Target & Ranges")] 
    public PlayerStats playerStats;
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;

    [Header("Attack Settings")]
    public float attackCooldown = 1.5f;
    private float lastAttackTime;
    public int damage = 1; 
    
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        // Tự động tìm Player nếu chưa gán trong Inspector
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // Tự động tìm PlayerStats nếu chưa gán
        if (playerStats == null && player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 1. Cập nhật thông số Speed cho Animator chạy animation di chuyển
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        // 2. Logic phát hiện và đuổi theo Player
        if (distanceToPlayer <= detectionRange)
        {
            // Trong phạm vi tấn công
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
            // Đuổi theo Player
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Giữ hướng xoay phẳng trên mặt đất
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

    // Hàm trừ máu
    public void TriggerAttackDamage()
    {
        if (playerStats != null && player != null)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange + 0.5f)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }

    // Hiển thị vòng tròn phạm vi trong cửa sổ Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}