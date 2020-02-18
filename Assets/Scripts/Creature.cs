using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, IDestructable
{
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected Animator animator;

    [SerializeField] protected float speed;

    [SerializeField] private int health;

    [SerializeField] protected LayerMask whatIsEnemy;

    public int Health { get => health; set => health = value; }

    public void Die()
    {
        Destroy(gameObject, 0.2f);
    }

    public void Hit(int damage)
    {
        GameController.Instance.Hit(this);
    }
}
