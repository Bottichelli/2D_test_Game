using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{


    public bool lookRight;

    public LayerMask whatIsDamager;

    [SerializeField] private bool isAttack;

    [SerializeField] private float hitTime;

    [SerializeField] private float checkDis;

    [SerializeField] private float startHitTime;

    [SerializeField] private Transform enemy;

    [SerializeField] private Transform boom;

    public Vector3 lookPos;

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
        matWhite = Resources.Load("Materials/WhiteFlash", typeof(Material)) as Material;

        matDefault = sr.material;
    }

    // Update is called once per frame
    private void Update()
    {
        HitReact();

        {
            Vector2 velocity = rb.velocity;
            velocity.x = speed * transform.localScale.x * -1;
            rb.velocity = velocity;
        }

        WhereToLook();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if ((collider.tag == "ChangeDirection") || (collider.tag == "Enemy"))
        {
            //animator.SetTrigger("Attack");

            ChangeDirection();
        }

        //else
        //{
        //    ChangeDirection();
        //}
    }

    private void ChangeDirection()
    {
        if (transform.localScale.x < 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    public void HitReact()
    {
        if (hitTime <= 0)
        {
            speed = 3f;
        }
        else
        {
            if (Physics2D.Raycast(enemy.transform.position, lookPos, checkDis, whatIsDamager))
            {
                ChangeDirection();
            }

            speed = -0.1f;
            hitTime -= Time.deltaTime;
            animator.SetTrigger("hit");
        }

    }
    public void TakeDamage(int damage)
    {
        hitTime = startHitTime;

        Health -= damage;

        GameController.Instance.Hit(this);

        sr.material = matWhite;

        if (Health == 0)
        {
            Instantiate(boom, transform.position, Quaternion.identity);
            speed = 0f;
            Die();
        }
        else
        {
            Invoke("ResetMaterial", 0.2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (lookRight == false)
        {
            Gizmos.DrawLine(enemy.position, enemy.position + (Vector3.right * checkDis));
        }
        else
        {
            Gizmos.DrawLine(enemy.position, enemy.position + (Vector3.left * checkDis));
        }
    }

    private void WhereToLook()
    {
        if (transform.localScale == Vector3.one)
        {
            lookRight = false;
            lookPos = Vector3.right;
        }

        else if (transform.localScale == new Vector3(-1, 1, 1))
        {
            lookRight = true;
            lookPos = Vector3.left;
        }
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

}
