using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float Xinput;
    float Yinput;
    Rigidbody rigidbody;
    public float speed=10;

    private Vector3 movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Xinput = Input.GetAxis("Horizontal");
        Yinput = Input.GetAxis("Vertical");

        movementDirection = transform.forward * Yinput + transform.right * Xinput;

        rigidbody.AddForce(movementDirection * speed * 10f, ForceMode.Force);
        
    }
}
