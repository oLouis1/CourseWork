using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    //movement variables
    float Xinput;   //2D inputs and speed of player
    float Zinput;
    
    public float jumpPower;
    private bool onFloor;
    
    public Rigidbody rb;
    private Vector3 movementDirection;//direction player will move in based off which way there looking
    public Transform enemyPostion;
    //player stats variables
    public float speed=3.5f;
    public float health = 20;
    public UnityEngine.UI.Image healthBar;
    private float healthBarWidth;
    public GameObject heldItem;
    public Vector3 itemOffset; 
    public bool attacking=false;
    private int attackingTickCounter=20;
    public Text scoreText;
    private int score;
    private int scoreTimer;
     void Start()
    {
        rb = GetComponent<Rigidbody>();
        heldItem = Instantiate(heldItem, transform);
        healthBarWidth = healthBar.rectTransform.rect.width;
        score = 0;
        scoreTimer = 50;
    }

    void OnCollisionEnter(Collision collision){
        onFloor = true;
       
        
    }

    public void receivedDamage(float damage){
        health = health - damage;
        if(health == 0)
        {
            Color transparent = healthBar.color;    //hides healthbar when health is 0
            transparent.a = 0f;                     //will also be used to end game
            healthBar.color = transparent;
        }
        else
        {
            float newWidth;
            Vector2 currentHealthBarSize = healthBar.rectTransform.sizeDelta;
            if (damage < 0)
            {
                 newWidth = Mathf.Max(0, currentHealthBarSize.x + healthBarWidth / 20f);   //if damage less than zero than player is being healed so make bar bigger
            }
            else
            {
                 newWidth = Mathf.Max(0, currentHealthBarSize.x - healthBarWidth / 20f);
            }
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
        //handling Score counter
        scoreText.text = "Score: " + score;
        scoreTimer -= 1;
        if (scoreTimer == 0)
        {
            scoreTimer = 100;
            score += 1;
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
            
        }

        if(Input.GetKey("space") && onFloor){
            
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

        if (health == 0)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            SceneManager.LoadScene(0);
            
        }

       
        
    }
}
