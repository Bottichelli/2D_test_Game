using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructable 
{
    
    void Hit(int damage);
    void Die();
    int Health { get; set; }
}
