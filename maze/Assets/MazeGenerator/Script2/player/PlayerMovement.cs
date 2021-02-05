using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public Animator anim;

    public int maxHealth=100;
    private int currentHealth;

    public HealthBar healthBar;

    public Camera cam;
    // Start is called before the first frame update
   /* void Start()
    {
        
    }*/

    // Update is called once per frame

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
            anim.SetBool("jump", false);
        }

        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        anim.SetFloat("verticle", z);
        anim.SetFloat("horizontal", x);
        //UnityEngine.Debug.Log(Time.deltaTime);


        Vector3 move = transform.right * x + transform.forward * z;
        

        controller.Move(move * speed * Time.deltaTime);

        if (healthBar.GetHealth()==0f)
        {
            anim.enabled = false;
            cam.GetComponent<Mouselook>().enabled = false;
        }

        if (Input.GetButtonDown("Jump")&& isGrounded)
        {
            anim.SetBool("jump", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            
        }
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        currentHealth = healthBar.GetHealth();
       // UnityEngine.Debug.Log(currentHealth);

        /* if (Input.GetKeyDown(KeyCode.Space))
         {
             currentHealth -= 20;
             healthBar.SetHealth(currentHealth);
         }*/
    }
}
