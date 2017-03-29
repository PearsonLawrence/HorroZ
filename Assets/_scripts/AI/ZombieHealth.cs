using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{


    // Use this for initialization
    public float healthValue = 0;

    public void TakeDamage(float damageDealt)
    {
        healthValue += damageDealt;
     
    }
}
