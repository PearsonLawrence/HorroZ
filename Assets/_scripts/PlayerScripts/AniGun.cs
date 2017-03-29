using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniGun : MonoBehaviour {
    public GameObject gun;
    private Animation anim;
	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update ()
    {
        gun.GetComponent<GunController>();
      
        if (Input.GetMouseButton(1))
        {
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().Play("Fire");
        }
		

	}
}
