using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform playerTarget;
    
    public float[,] pointCosts; //this is the height of each point higher points have higher cost and low points will need to be ignored.

  



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame\
    void Update()
    {
        if(pointCosts == null){ //assigns the worlds noiseMap/heigh of points to the cost array. this isnt done in start because the worlds noisemap is generated in start.
            pointCosts = NoiseGen.Noise;
        }


        //need to calculate the point the player is on  
        //need to find point itself is on
        //use A* algorithm to move from current point to player point
        //despawn enemy if too far away
        float distanceFromPlayer = Vector3.Distance(playerTarget.position, transform.position);
        if (distanceFromPlayer > 1000)
        { Destroy(this);}

        int playerX = (int)Mathf.Round(playerTarget.position.x);
        int playerZ = (int)Mathf.Round(playerTarget.position.z);

    }

    
}
