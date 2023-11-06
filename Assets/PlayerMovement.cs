using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody Rigidbody;
    public float Thrust = 20f;
    public bool onGround;
    void Start()
    {
        //gets the players rigidbody
        Rigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision){
        onGround = true;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump") && onGround)
        {
            onGround = false;
            Rigidbody.AddForce(transform.up * Thrust);
        }
    }
}
