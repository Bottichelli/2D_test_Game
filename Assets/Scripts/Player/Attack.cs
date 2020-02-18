using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Creature
{

    [SerializeField] private float attackRange;

    [SerializeField] private float startAttackTime;

    [SerializeField] private Transform attackPos;

    [SerializeField] private int damage;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("attack");
        }
    }


    public void PlayerAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

        for (int i = 0; i < enemiesToDamage.Length; )
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            break;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
