using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderDmg : MonoBehaviour, IDamageable
{
    //health
    private float maxHealth = 2f;
    private float currentHealth;

    private GameObject player;
    GameObject tilemap;

    public bool HasTakenDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player");
        tilemap = GameObject.Find("TilemapWall2");
    }

    public void Damage(float damageAmt)
    {
        HasTakenDamage = true;

        currentHealth -= damageAmt;

        if (currentHealth <= 0)
        {
            player.GetComponent<Movement>().enabled = true; //fixes pushback bug if boss dies
            Destroy(tilemap);
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
