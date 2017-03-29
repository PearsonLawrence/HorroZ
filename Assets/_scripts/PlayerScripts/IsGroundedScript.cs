using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedScript : MonoBehaviour {

    public bool isGrounded;

    void Update()
    {
        isGrounded = false;
    }

    void OnTriggerStay(Collider other)
    {
        //if(!other.gameObject.GetComponent<FPScontroller>())
        isGrounded = true;

    }
    void OnTriggerExit(Collider other)
    {
        //if(!other.gameObject.GetComponent<FPScontroller>())
        isGrounded = false;

    }

}
