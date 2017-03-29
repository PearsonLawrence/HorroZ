using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunController : MonoBehaviour {
 
    public GameObject bullet;
    public GameObject Shootpoint;
    public GameObject Camera;
    public GameObject Camera1;
    public GameObject Model;
    public Transform Zoom, unZoomed;
    public float shootDelay = .5f;
    private float setDelay = .5f;
    public float shootForce = 100;
    public float MaxBulletSpread = 15f;
    public int clipSize = 20;
    private bool reloading;
    private Vector3 offset;
    private float Timer = 0;
    public float zoomTime =.3f;
    private float dt;
    private float alpha;
    private float setSpread;
    private Vector3 myPOC;
    public GameObject bulletHole;
    private Transform T;
    public ParticleSystem Pe;
    public ParticleSystem PS;
    public ParticleSystem CE;
    public ParticleSystem impact;
    public ParticleSystem Zombimpact;
    public ParticleSystem PSmoke;
    public Light flash;
    public Animator anim;
    public int Clips;
    float AnimTime = 2;
    public int Ammo;
    //public float Kick;
    //private float alphaKick;
    //private Vector3 kickBack;
    //private Vector3 
    public Text AmmoUI;
    public Text Count;
    
    float flashTimer;
    float flashDuration;

    void DoFlash(float duration)
    {
        
        flashTimer = 0;
        flashDuration = duration;
    }

    void UpdateFlash(float dt)
    {
        flashTimer += dt;
        flash.gameObject.SetActive(flashTimer <= flashDuration);

        float alpha = flashTimer / flashDuration;

        flash.gameObject.GetComponent<Light>().intensity = (1-alpha) * 10;
        flash.gameObject.GetComponent<Light>().range = (1-alpha) * 10;
    }


    // Use this for initialization
    void Start ()
    {
        offset = transform.position -= Camera.transform.position;
        setDelay = shootDelay;
        setSpread = MaxBulletSpread;
        clipSize = Ammo;
    }
	
	// Update is called once per frame
	void Update ()
    {
        string Ui = (Ammo - clipSize).ToString() + " / " + clipSize.ToString();
        
        AmmoUI.text = Ui.ToString();
        dt = Time.deltaTime;

        UpdateFlash(dt);
        AudioSource[] sources = GetComponents<AudioSource>();
        AudioSource audio = sources[0], audio2 = sources[1];
        setDelay -= Time.deltaTime;
        dt = Time.deltaTime;
        myPOC = Camera.GetComponent<FPScontroller>().POC;

        Vector3 t = transform.position + transform.forward * 100f;

        transform.LookAt(Vector3.Slerp(t, myPOC, .1f));

        //transform.forward = Vector3.Slerp(t, myPOC, .3f);// Vector3.Lerp(transform.forward,(myPOC - transform.position).normalized, dt*40);
        //currentXrot = Mathf.SmoothDamp(currentXrot, Xrotation, ref XrotV, SmoothDamp);
        //currentYrot = Mathf.SmoothDamp(currentYrot, yRotation, ref YrotV, SmoothDamp);
        // find the desired forward and slerp to it

        //transform.LookAt(myPOC);
        if (Input.GetMouseButton(1))
        {
            anim.GetComponent<Animator>().SetBool("IsAiming", true);
            setSpread =.5f;
            Timer -= dt;
        }
        else
        {
            anim.GetComponent<Animator>().SetBool("IsAiming", false);
            setSpread = MaxBulletSpread;
            Timer += dt;
        }

        Timer = Mathf.Clamp(Timer, 0, zoomTime);

        alpha = Timer / zoomTime;
        transform.position = Vector3.Slerp(Zoom.position,unZoomed.position,alpha);

        if (Input.GetMouseButton(0) && setDelay <= 0 && clipSize > 0 && !reloading)
        {
            
            anim.GetComponent<Animator>().SetBool("Fire", true);

            DoFlash(shootDelay/1.3f);                                  
             
            RaycastHit hit;
            Vector3 fireDirectionCam = Camera.transform.forward;

            Quaternion fireRotationCam = Quaternion.LookRotation(fireDirectionCam);
            Quaternion randRotCam = Random.rotation;
            fireRotationCam = Quaternion.RotateTowards(fireRotationCam, randRotCam, Random.Range(0.0f, setSpread));

            //Camera.transform.forward = fireRotationCam * Vector3.forward * 1000;

            FPScontroller fps = GetComponentInParent<FPScontroller>();

            fps.Xrotation -= Random.RandomRange(-.1f, .3f);
            fps.yRotation += Random.RandomRange(-.05f, .005f);



            //Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y + 10, Camera.transform.position.z -5);

            //transform.LookAt(Vector3.Slerp(t, myPOC, .5f));
            //Camera.transform.forward

            Vector3 lp = transform.localPosition;
           // lp.y += Random.RandomRange(-0.001f, 0f);
          //  lp.z -= Random.RandomRange(0.02f, 0.04f);
            transform.localPosition = Vector3.Slerp(lp, transform.localPosition,.02f);


            // Camera.transform.forward = Vector3.Slerp(t, myPOC, .3f);// Vector3.Lerp(transform.forward,(myPOC - transform.position).normalized, dt*40);
            Vector3 fireDirection = Shootpoint.transform.forward;

            Quaternion fireRotation = Quaternion.LookRotation(fireDirection);
            Quaternion randRot = Random.rotation;
            fireRotation = Quaternion.RotateTowards(fireRotation, randRotCam, Random.Range(0.0f, setSpread));
            //PS.simulationSpace = ParticleSystemSimulationSpace.World;

            //PS.transform.forward = fireRotation * Vector3.forward;
            if (PS != null)
            {
                PS.Emit(1);
            }
            if (CE != null)
            {
                CE.Emit(1);
            }
            Pe.simulationSpace = ParticleSystemSimulationSpace.World;
            Pe.transform.forward = fireRotation * Vector3.forward;
            if (Pe != null)
            {
                Pe.Emit(1);
            }
            PSmoke.simulationSpace 
                = ParticleSystemSimulationSpace.World;
            if (PSmoke != null)
            {
                PSmoke.Emit(1);
            }
            //Instantiate(bullet, Shootpoint.transform.position, fireRotation);

            if (Physics.Raycast(Camera.transform.position,fireRotationCam * Vector3.forward, out hit, 1000f))
            {
                
                if(hit.collider.CompareTag("ZombHead") || hit.collider.CompareTag("ZombBod"))
                {

                   

                        ParticleSystem bulletHit = Instantiate(Zombimpact, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        bulletHit.transform.parent = hit.collider.gameObject.transform;


                    IDamageable dmg = hit.collider.GetComponent<IDamageable>();
                    if (hit.collider.CompareTag("ZombHead") && dmg != null)
                    {
                        dmg.TakeDamage(50);
                    }
                    if (hit.collider.CompareTag("ZombBod") && dmg != null)
                    {
                        dmg.TakeDamage(50);
                    }
                }
                else if (!hit.collider.CompareTag("Radius"))
                {
                    ParticleSystem bulletHit = Instantiate(impact, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    bulletHit.transform.parent = hit.collider.gameObject.transform;
                    GameObject bulletH = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    bulletH.transform.parent = hit.collider.gameObject.transform;

                    IDamageable dmg = hit.collider.GetComponent<IDamageable>();
                    if (dmg != null)
                    {
                        dmg.TakeDamage(10);
                    }
                }



            }
            
            clipSize -= 1;
            Ammo -= 1;
            setDelay = shootDelay;

           
            audio.Play();
           

        }
        else
        {
            anim.GetComponent<Animator>().SetBool("Fire", false);

        }



        if (Input.GetKey(KeyCode.R) && clipSize < 40 && Ammo - clipSize > 0 )
        {
            anim.GetComponent<Animator>().SetBool("Reload", true);
            audio2.Play();
            if (Ammo >= 40)
            {
                
                clipSize = 40;
               
              
            }
            else
            {
                clipSize = Ammo;
             
            }

          

        }
        
           if(anim.GetComponent<Animator>().GetBool("Reload") == true)
            {
            AnimTime -= Time.deltaTime;
            
                
            
            if (AnimTime < 0)
            {
                anim.GetComponent<Animator>().SetBool("Reload", false);
                AnimTime = 1;
            }
            
            }
        

        if (audio2.isPlaying)
        {
            reloading = true;
        }
        else
        {
            reloading = false;
        }
       
        
    }
}
