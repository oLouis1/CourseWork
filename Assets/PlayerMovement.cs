using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController movementController;
    public float playerSpeed=5;
    private Vector3 HorizontalDirection;
    private Vector3 VerticalDirection;
    public float jumpPower = 5;
    public float gravity = -9.81f;
    
    public Collider Collider;
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        movementController = gameObject.AddComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider collider){
       onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = movementController.isGrounded;

        if(movementController.isGrounded && VerticalDirection.y<0){   //to stop the character from trying to move down when it hits the ground
            VerticalDirection.y =0;
        }

        HorizontalDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));   //for moving sideways and forwards


        if(Input.GetButtonDown("Jump") && onGround){  //when player jumps
            VerticalDirection.y += (jumpPower * -1f * gravity);
        }
        VerticalDirection.y += gravity * Time.deltaTime;

        movementController.Move(VerticalDirection * Time.deltaTime);
        movementController.Move(HorizontalDirection * playerSpeed * Time.deltaTime);
    }
}
