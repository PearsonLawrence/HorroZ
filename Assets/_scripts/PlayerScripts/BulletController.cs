using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    private Rigidbody rb;
    public float shootForce = 100;
    public GameObject bulletHole;
    public float destroyTime = 100;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

   
    }
    //void OnCollisionEnter(Collision other)
    //{
        
    //}
    // Update is called once per frame
    void FixedUpdate ()
    {
        //if(isActiveAndEnabled)
        //      {
        destroyTime -= Time.deltaTime;
        rb.velocity = (transform.forward * shootForce);
        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, transform.forward);
        //if (Physics.Raycast(ray, out hit, 1f))
        //{
        //  GameObject bulletH =  Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        //    bulletH.transform.parent = hit.collider.gameObject.transform;
            

        //    IDamageable dmg = hit.collider.GetComponent<IDamageable>();
        //    if(dmg != null)
        //    {
        //        dmg.TakeDamage(10);
        //    }

        //   Destroy(gameObject);
        //}
        if(destroyTime <= -4)
        {
            Destroy(gameObject);
        }
     
    }
	}

