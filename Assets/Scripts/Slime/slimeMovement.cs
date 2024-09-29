using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.LogError("No player found, LichAttacks");
        }

        animator = GetComponent<Animator>();

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1.6f);
        while(true)
        {
            animator.SetTrigger("jump");

            yield return new WaitForSeconds(1.6f);
            if(player.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(-120f, 500f));
            }
            else
            {
                rb.AddForce(new Vector2(120f, 500f));
            }
            yield return new WaitForSeconds(1.6f);
            
            animator.SetTrigger("land");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player") && (other.transform.position.y + 3f) < transform.position.y)
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1f);
            StartCoroutine(ApplyPushback(other.gameObject.GetComponent<Rigidbody2D>(), other.gameObject.GetComponent<Movement>()));
        }
    }

    private IEnumerator ApplyPushback(Rigidbody2D rb, Movement playerMovement)
    {
        // Disable the player's movement script
        playerMovement.enabled = false;

        // Apply the pushback force
        if (rb.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-10f, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(10f, rb.velocity.y);
        }

        // Wait for the pushback duration
        yield return new WaitForSeconds(1f);

        // Re-enable the player's movement script
        playerMovement.enabled = true;
    }
}
