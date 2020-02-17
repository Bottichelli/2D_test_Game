using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxStairs;
    [SerializeField] private Movement _player;

    private void Start()
    {
        Movement _player = gameObject.GetComponent<Movement>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {

        if (_player != null)
        {
            _player.OnStair = true;       
        }
        
    }

    void OnTriggerExit2D(Collider2D collider)
    {

        if (_player != null)

        {
            _player.OnStair = false;
            _player.isClimb = false;    
        }
     
    }

    private void Update()
    {
        StairsCollider();   
    }

    private void StairsCollider()
    {
        if (_player.isClimb == true)
        {
            boxStairs.enabled = false;
        }

        else if (_player.isClimb == false && _player.isJumping == false)
        {
            boxStairs.enabled = true;
        }
    }
}
