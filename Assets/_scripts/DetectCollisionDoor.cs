using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionDoor : MonoBehaviour {
    public GameObject door;
    public GameObject Player;

	void OnTriggerEnter(Collider other)
    {
        if (Player.GetComponent<FPScontroller>().SciFiDoor)
        {
            door.GetComponent<DoorAni>().open = true;
        }
      
    }
    void OnTriggerExit(Collider other)
    {
        if (Player.GetComponent<FPScontroller>().SciFiDoor)
        {
            door.GetComponent<DoorAni>().open = true;
        }

    }
}
