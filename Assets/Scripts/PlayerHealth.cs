using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthBar;
    public GameObject deathScreen;
    public Transform spawnPoint;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null) healthBar.value = currentHealth;
        if (deathScreen != null) deathScreen.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null) healthBar.value = currentHealth;

        Debug.Log("Walter HP: " + currentHealth);

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (deathScreen != null) deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        if (healthBar != null) healthBar.value = currentHealth;
        if (deathScreen != null) deathScreen.SetActive(false);

        if (spawnPoint != null)
            transform.position = spawnPoint.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}