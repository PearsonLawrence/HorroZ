using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTime : MonoBehaviour {

	// Use this for initialization
    
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.forward);
        if (Physics.Raycast(ray, out hit, .01f))
        {
          if(!hit.collider)
                Destroy(gameObject) ;
            
          
        }
    }
}
