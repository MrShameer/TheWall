using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HealthBar health;
    public Animator anim;
    public Transform camTransform;
    void Start()
    {
        health.SetMaxHealth(100);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClientSend.PlayerShoot(camTransform.forward);
        }

        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        //anim.SetFloat("verticle", z);
        //anim.SetFloat("horizontal", x);
        //anim.SetBool("jump", Input.GetKey(KeyCode.Space));

    }
    private void FixedUpdate()
    {
        SendInputToServer();
        
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)

    };

        ClientSend.PlayerMovement(_inputs);
    }


}