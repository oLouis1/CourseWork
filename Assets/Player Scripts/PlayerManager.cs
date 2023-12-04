using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image healthBar;
    public GameObject heldItem;
   
     void Start()
    {
        rb = GetComponent<Rigidbody>();
        heldItem = Instantiate(heldItem, transform.position, transform.rotation);
        
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
        

        heldItem.transform.position = transform.position + new Vector3(30, 0, 30);



        //for floor movement---------
        Xinput = Input.GetAxis("Horizontal");   //gets X and Z inputs
        Zinput = Input.GetAxis("Vertical");
        movementDirection = transform.forward * Zinput + transform.right * Xinput;

        if(movementDirection.magnitude != 0){
            transform.position+=movementDirection * speed;
        }
        //-----------------------------
        //jumping

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

        //if()

       
        
    }
}
