using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    private GameObject Lich;
    private Rigidbody2D rb;

    float direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Lich = GameObject.FindWithTag("Lich");
        if(Lich == null)
        {
            Debug.LogError("No Lich found, Skull");
        }

        if(transform.position.x < Lich.transform.position.x) //left
        {
            direction = -1f;
        }
        else //right
        {
            direction = 1f;
            transform.localScale = transform.localScale * new Vector2(-1, 1);
        }

        rb.AddForce(new Vector2(direction * 0f, -10f), ForceMode2D.Impulse);

        StartCoroutine(DestroySkull());
    }

    void FixedUpdate()
    {
        if(rb.velocity.y <= 0)
        {
            rb.velocity = new Vector2(direction * 10f, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1f);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Floor"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    IEnumerator DestroySkull()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
