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
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
