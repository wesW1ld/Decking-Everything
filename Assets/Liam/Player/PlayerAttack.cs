using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // CircleCastAll variables.
    [SerializeField] private Transform attackTransform;
    [SerializeField] private LayerMask attackableLayer;
    private float attackRange = 0.75f;
    private Vector2 attackDirection;

    public bool shouldBeDamaging { get; private set; } = false;

    private List<IDamageable> iDamageables = new List<IDamageable>();

    // Amount of damage the player's attack does.
    private float damageAmount = 1f;

    // Array that stores all the hits from the circle cast all.
    private RaycastHit2D[] attackHits;

    // Anim ref.
    private Animator animator;

    // Attack cooldown.
    private float timeBetweenAttacks = 0.95f;
    private float attackTimeCounter;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Set in start to ensure player can attack the moment the game begins if they so desire.
        attackTimeCounter = timeBetweenAttacks;
    }

    private void Update()
    {
        // Set the direction of the player's attack based on which direction they are moving in.
        SetAttackDirection();

        // If the attack input is pressed, attack the enemy.
        if (InputManager.instance.attackInput && attackTimeCounter >= timeBetweenAttacks)
        {
            // Reset the attack time counter to begin attack cooldown.
            attackTimeCounter = 0f;

            animator.SetTrigger("attack");
        }

        // Increase the attack time counter.
        attackTimeCounter += Time.deltaTime;
    }

    private void SetAttackDirection()
    {
        // Player is facing left, so attack in the left direction.
        if (transform.localScale.x > 0)
        {
            attackDirection = -transform.right;
        }
        // Player is facing right, so attack in the right direction.
        else if (transform.localScale.x < 0)
        {
            attackDirection = transform.right;
        }
    }

    // Called as an animation event in the player attack animation.
    public IEnumerator DamageWhileAttackIsActive()
    {
        shouldBeDamaging = true;

        // While the player can damage the enemies, check for enemies and damage them if they are damageable.
        while (shouldBeDamaging)
        {
            attackHits = Physics2D.CircleCastAll(attackTransform.position, attackRange, attackDirection, 0f, attackableLayer);

            for (int i = 0; i < attackHits.Length; i++)
            {
                IDamageable iDamageable = attackHits[i].collider.gameObject.GetComponent<IDamageable>();

                // If an iDamageable object was found, call its Damage method and add it to a list.
                if (iDamageable != null && !iDamageable.HasTakenDamage)
                {
                    iDamageable.Damage(damageAmount);
                    iDamageables.Add(iDamageable);
                }
            }

            yield return null;
        }

        // Reset the damageables' ability to take damage, so they can be damaged again.
        ResetDamageablesTakeDamageAbility();
    }

    private void ResetDamageablesTakeDamageAbility()
    {
        // Set all IDamageable objects in the list to false so they can be damaged again.
        foreach (IDamageable damagedThing in iDamageables)
        {
            damagedThing.HasTakenDamage = false;
        }

        // Clear the list.
        iDamageables.Clear();
    }

    // Draw the CircleCastAll gizmo so we can see that the attack is working.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    #region Animation Triggers

    public void ShouldBeDamagingToTrue()
    {
        shouldBeDamaging = true;
    }

    public void ShouldBeDamagingToFalse()
    {
        shouldBeDamaging = false;
    }

    #endregion
}
