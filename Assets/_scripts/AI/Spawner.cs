using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject zombie;
    public GameObject player;
    public float spawnTime;
    private float SetSpawn;
    public int maxZombies;
	// Use this for initialization
	void Start () {
        SetSpawn = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {

        SetSpawn -= Time.deltaTime;
        if(SetSpawn < 0)
        {
            GameObject zomb = Instantiate(zombie, transform);
            zomb.transform.position = zomb.transform.position + new Vector3(Random.RandomRange(-10, 10), 0, Random.RandomRange(-10, 10));
            zomb.GetComponent<WalkTo>().Player = player;
            SetSpawn = spawnTime;
        }
		
	}
}
