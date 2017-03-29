using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {

    public GameObject center;
    private Rigidbody rb;
    public float speed;
    private Vector3 lastMoved;
    public Animator myAnimation;
    private float timer;
    public bool isIdle = false;
    // Use this for initialization
    void Start () {
        myAnimation = GetComponent<Animator>();

        rb = center.GetComponent<Rigidbody>();
        lastMoved = center.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
       
        myAnimation.SetFloat("Speed", speed);
        
    }
}
