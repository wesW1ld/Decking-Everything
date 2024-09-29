using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Singleton ref.
    public static PlayerHealth instance;

    // Health variables.
    private float maxHealth = 5f;
    public float currentHealth { get; private set; }
    private bool healthSet = false;

    // Bool that prevents the player from taking damage again until after their damage animation has finished.
    public bool PlayerHasTakenDamage { get; set; }

    private Animator animator;

    // Time it takes for the player's damage animation to finish.
    private float damageAnimationTime = 1.0f;

    // Audio sources.
    [SerializeField] private AudioSource playerDamaged;

    private void Awake()
    {
        // Singleton ref.
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Set component ref.
        animator = GetComponent<Animator>();

        // Only set the player's health to the max health once.
        if (!healthSet)
        {
            currentHealth = maxHealth;
            healthSet = true;
        }
    }

    public void TakeDamage(float damageTaken)
    {
        // Exit the function if the game is over as the player cannot take any more damage.
        if (GameManager.Instance.isGameOver || PlayerHasTakenDamage)
        {
            return;
        }

        PlayerHasTakenDamage = true;

        // Set animation trigger to play damage animation and play a sound.
        animator.SetTrigger("damaged");
        playerDamaged.Play();

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
