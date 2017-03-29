using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private Vector3 Closed, Open;
    public GameObject rot;
    public bool open = false;
    private float rotatin;
    public bool isOpen;
    [SerializeField]
    private OcclusionPortal portal;

void Start()
    {
        Closed = transform.forward;
        Open = transform.forward - transform.right;
    }
    // Update is called once per frame
    void Update ()
    {
        if (portal)
        {
            portal.open = open || isOpen;
        }
		
        if (open == true && !isOpen)
        {
            rotatin -= Time.deltaTime * 100;
            rotatin = Mathf.Clamp(rotatin, -90, 0);
            rot.transform.rotation = Quaternion.Euler(0, rotatin, 0);
            if(rotatin <= -90)
            {
                isOpen = true;
                open = false;
            }
        }
     
        if (open == true && isOpen)
        {
            rotatin += Time.deltaTime * 100;
            rotatin = Mathf.Clamp(rotatin, -90, 0);
            rot.transform.rotation = Quaternion.Euler(0, rotatin, 0);
            if (rotatin >= 0)
            {
                isOpen = false;
                open = false;
            }
        }
    }
}
