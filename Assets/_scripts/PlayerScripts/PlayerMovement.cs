using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour {

    public float speed;
    private bool ismoving = false;
    public bool isGrounded = false;
    private Rigidbody rb;
    public float jumpHeight = 8;
    public Vector3 JumpPeek;
    public float gravity = 100;
    private float gravityPrivate;
    private bool jump;
    public Transform down;
    private float setSpeed;
    public float jumptime;
    private float setJump;
    public float sprintSpeed;
    private AudioSource JumpSound;
    public bool IsSprinting;
    public float Stamina = 100;
    public Image StaminaBar;
    public bool CoolingDown;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravityPrivate = gravity;
        setSpeed = speed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        JumpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        setJump -= Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        RaycastHit hit;

        Vector3 input = new Vector3(horizontal, 0, Vertical);
        Vector3 dir = Vector3.Normalize(transform.forward);
      
        if(Physics.Raycast(down.position, -down.up, out hit, .1f))
        {
           // gravityPrivate = 0;
            isGrounded = true;
        }
        else
        {
           
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded && !Input.GetMouseButton(1) && setJump < 0)
        {
            gravityPrivate = gravity;
            JumpPeek = new Vector3(0, transform.localPosition.y + jumpHeight, 0);
            isGrounded = false;
            jump = true;
            setJump = jumptime;
            JumpSound.Play();
        }
        if (isGrounded == false)
        {
            if (jump == true)
            {
                rb.AddForce(0, gravityPrivate, 0);
                if (transform.localPosition.y >= JumpPeek.y)
                {
                    //float AirTime = 4;
                    //AirTime -= Time.deltaTime;
                    //if (AirTime <= 0)
                    //{

                    //    AirTime = 4;
                    //}
                    gravityPrivate = -gravity ;
                }

            }
            else
            {
                gravityPrivate = gravity;

                rb.AddForce(0, -gravityPrivate * 2, 0);
            }
        }
        if (Input.GetMouseButton(1))
        {
            setSpeed = speed / 3;
        }
        else
        {
            setSpeed = speed;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetMouseButton(1) && Stamina > 0 && !CoolingDown)
        {
            IsSprinting = true;
           
        }
        else if(!Input.GetMouseButton(1))
        {
            IsSprinting = false;
          
        }
        
        
        Mathf.Clamp(Stamina, 0, 100);

        if (IsSprinting && !CoolingDown && Stamina != 0)
        {
            Stamina -= Time.deltaTime * 10;
            setSpeed = sprintSpeed;
        }
        else if(!IsSprinting && Stamina <= 100)
        {
            Stamina += Time.deltaTime *10;
            setSpeed = speed;
        }
        
        if(Stamina <= 0)
        {
            CoolingDown = true;
        }
        if (Stamina >= 100)
        {
            CoolingDown = false;
        }
        if (CoolingDown)
        {
            Stamina += Time.deltaTime *5;
           
        }

        StaminaBar.fillAmount = Stamina / 100;


        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(dir * setSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(dir * -setSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * setSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * setSpeed);
        }
        if (isGrounded && !Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
     
      
       
      

    }
    //public void OnCollisionStay(Collision other)
    //{
    //    if (transform.position.y >= other.transform.position.y)
    //    {

    //        gravityPrivate = 0;
    //        isGrounded = true;
    //        jump = false;
    //    }
    //    else
    //    {
    //        isGrounded = false;
    //        jump = false;
    //    }
       
    //}
    //public void OnCollisionExit(Collision other)
    //{
    //    if (jump == false)
    //    {
    //        gravityPrivate = gravity ;
    //    }

    //    isGrounded = false;
    //}

}
