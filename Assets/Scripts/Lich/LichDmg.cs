using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichDmg : MonoBehaviour, IDamageable
{
    //health
    private float maxHealth = 3f;
    private float currentHealth;

    LichAttacks lichAttacks;
    private GameObject player;

    //pushback
    public float force = 15f;
    public float pushDuration = 0.2f;

    public bool HasTakenDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        lichAttacks = GetComponent<LichAttacks>();
        player = GameObject.FindWithTag("Player");
    }

    public void Damage(float damageAmt)
    {
        HasTakenDamage = true;

        currentHealth -= damageAmt;
        Debug.Log("Lich hit");

        lichAttacks.attackNum += 1;

        if (currentHealth <= 0)
        {
            player.GetComponent<Movement>().enabled = true; //fixes pushback bug if boss dies

            if(lichAttacks.ice1 != null)
            {
                Destroy(lichAttacks.ice1);
            }
            if(lichAttacks.ice2 != null)
            {
                Destroy(lichAttacks.ice2);
            }
            if(lichAttacks.ice3 != null)
            {
                Destroy(lichAttacks.ice3);
            }

            Death();
        }
        else
        {
            StartCoroutine(ApplyPushback(player.GetComponent<Rigidbody2D>(), player.GetComponent<Movement>()));
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private IEnumerator ApplyPushback(Rigidbody2D rb, Movement playerMovement)
    {
        // Disable the player's movement script
        playerMovement.enabled = false;

        // Apply the pushback force
        if (rb.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-force, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(force, rb.velocity.y);
        }

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.StartCoroutine(playerHealth.makeInvincible(pushDuration));


        // Wait for the pushback duration
        yield return new WaitForSeconds(pushDuration);

        // Re-enable the player's movement script
        playerMovement.enabled = true;
    }
}
