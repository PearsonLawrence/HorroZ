using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class WalkTo : MonoBehaviour {


    public Transform goal;
    public GameObject[] RoamPts;
   
    public GameObject Player, RoamPt;
    NavMeshAgent agent;
    public GameObject radiuz;
    public bool follow;
    private bool Run;
    public GameObject zomb;
    private Vector3 dest;
    public float chaseTime;
    private float chaseTimer;
    private Vector3 playerLastPos;
    Vector3 lastPos;
    bool there = true;
    bool cacheOncePlayer = true;
    bool CacheOnceZomb = true;
    float movevar = 10;
    float moveTime = 3;
    float switchTime = 1;
    float attackSwitch = 1;
    public GameObject Head;
    public GameObject Body;
    public float StartHealth;
    public float Health = 100f;
    private float deathPause = 1;
    public bool dead;
    public float WalkTime;
    public float RunTime;
    float playTime;
    float dt;
    float CheckDt;
    Vector3 lastMoved;
    Vector3 lastPosAni;
    public AudioSource[] footsteps;
    public AudioSource[] ZombSounds;
    public bool roamer = false;
    public bool alive;
   
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        dt = Time.deltaTime;
        StartHealth = Health;
        RoamPt = RoamPts[Random.Range(0, RoamPts.Length - 1)];


    }
    void Update()
    {

        
            follow = radiuz.GetComponent<ActiveFeild>().Follow;




            if ((Health - (Head.GetComponent<ZombieHealth>().healthValue + Body.GetComponent<ZombieHealth>().healthValue)) < 0)
            {
                deathPause -= dt;
                if (deathPause > 0)
                {
                    zomb.GetComponent<AnimationScript>().myAnimation.SetBool("Died", true);
                }

                agent.Stop();
                dead = true;
                follow = false;
                return;
            }


            if (roamer == true && !follow)
            {
                if (!Run)
                {

                    dest = RoamPt.transform.position;
                    zomb.GetComponent<AnimationScript>().speed = 1;
                    agent.speed = 2;
                    agent.acceleration = 3;

                }


                agent.destination = dest;
            }

            if (follow && !dead)
            {

                playTime -= dt;

                if (playTime < 0)
                {
                    int i = Random.Range(0, 2);
                    ZombSounds[i].Play(2);
                    playTime = 30;
                }
                if (CacheOnceZomb)
                {
                    zomb.GetComponent<AnimationScript>().speed = 1;
                    lastPos = agent.transform.position;
                    CacheOnceZomb = false;
                }
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        chaseTimer = chaseTime;
                        Run = true;
                    }
                }
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        zomb.GetComponent<AnimationScript>().myAnimation.SetBool("Attacked", true);
                    }
                }





                if (!Run && !roamer)
                {

                    moveTime -= dt;
                    if (moveTime < 0)
                    {
                        moveTime = 3;
                        movevar = movevar * -1;
                        dest = lastPos + new Vector3(0, 0, movevar);
                    }
                    WalkTime -= dt;
                    if (WalkTime < 0 && !zomb.GetComponent<AnimationScript>().myAnimation.GetBool("Attacked"))
                    {
                        int i = Random.Range(0, 2);
                        footsteps[i].Play();
                        WalkTime = .6f;
                    }
                    zomb.GetComponent<AnimationScript>().speed = 1;
                    agent.speed = 2;
                    agent.acceleration = 3;
                }
            else if (!Run && roamer)
            {
                moveTime -= dt;

                dest = RoamPt.transform.position;

                WalkTime -= dt;
                if (WalkTime < 0 && !zomb.GetComponent<AnimationScript>().myAnimation.GetBool("Attacked"))
                {
                    int i = Random.Range(0, 2);
                    footsteps[i].Play();
                    WalkTime = .6f;
                }
                zomb.GetComponent<AnimationScript>().speed = 1;
                agent.speed = 2;
                agent.acceleration = 3;
            }

            else if(Run)
                {
                    chaseTimer -= dt;
                    if (chaseTimer < 0)
                    {
                        dest = playerLastPos;
                    }
                    else
                    {
                        dest = Player.transform.position;
                    }
                    RunTime -= dt;
                    if (RunTime < 0 && !zomb.GetComponent<AnimationScript>().myAnimation.GetBool("Attacked"))
                    {
                        int i = Random.Range(0, 1);
                        footsteps[i].Play();
                        RunTime = .3f;
                    }
                    zomb.GetComponent<AnimationScript>().speed = 2;
                    agent.speed = 7;
                    agent.acceleration = 12;
                    agent.destination = dest;
                }

                if (chaseTimer < 0 && Run)
                {
                    if (cacheOncePlayer)
                    {
                        playerLastPos = Player.transform.position;
                        cacheOncePlayer = false;
                    }
                    if (chaseTimer < -5 || transform.position == playerLastPos && cacheOncePlayer == false)
                    {
                        Run = false;
                    }

                }
                if (zomb.GetComponent<AnimationScript>().myAnimation.GetBool("Attacked"))
                {
                    attackSwitch -= dt;
                    if (attackSwitch < 0)
                    {
                        zomb.GetComponent<AnimationScript>().myAnimation.SetBool("Attacked", false);
                        attackSwitch = 1;
                    }
                }


                agent.destination = dest;
                zomb.GetComponent<AnimationScript>().myAnimation.SetBool("Isidle", false);
                switchTime = 5;
                playTime -= dt;

            }
            else if (!follow && !roamer)
            {

                switchTime -= dt;
                cacheOncePlayer = true;
                CacheOnceZomb = true;
                chaseTimer = 0;

                if (transform.position.x < lastPosAni.x || transform.position.y < lastPosAni.y || transform.position.z < lastPosAni.z || transform.position.x > lastPosAni.x || transform.position.y > lastPosAni.y || transform.position.z > lastPosAni.z)
                {
                    zomb.GetComponent<AnimationScript>().speed = 1;
                }
                else
                {
                    zomb.GetComponent<AnimationScript>().speed = 0;
                    zomb.GetComponent<AnimationScript>().myAnimation.SetBool("Isidle", true);
                }

                lastPosAni = transform.position;
                //if (switchTime < 0)

            }





        
    }
}
