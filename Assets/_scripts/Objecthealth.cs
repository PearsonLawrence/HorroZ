using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objecthealth : MonoBehaviour, IDamageable
{

    // Use this for initialization
    public float healthValue = 100.0f;

    public void TakeDamage(float damageDealt)
    {
        healthValue -= damageDealt;
        if(healthValue <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
