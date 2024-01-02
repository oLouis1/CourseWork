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
    private float healthBarWidth;
    public GameObject heldItem;
    public Vector3 itemOffset; 
    private bool attacking=false;
    private int attackingTickCounter=20;
     void Start()
    {
        rb = GetComponent<Rigidbody>();
        heldItem = Instantiate(heldItem, transform);
        healthBarWidth = healthBar.rectTransform.rect.width;
    }

    void OnCollisionEnter(Collision collision){
        onFloor = true;
        Debug.Log("touched floor");
        
    }

    void receivedDamage(float damage){
        health = health - damage;
        if(health == 0)
        {
            Color transparent = healthBar.color;    //hides healthbar when health is 0
            transparent.a = 0f;                     //will also be used to end game
            healthBar.color = transparent;
        }
        else
        {
            Vector2 currentHealthBarSize = healthBar.rectTransform.sizeDelta;
            float newWidth = Mathf.Max(0, currentHealthBarSize.x - healthBarWidth/20f);
            healthBar.rectTransform.sizeDelta = new Vector2(newWidth, currentHealthBarSize.y);
            healthBar.rectTransform.pivot = new Vector2(0f, 0.5f);
            healthBar.rectTransform.anchorMin = new Vector2(0f, 1f);
            healthBar.rectTransform.anchorMax = new Vector2(0f, 1f);
            healthBar.rectTransform.anchoredPosition = new Vector2(0f,1f);
        }
        
    }

    void FixedUpdate()
    {   
        
        //moving sword when attacking
       // heldItem.transform.position = transform.position + itemOffset;
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
