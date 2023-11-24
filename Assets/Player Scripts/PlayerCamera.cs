using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    float totalXRotation=0;
    float totalYRotation=0;

    public int sensitivity=50;

    public Transform playerTransform;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks cursor in middle of game screen
        Cursor.visible = false; //hides cursor
        cameraOffset = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = playerTransform.position + cameraOffset;


        //Getting mouse input to rotate player and camera
        float Xmouse = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float Ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        totalXRotation += Xmouse;
        totalYRotation -= Ymouse;

        totalYRotation = Mathf.Clamp(totalYRotation, -90f, 90f); //so player cant look 360 degrees around y-axis

        transform.rotation = Quaternion.Euler(totalYRotation, totalXRotation, 0); //rotates the camera
        playerTransform.rotation = Quaternion.Euler(0, totalXRotation, 0);
    }
  
}
