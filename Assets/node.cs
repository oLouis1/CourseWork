using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node {
    public node parent;
    public Vector3 position;
    public int heuristicCost;   //pure distance to target
    public int pathCost;        //distance to reach this node from current node
    public int totalCost;       //heurisitc + path
  
    List<node> open;
    List<node> closed;
    
    public int CalculateDistanceCost(Vector3 target)
    {
        float distance = Vector3.Distance(target, position);
        distance = distance * 10;
        return Mathf.RoundToInt(distance);   //I multiply by 10 and round as this gives nice whole numbers.

    }
    public void setCosts(node target, node parent) //sets all the cost values for a node
    {
        heuristicCost = CalculateDistanceCost(target.position);         
        pathCost = CalculateDistanceCost(parent.position);
        totalCost = heuristicCost + pathCost;

    }
    public void findNodeNeighbours(node currentNode)
    {
        
       
        node[] Neighbours = new node[9];
        Vector3 centerPosition = currentNode.position;
        for(int i = 0; i < 9; i++)
        {
            Neighbours[i] = currentNode;    //create a list of similar nodes
            Debug.Log(Neighbours[i]);
        }
        //tweak postion of each node
        // I believe I could've done this in a more effeicent way using the verticies array from the meshGen class as that is an array for all the points in the world
        //However I was not completely sure how to link them together so instead im going to use this method
        Vector3 testPoint = MeshGen.verticies[10];
        for(int i =0, zVal=0; zVal < 3 && i < 9; zVal++ ){
            for(int xVal=0; xVal<3;xVal++){
              
            }
        }
    }
    public void AStarSearch(node target)
    {
        //runs a AstarSearch algorithmn based off psudeocode from Sebestian Lague on youtube
        
        open = new List<node>(); //nodes to be checked
        closed = new List<node>();  //nodes already checked
        
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
