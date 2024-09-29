using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float damageDealt = 1f;

    // Check what has collided with the enemy.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player was it, get reference to their health script and deal damage.
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (!playerHealth.PlayerHasTakenDamage)
            {
                playerHealth.TakeDamage(damageDealt);
            }
        }
    }
}
