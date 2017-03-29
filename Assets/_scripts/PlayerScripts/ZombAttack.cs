using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombAttack : MonoBehaviour {
    public AudioSource damage;
    public GameObject Zomb;
    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
            if (!Zomb.GetComponent<WalkTo>().dead)
            {
                damage.Play();
                Other.GetComponent<IDamageable>().TakeDamage(5);
            }
           
        }
    }

}
