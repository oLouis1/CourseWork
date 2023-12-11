using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public UnityEngine.UI.Image healthBar;
    public GameObject heldItem;
    public Vector3 itemOffset; 
    private bool attacking=false;
    private int attackingTickCounter=20;
     void Start()
    {
        rb = GetComponent<Rigidbody>();
        heldItem = Instantiate(heldItem, transform);
        
    }

    void OnCollisionEnter(Collision collision){
        onFloor = true;
        Debug.Log("touched floor");
        
    }

    void receivedDamage(float damage){
        health = health - damage;
        healthBar.transform.localScale = healthBar.transform.localScale + (new Vector3(-0.05f,0,0));
    }

    void FixedUpdate()
    {   
        

        heldItem.transform.position = transform.position + itemOffset;
        if(attacking && attackingTickCounter > 0){
            heldItem.transform.Rotate(5,0,0);
            attackingTickCounter--;
            if(attackingTickCounter==0){
                heldItem.transform.Rotate(-5*20f,0,0);
                attackingTickCounter=20;
                attacking=false;
            }
        }


        //for floor movement---------
        Xinput = Input.GetAxis("Horizontal");   //gets X and Z inputs
        Zinput = Input.GetAxis("Vertical");
        movementDirection = transform.forward * Zinput + transform.right * Xinput;

        if(movementDirection.magnitude != 0){
            transform.position+=movementDirection * speed;
        }
        //-----------------------------
        //input

        if(Input.GetKeyDown(KeyCode.T)){
            receivedDamage(1);
        }

        if(Input.GetKey("space") && onFloor){
            Debug.Log("Jumped");
            onFloor = false;
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.L)){
            Time.timeScale = 0;
        }

        if(Input.GetMouseButtonDown(0) && attacking==false){
            attacking = true;
            Debug.Log("clicking");
           
        }

       
        
    }
}
