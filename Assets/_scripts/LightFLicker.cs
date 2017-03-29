using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFLicker : MonoBehaviour {
    public Light light;
    public float FlickerRange;
    public float flickerTime;
	// Update is called once per frame
	void Update ()
    {
        flickerTime -= Time.deltaTime;
        if(flickerTime <= Random.RandomRange(.1f,FlickerRange))
        {

            if (light.gameObject.active == false)
            {
                light.gameObject.SetActive(true);
            }
            else
            {
                light.gameObject.SetActive(false);
            }
            flickerTime = FlickerRange;
        }
        
	}
}
