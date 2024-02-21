using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform playerTarget;
    private int attackTimer=75;
    public PlayerManager player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        //moving to the player and making enemy face the player
        Vector3 enemyToPlayer = playerTarget.position - transform.position;
        
        Vector3 velocity = enemyToPlayer.normalized;
        
       
        transform.rotation = Quaternion.LookRotation(velocity);

        transform.position += velocity;


        //attacking the player
        if (attackTimer > 0)
        {
            attackTimer -= 1;
        }
        if (enemyToPlayer.magnitude < 200)
        {
            if (attackTimer == 0)
            {
                player.receivedDamage(1);
                attackTimer = 75;
            }
            
        }
    }

    
}
