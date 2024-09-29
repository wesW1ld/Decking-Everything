using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDmg : MonoBehaviour, IDamageable
{
    //health
    private float maxHealth = 4f;
    private float currentHealth;

    private Rigidbody2D rb;

    private GameObject player;

    public bool HasTakenDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Damage(float damageAmt)
    {
        HasTakenDamage = true;

        currentHealth -= damageAmt;

        if (currentHealth <= 0)
        {
            player.GetComponent<Movement>().enabled = true; //fixes pushback bug if boss dies
            Death();
        }
        else
        {
            if(player.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(5f, 10f), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-5f, 10f), ForceMode2D.Impulse);
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
