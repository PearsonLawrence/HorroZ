using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {

    public fading fader;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            fader.FadeOut = true;
            fader.scene = "WinScene";
        }
    }

}
