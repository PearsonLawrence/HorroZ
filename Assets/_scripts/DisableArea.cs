using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableArea : MonoBehaviour {

    public GameObject area1, area2, area3;

    void OnTriggerEnter(Collider other)
    {
        return;
        if (area1.activeSelf == true)
        {
            area1.gameObject.SetActive(false);
        }
        if (area2.activeSelf == true)
        {
            area2.gameObject.SetActive(false);
        }
        if (area3.activeSelf == true)
        {
            area3.gameObject.SetActive(false);
        }
    }
}
