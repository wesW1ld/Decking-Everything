using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderDmg : MonoBehaviour, IDamageable
{
    //health
    private float maxHealth = 7f;
    private float currentHealth;

    public bool HasTakenDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damageAmt)
    {
        HasTakenDamage = true;

        currentHealth -= damageAmt;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
