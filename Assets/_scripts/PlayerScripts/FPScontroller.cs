using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPScontroller : MonoBehaviour {
    public GameObject Player;

    public GameObject SFdoor;
    public float lookSensitivity = 5f;
    public float Xrotation;
    public float yRotation;
    private float currentXrot;
    private float currentYrot;
    private float currentXpos;
    private float currentYpos;
    private float currentZpos;

    private float XrotV;
    private float YrotV;

    private float XposV;
    private float YposV;
    private float ZposV;

    public float SmoothDamp = .1f;
    private Vector3 offset;
    private Rigidbody rb;
    public GameObject gun;
    private Camera cam;

    public Camera cam2;
    public float viewZoom = 25, viewUnzoom = 60;
    public float zoomTime = .3f;
    private float Timer;
    private float dt;
    private float alpha;
    public Vector3 POC;

    bool bobRight;
    bool bobLeft;
    bool isMoving;
    bool isZoomed;
    public float bobtime = .3f;
    private float SetBobTime;
    public float switchTime;
    public float breather;
    public float breatherRun;
    private float breathTime;
    private int switchint = 5;
    private bool CacheBreath = true;
    public AudioSource[] footsteps;
    public int MedPacks;
    public AudioSource[] Breathing;
    public AudioSource pickup;
    public Text Center, pickupTXT, bookTXT;
    public Image medkit;
    public Image book;
    public GameObject fade;
    private float textTime;
    bool sprint;
    public bool SciFiDoor;
    public bool DoorKey;
    public bool ChestKey;
    public bool lockCode;
    public bool Alive = true;
    // Use this for initialization
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        cam = GetComponent<Camera>();
        offset = transform.position -= Player.transform.position;
        transform.position = Player.transform.position;
        transform.forward = Player.transform.forward;
        SetBobTime = bobtime;
        book.gameObject.SetActive(false);
        bookTXT.text = "";

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Alive)
        {
            sprint = Player.GetComponent<PlayerMovement>().IsSprinting;
            XposV = rb.velocity.x;
            YposV = rb.velocity.y;
            ZposV = rb.velocity.z;
            dt = Time.deltaTime;
            switchTime -= dt;
            breathTime -= dt;
            textTime -= dt;
            POC = transform.position + transform.forward * 100f;

            if (Input.GetMouseButton(1))
            {
                Timer -= dt;
                isZoomed = true;
            }
            else
            {
                Timer += dt;
                isZoomed = false;
            }

            Timer = Mathf.Clamp(Timer, 0, zoomTime);

            alpha = Timer / zoomTime;

            cam.fieldOfView = Mathf.Lerp(viewZoom, viewUnzoom, alpha);
            cam2.fieldOfView = cam.fieldOfView;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            if (sprint)
            {
                if (CacheBreath)
                {
                    breathTime = -1;
                    CacheBreath = false;
                }

                if (breathTime < 0)
                {

                    Breathing[4].Play();

                    breathTime = breatherRun;
                }

            }
            else
            {
                CacheBreath = true;
                if (breathTime < 0)
                {
                    int i = Random.Range(0, 2);
                    Breathing[i].Play();
                    breathTime = breather;
                }


            }


            if (isMoving)
            {
                if (sprint)
                {
                    bobtime = .2f;
                }
                else
                {
                    bobtime = SetBobTime;
                }

                if (switchTime < 0)
                {
                    if (bobRight && !isZoomed)
                    {
                        currentYpos += .03f;

                    }
                    else if (bobLeft && !isZoomed)
                    {
                        currentYpos += .03f;

                    }

                    int i = Random.Range(0, 2);
                    if (switchTime < -bobtime)
                    {
                        switchTime = bobtime;
                        if (!bobLeft)
                        {
                            bobLeft = true;
                        }
                        else
                        {
                            bobRight = true;
                        }
                        footsteps[i].Play();
                    }
                    else
                    {
                        footsteps[i].Stop();
                    }

                }
                else if (switchTime > 0)
                {

                    if (bobRight && !isZoomed)
                    {
                        currentYpos -= .03f;

                    }
                    else if (bobLeft && !isZoomed)
                    {
                        currentYpos -= .03f;

                    }

                }

            }
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
            {
                if (hit.collider.CompareTag("AKPickup"))
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        pickup.Play();
                        gun.SetActive(true);
                        Destroy(hit.collider.gameObject);
                        pickupTXT.text = "Picked up AKM";
                    }
                    Center.text = "Press E To pick Up";
                    textTime = 5;
                }
                else if (hit.collider.CompareTag("AmmoPickup"))
                {
                    if (gun.gameObject.active == true)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            pickup.Play();
                            if (gun.GetComponent<GunController>().Ammo <= 80)
                            {
                                gun.GetComponent<GunController>().Ammo += 40;

                                Destroy(hit.collider.gameObject);
                            }
                            pickupTXT.text = "Picked up Ammo +40";
                            textTime = 5;
                        }
                        Center.text = "Press E To pick Up";
                    }
                }
                else if (hit.collider.CompareTag("FirstAid"))
                {
                    if (MedPacks != 1)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            pickup.Play();
                            MedPacks += 1;
                            Destroy(hit.collider.gameObject);

                            pickupTXT.text = "Picked up MedKit press F to use";
                            textTime = 5;
                            medkit.gameObject.SetActive(true);
                        }
                        Center.text = "Press E To pick Up";
                    }
                    else
                    {
                        Center.text = "MedKit Full";
                    }


                }
                else if (hit.collider.CompareTag("DoorKey"))
                {

                    if (Input.GetKey(KeyCode.E))
                    {
                        pickup.Play();

                        Destroy(hit.collider.gameObject);

                        pickupTXT.text = "Picked up Door Key";
                        textTime = 5;
                        DoorKey = true;
                    }
                    Center.text = "Press E To pick Up";




                }
                else if (hit.collider.CompareTag("ChestKey"))
                {

                    if (Input.GetKey(KeyCode.E))
                    {
                        pickup.Play();

                        Destroy(hit.collider.gameObject);

                        pickupTXT.text = "Picked up Chest Key";
                        textTime = 5;
                        ChestKey = true;
                    }
                    Center.text = "Press E To pick Up";




                }
                else if (hit.collider.CompareTag("LockCodeBook"))
                {

                    if (Input.GetKey(KeyCode.E))
                    {
                        pickup.Play();

                        Destroy(hit.collider.gameObject);

                        pickupTXT.text = "Picked up Lock Codes";
                        textTime = 5;
                        lockCode = true;
                    }
                    Center.text = "Press E To pick Up";




                }
                else if (hit.collider.CompareTag("SF_doorKey"))
                {

                    if (Input.GetKey(KeyCode.E))
                    {
                        if (lockCode)
                        {




                            pickupTXT.text = "Unlocked Blast Door";
                            textTime = 5;
                            SciFiDoor = true;
                        }
                        else
                        {

                            pickupTXT.text = "You do not know the lock code";
                            textTime = 5;
                        }
                    }
                    Center.text = "Press E To enter the lock code";




                }
                else if (hit.collider.CompareTag("BaseDoor"))
                {

                    if (Input.GetKey(KeyCode.E))
                    {

                        hit.collider.GetComponent<DoorController>().open = true;
                        textTime = 11;

                    }

                    if (hit.collider.GetComponent<DoorController>().isOpen == true)
                    {
                        Center.text = "Press E To Close Door";

                    }
                    else
                    {
                        Center.text = "Press E To Open Door";

                    }



                }
                else if (hit.collider.CompareTag("LockedDoor"))
                {
                    if (DoorKey == true)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {

                            hit.collider.GetComponent<DoorController>().open = true;
                            textTime = 11;

                        }

                        if (hit.collider.GetComponent<DoorController>().isOpen == true)
                        {
                            Center.text = "Press E To Close Door";

                        }
                        else
                        {
                            Center.text = "Press E To Open Door";

                        }
                    }
                    else
                    {
                        Center.text = "Door is locked";
                    }


                }
                else if (hit.collider.CompareTag("Chest"))
                {
                    if (ChestKey)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {

                            hit.collider.GetComponent<BoxController>().open = true;


                        }


                        Center.text = "Press E To Open Crate";
                    }
                    else
                    {

                        Center.text = "Crate is locked";
                    }
                }
                else if (hit.collider.CompareTag("StartBook"))
                {
                    if (Input.GetKey(KeyCode.E))
                    {

                        book.gameObject.SetActive(true);
                        bookTXT.text = "Subject 001 seems to be advancing well after its injections. It was our biggest mistake but it has become our biggest asset. We may be able to pull through this now. I plan to continue to see the treatments through until the end.\n\n-New Update\nSupject 001 has finally been finished. But it remains in a sleeping state... hope is lost.\n\n\n\nPress X to close";

                    }


                    Center.text = "Press E To Read Book";
                }
                else if (hit.collider.CompareTag("Book"))
                {
                    if (Input.GetKey(KeyCode.E))
                    {

                        book.gameObject.SetActive(true);
                        bookTXT.text = "Subject 002\n - His Mental stability has dropped significantly. We will proceede with the injections to record later data\nUpdate- The subject has lost all sense of self. We have tried to put it down but we continue to fail. I alone think we should cancel this project so I have started preperations. My only fail safe is my rusty assult rifel I have stored away in the locker room.\n\nPress X to close";

                    }


                    Center.text = "Press E To Read Book";
                }
                else if (hit.collider.CompareTag("GunRoomBook"))
                {
                    if (Input.GetKey(KeyCode.E))
                    {

                        book.gameObject.SetActive(true);
                        bookTXT.text = "It has fallen.... \n The damn project failed and no one is left. They locked down the fuckin blast door to prevent the things to move on to upper levels but I beleive its to late. On top of that I left the damn keys to my fail safe somewhere where I was lounging around. If only I had it I could kill that damn thing in the control room and get the hell out of this hell...";

                    }


                    Center.text = "Press E To Read Book";
                }
                else if (hit.collider.CompareTag("TestSubBook"))
                {
                    if (Input.GetKey(KeyCode.E))
                    {

                        book.gameObject.SetActive(true);
                        bookTXT.text = "We were able to bring one down. We are testing ways that may be able to do more damage. It seems like the head is the weekest area at this point. \n\n-Update- We have noticed that injecting the dead once more revives and reactivates the fluids inside of the bodies. I am going to proceed to test this on the failed Subject 001\n\nPress x to close";

                    }


                    Center.text = "Press E To Read Book";
                }
                else if (hit.collider.CompareTag("SF_Door"))
                {



                    if (SciFiDoor == false)
                    {
                        Center.text = "The blast door seems to be sealed";
                    }
                }

            }
            else
            {

                Center.text = "";

            }

            if (book.gameObject.activeSelf)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    book.gameObject.SetActive(false);
                    bookTXT.text = "";
                }
            }


            if (textTime < 0)
            {
                pickupTXT.text = "";

            }
            if (textTime > 10)
            {

                {

                    Center.text = "";
                }
            }
            Xrotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
            yRotation += Input.GetAxis("Mouse X") * lookSensitivity;

            Xrotation = Mathf.Clamp(Xrotation, -90, 90);

            currentXpos = Mathf.SmoothDamp(currentXpos, Player.transform.position.x, ref XposV, SmoothDamp / 3);
            currentYpos = Mathf.SmoothDamp(currentYpos, Player.transform.position.y, ref YposV, SmoothDamp / 3);
            currentZpos = Mathf.SmoothDamp(currentZpos, Player.transform.position.z, ref ZposV, SmoothDamp / 3);

            currentXrot = Mathf.SmoothDamp(currentXrot, Xrotation, ref XrotV, SmoothDamp);
            currentYrot = Mathf.SmoothDamp(currentYrot, yRotation, ref YrotV, SmoothDamp);



            Player.transform.rotation = Quaternion.Euler(Player.transform.rotation.x, currentYrot, Player.transform.rotation.z);
            transform.rotation = Quaternion.Euler(currentXrot, currentYrot, 0);
            transform.position = new Vector3(currentXpos, currentYpos, currentZpos) + offset;

        }
    }
    }
