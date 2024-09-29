using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;

    private float damageDealt = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(0, -2f);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageDealt);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
