using UnityEngine;

public class Tatzelwurm : MonoBehaviour
{
    public float detectionRange = 10f;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    private float attackTimer = 0f;
    private Transform player;

    void Start()
    {
        // Automatically find Walter
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            transform.LookAt(new Vector3(player.position.x,
                transform.position.y, player.position.z));

            if (distance > attackRange)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
                {
                    Attack();
                    attackTimer = attackCooldown;
                }
            }
        }
    }

    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}