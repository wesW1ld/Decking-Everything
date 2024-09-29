using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushback : MonoBehaviour
{
    public float force = 15f;
    public float pushDuration = 0.2f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
            Debug.Log("Player hit by pushback.");

            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Movement playerMovement = other.GetComponent<Movement>(); // Assuming your movement script is named Movement

            if (rb != null && playerMovement != null)
            {
                StartCoroutine(ApplyPushback(rb, playerMovement));
            }
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

        // Wait for the pushback duration
        yield return new WaitForSeconds(pushDuration);

        // Re-enable the player's movement script
        playerMovement.enabled = true;
    }
}
