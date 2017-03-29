using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    public GameObject box;
    public bool open;

    void Update()
    {
        if(open == true)
        {
            box.GetComponent<Animation>().Play();
            open = false;
        }
    }
}
