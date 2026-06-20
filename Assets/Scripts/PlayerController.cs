using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackDamage = 25f;
    public float attackRange = 2f;

    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Attack on left click
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            if (animator != null) animator.SetTrigger("Attack");
            PerformAttack();
            Invoke("ResetAttack", 0.5f);
        }

        if (isAttacking)
        {
            if (animator != null) animator.SetBool("isWalking", false);
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Transform cam = Camera.main.transform;
        Vector3 camForward = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;
        Vector3 camRight = new Vector3(cam.right.x, 0, cam.right.z).normalized;
        Vector3 moveDirection = (camForward * v + camRight * h).normalized;

        if (animator != null) animator.SetBool("isWalking", moveDirection.magnitude > 0);

        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void PerformAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(
            transform.position + transform.forward * 1f,
            attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage, transform.position);
                }
            }
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1f, attackRange);
    }
}