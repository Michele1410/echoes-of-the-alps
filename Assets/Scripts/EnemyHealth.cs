using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    private float currentHealth;
    public Slider healthBar;
    public GameObject enemyCanvas;
    public float showRange = 10f;

    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;

    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        if (enemyCanvas != null) enemyCanvas.SetActive(false);
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null || enemyCanvas == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        enemyCanvas.SetActive(distance < showRange);

        enemyCanvas.transform.LookAt(Camera.main.transform);
        enemyCanvas.transform.Rotate(0, 180f, 0);
    }

    public void TakeDamage(float damage, Vector3 attackerPosition)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null) healthBar.value = currentHealth;

        // Knockback
        if (rb != null && !isKnockedBack)
        {
            Vector3 knockbackDir = (transform.position - attackerPosition).normalized;
            knockbackDir.y = 0;
            rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
            Invoke("ResetKnockback", knockbackDuration);
            isKnockedBack = true;
        }

        if (currentHealth <= 0) Die();
    }

    void ResetKnockback()
    {
        isKnockedBack = false;
        if (rb != null) rb.velocity = Vector3.zero;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}