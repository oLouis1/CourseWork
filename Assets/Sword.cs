using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool attacking;
    public PlayerManager player;
    private int hits;
    //Script to handle when sword hits something
    private void Start()
    {
        player = GetComponentInParent<PlayerManager>();
    }
    private void Update()
    {
        attacking = player.attacking;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Tree"))
        {
            if (attacking)
            {
                other.transform.localScale *= 0.5f;
                if(other.transform.localScale.magnitude < 10)
                {
                    Destroy(other.gameObject);
                    player.receivedDamage(-1);
                }
                Debug.Log("hit Tree");
            }
        }
    }
}
