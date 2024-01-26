using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node {
    public node parent;
    public Vector3 position;
    public int heuristicCost;   //pure distance to target
    public int pathCost;        //distance to reach this node from current node
    public int totalCost;       //heurisitc + path
    
    int CalculateDistanceCost(Vector3 target)
    {
        float distance = Vector3.Distance(target, position);
        distance = distance * 10;
        return Mathf.RoundToInt(distance);   //I multiply by 10 and round as this gives nice whole numbers.

    }
    void setCosts(node target, node parent) //sets all the cost values for a node
    {
        heuristicCost = CalculateDistanceCost(target.position);         
        pathCost = CalculateDistanceCost(parent.position);
        totalCost = heuristicCost + pathCost;

    }
    void findNodeNeighbours(node currentNode)
    {
        node[] Neighbours = new node[7];
        Vector3 centerPosition = currentNode.position;
        for(int i = 0; i < 8; i++)
        {
            Neighbours[i] = currentNode;    //create a list of similar nodes
        }
        //tweak postion of each node
        Neighbours[0].position = centerPosition - new Vector3(-1,NoiseGen.Noise[(int)centerPosition.x-1,(int)centerPosition.z-1],-1);
    }
    void AStarSearch(node target)
    {
        //runs a AstarSearch algorithmn based off psudeocode from Sebestian Lague on youtube
        
        List<node> open = new List<node>(); //nodes to be checked
        List<node> closed = new List<node>();  //nodes already checked
        
        open.Add(this);
        bool found = false;
        node currentNode = this;
        node closestNode;
        int lowestCost = int.MaxValue;
        while (!found)
        {
            foreach  (node openNode in open)    //finds node with lowest cost which will be next to explore
            {
                openNode.setCosts(target, currentNode);
                if (lowestCost < openNode.totalCost)
                {
                    lowestCost = openNode.totalCost;
                }
            }
        }
    }
    
}
