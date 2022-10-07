using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackpoint;
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 20;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Attack2();
                nextAttackTime = Time.time + 3f / attackRate;
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    async void Attack2()
    {
        animator.SetTrigger("Attack2");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage = 40);
            await Task.Delay(millisecondsDelay: 1100);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage = 10);
            await Task.Delay(millisecondsDelay: 180);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage = 10);

        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (attackpoint = null)
    //        return;

    //    Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    //}
}
