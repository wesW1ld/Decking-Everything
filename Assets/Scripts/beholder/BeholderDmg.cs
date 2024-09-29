using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderDmg : MonoBehaviour, IDamageable
{
    //health
    private float maxHealth = 2f;
    private float currentHealth;

    private GameObject player;

    public bool HasTakenDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player");
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
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
