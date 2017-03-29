using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamPtController : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Zombie"))
        {
            WalkTo target = other.GetComponent<WalkTo>();
            GameObject once = target.RoamPt;
            int idx = Random.Range(0, target.RoamPts.Length - 1);
            Debug.Log("Roam" + idx);
           
            target.RoamPt = target.RoamPts[idx];
            if(target.RoamPt == once)
            {
                if ((idx + 1) != null)
                {
                    target.RoamPt = target.RoamPts[idx + 1];
                }
                else
                {
                    target.RoamPt = target.RoamPts[idx - 1];
                }
            }
        }
    }
}
