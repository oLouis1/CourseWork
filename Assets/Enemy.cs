using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform playerTarget;
    private NavMeshAgent enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame\
    void Update()
    {
        enemyAI.SetDestination(playerTarget.position);
    }
}
