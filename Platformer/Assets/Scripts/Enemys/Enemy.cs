using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startStopTime;
    public float startTimeBtwAttack;
    public int maxHealth = 100;
    public float _speed = 2f;
    int currentHealth;
    public int damage;
    private float stopTime;
    public float normalSpeed;
    private Player player;

    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        //normalSpeed = _speed;
    }
    private void Update()
    {
        //if(stopTime <= 0)
        //{
        //    _speed = normalSpeed;
        //}
        //else
        //{
        //    _speed = 0;
        //    stopTime -= Time.deltaTime;
        //}
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


    public void TakeDamage(int damage)
    {

        if(currentHealth <= 0)
        {
            Die();
        }
        stopTime = startStopTime;
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(timeBtwAttack <= 0)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    async void Die()
    {
        Debug.Log("Enemy died ");
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        animator.SetBool("IsDead", true);
        this.enabled = false;

        await Task.Delay(millisecondsDelay: 6000);
        Destroy(gameObject);
    }
    public void OnEnemyAttack()
    {
        player._health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }
}
