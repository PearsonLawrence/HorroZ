using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableArea : MonoBehaviour {

    public GameObject area1, area2, area3;

    void OnTriggerEnter(Collider other)
    {
        return;
        if(area1.activeSelf == false)
        {
            area1.gameObject.SetActive(true);
        }
        if (area2.activeSelf == false)
        {
            area2.gameObject.SetActive(true);
        }
        if (area3.activeSelf == false)
        {
            area3.gameObject.SetActive(true);
        }
    }

}
