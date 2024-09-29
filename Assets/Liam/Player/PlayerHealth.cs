using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Health variables.
    private float maxHealth = 3f;
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
        // Exit the function if the game is over as the player cannot take any more damage.
        if (GameManager.Instance.isGameOver)
        {
            return;
        }

        PlayerHasTakenDamage = true;

        // Set animation trigger to play damage animation.
        animator.SetTrigger("damaged");

        currentHealth -= damageTaken;

        // If the player's current health hits or goes below 0, they dead.
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }

        StartCoroutine(DamageCooldown());
    }

    // Death...
    private IEnumerator Death()
    {
        // Transition to the death animation, then wait 5 seconds for the animation to finish before destroying the player.
        transform.localScale = new Vector3(-.5f, .5f, 1f);
        animator.SetTrigger("death");
        InputManager.playerInput.SwitchCurrentActionMap("UI");
        GameManager.Instance.isGameOver = true;
        yield return new WaitForSeconds(5.0f);
        GameManager.Instance.GameOver(false);
    }

    // Cooldown before the player can take damage again.
    // This allows for the damaeg animation to complete before occurring again.
    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageAnimationTime);
        PlayerHasTakenDamage = false;
    }
}
