using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // Interface for all damageable objects.
    // All damageable objects must have a Damage method that can be called by their damager.
    public void Damage(float damageAmount);

    // This bool helps prevent multiple instances of damage from a single attack.
    public bool HasTakenDamage { get; set; }
}
