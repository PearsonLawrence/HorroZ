using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFeild : MonoBehaviour
{
    public GameObject Center;
    public bool Follow;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Follow = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Follow = false;
        }
    }
    void Update()
    {
        transform.position = new Vector3(Center.transform.position.x, Center.transform.position.y, Center.transform.position.z);
    }
}
