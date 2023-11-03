using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public Transform targetToFollow;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets the position of the camera to the target and then moves it away a bit for the offest.
        transform.position = targetToFollow.transform.position + offset;
    }
}
