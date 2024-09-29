using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Health variables.
    private float maxHealth = 30f;
    private float currentHealth;

    // Bool that prevents the player from taking damage again until after their damage animation has finished.
    public bool PlayerHasTakenDamage { get; set; }

    private Animator animator;

    // Time it takes for the player's damage animation to finish.
    private float damageAnimationTime = 1.0f;

    private void Start()
    {
        // Set component ref.
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageTaken)
    {
        PlayerHasTakenDamage = true;

        // Set animation trigger to play damage animation.
        animator.SetTrigger("damaged");

        currentHealth -= damageTaken;

        // If the player's current health hits or goes below 0, they dead.
        if (currentHealth <= 0)
        {
            Death();
        }

        StartCoroutine(DamageCooldown());
    }

    // Death...
    private void Death()
    {
        Destroy(gameObject);
    }

    // Cooldown before the player can take damage again.
    // This allows for the damaeg animation to complete before occurring again.
    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageAnimationTime);
        PlayerHasTakenDamage = false;
    }
}
