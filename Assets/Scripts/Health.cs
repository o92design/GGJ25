using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public DefenseController defenseController; // Reference to the DefenseController

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (defenseController != null && defenseController.IsDefending)
        {
            Debug.Log(gameObject.name + " is defending. Damage reduced.");
            damage = 0; // Reduce or nullify damage if defending
        }

        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");
        // Add death animation or effects here
        Destroy(gameObject);
    }
}