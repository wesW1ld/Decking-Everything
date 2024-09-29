using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private beholder beholder;
    private Color[] colors;

    RigidbodyConstraints2D originalConstraints;

    private float damageDealt = 1f;

    private void Start() 
    {
        colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = new Color(0.678f, 0.847f, 0.902f);
        colors[2] = new Color(0.5f, 0.0f, 0.5f);
        
        beholder = beholder = FindObjectOfType<beholder>();
        if (beholder == null)
        {
            Debug.LogError("ParentScript component not found in parent GameObject.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Reference to the player's health and movement scripts.
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            Movement playerMovement = other.GetComponent<Movement>();

            if (beholder.currentColor == colors[0])
            {
                // Player takes 1 damage currently.
                playerHealth.TakeDamage(damageDealt);
                Debug.Log("Player hit by red laser. Health: " + GameManager.Instance.health);
            }
            else if(beholder.currentColor == colors[1])
            {
                Debug.Log("Player hit by cyan laser.");
                StartCoroutine(FreezePlayer(other, playerMovement));
            }
            else if(beholder.currentColor == colors[2])
            {
                Debug.Log("Player hit by purple laser.");
                StartCoroutine(ReverseControls(other, playerMovement));
            }
        }
    }

    IEnumerator FreezePlayer(Collider2D other, Movement playerMovement)
    {
        // Freeze the player, but also disable their movement input to ensure they cannot move.
        other.GetComponent<Movement>().pushback = true;
        other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerMovement.FreezePlayer();
        yield return new WaitForSeconds(2.5f);
        other.GetComponent<Movement>().pushback = false;
        playerMovement.UnFreezePlayer();
    }

    IEnumerator ReverseControls(Collider2D other, Movement playerMovement)
    {
        playerMovement.reverseControls = true;
        playerMovement.ReverseControls();
        yield return new WaitForSeconds(3f);
        playerMovement.reverseControls = false;
    }
}
