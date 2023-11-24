using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //movement variables
    float Xinput;   //2D inputs and speed of player
    float Zinput;
    
    public float jumpPower;
    private bool onFloor;
    private int jumpTicks;
    public Rigidbody rb;
    private Vector3 movementDirection;//direction player will move in based off which way there looking

    //player stats variables
    public float speed=3.5f;
    public float health = 20;
   
     void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision){
        onFloor = true;
        Debug.Log("touched floor");
        
    }


    void FixedUpdate()
    {   


        //for floor movement---------
        Xinput = Input.GetAxis("Horizontal");   //gets X and Z inputs
        Zinput = Input.GetAxis("Vertical");
        movementDirection = transform.forward * Zinput + transform.right * Xinput;

        if(movementDirection.magnitude != 0){
            transform.position+=movementDirection * speed;
        }
        //-----------------------------
        //jumping

        if(Input.GetKeyDown("space")){
            Debug.Log("Jumped");
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }

       
        
    }
}
