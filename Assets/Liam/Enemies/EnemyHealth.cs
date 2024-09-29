using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    // Health
    private float maxHealth = 3f;
    private float currentHealth;

    public bool HasTakenDamage { get; set; }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        HasTakenDamage = true;

        //Debug.Log("Enemy Damaged");
        // Decrease the current health by the damage amount given when the function is called.
        currentHealth -= damageAmount;

        // If the current health hits or goes below 0, the enemy dies.
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // Enemy death.
    private void Death()
    {
        Destroy(gameObject);
    }
}
