using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : Creature

{
    [SerializeField] private float attackRange;

    [SerializeField] private Transform attackPos;

    [SerializeField] private int damage;

    [SerializeField] private Enemy enemy;

    [SerializeField] private float checkDis;



    void Start()
    {
        Enemy enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(enemy.transform.position, -enemy.lookPos, checkDis, enemy.whatIsDamager))
        {
            animator.SetTrigger("Attack");
            //Debug.Log("attack!");
        }
    }

    public void EnemyAttack()
    {
        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

        for (int i = 0; i < playerToDamage.Length;)
        {
            playerToDamage[i].GetComponent<Health>().TakeDamage(damage);
            break;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

        Gizmos.color = Color.red;
        if (enemy.lookRight == false)
        {
            Gizmos.DrawLine(enemy.transform.position, enemy.transform.position + (Vector3.left * checkDis));
        }
        else
        {
            Gizmos.DrawLine(enemy.transform.position, transform.position + (Vector3.right * checkDis));
        }
    }
}
