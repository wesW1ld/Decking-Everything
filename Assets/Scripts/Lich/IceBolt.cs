using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IceBolt : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    private float damageDealt = 1f;

    Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.LogError("No player found, LichAttacks");
        }

        direction = player.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);
    }

    void FixedUpdate() 
    {
        rb.velocity = direction * .5f;
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
