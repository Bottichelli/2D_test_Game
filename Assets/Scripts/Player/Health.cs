using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : Creature
{
    public int numOfHearts;
    public int[] hits;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private float layerNum;

    [SerializeField] private Movement _player;


    private void Start()
    {
        Movement _player = gameObject.GetComponent<Movement>();
    }

    private void Update()
    {


        if (Health > numOfHearts)
        {
            Health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }

    public void TakeDamage(int damage)
    {

        Health -= damage;

        if (Health == 0)
        {
            Die();
        }

    }



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == layerNum)
    //    {    
    //        Debug.Log(collision.ToString());
    //    }
    //}

    //IEnumerator Dmgr()
    //{
    //    yield return new WaitForSeconds(1f);
    //    _player.isDamaged = false;
    //    _player.def_speed = 10f;
    //}

    
}
