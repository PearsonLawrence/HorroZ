using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAni : MonoBehaviour {
    private Vector3 Closed, Open;
    public GameObject collider;
    public bool open = false;
    private float openin;
    public bool isOpen;
    void Start()
    {
        Closed = transform.position;
        Open = transform.forward - transform.right;
    }
    // Update is called once per frame
    void Update()
    {


        if (open == true && isOpen)
        {
            openin -= Time.deltaTime * .1f;
            openin = Mathf.Clamp(openin, -.1f, 0);
            transform.Translate(0, 0, openin);
            if (transform.position.y <= Closed.y + .1f)
            {
                isOpen = false;
                open = false;
            }
        }

        if (open == true && !isOpen)
        {
            openin += Time.deltaTime * .05f;
            openin = Mathf.Clamp(openin, 0, .1f);
           transform.Translate(0, 0, openin);
            if (transform.position.y >= Closed.y +3)
            {
                isOpen = true;
                open = false;
            }
        }
    }


}
