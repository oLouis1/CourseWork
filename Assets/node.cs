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

    public node(Vector3 cPosition) //node constuctor
    {
        position = cPosition;
        parent = null;
    }

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

    public float getMeshHieght(float xPoint, float zPoint)
    {
        RaycastHit raycastHit;
        Ray ray = new Ray(new Vector3(xPoint, 500f, zPoint), Vector3.down);
        if(Physics.Raycast(ray ,out raycastHit))    //start raycast from a high point and move down untill it hits land
        {
            return raycastHit.point.y;

        }
        return float.NaN;
        //if nothing is hit returns NaN

    }
    
    public node[] findNodeNeighbours(node currentNode)
    {

        Debug.Log(World.worldCreator.LandscapeMeshFilter.mesh);
        node[] Neighbours = new node[9];
        Vector3 centerPosition = currentNode.position;
        for (int i = 0; i < 9; i++)
        {
            Neighbours[i] = currentNode;    //create a list of similar nodes
        }

        for (int i = 0, z = -10; z < 20; z+=10)
        {
            for(int x = -10; x<20; x+=10) //i count up in tens as the player can move hundred "units" in around a second so going up in ones would be uncessary and labour intensive
            {
                if (!(x == 0 && z == 0))//so it doesnt change the value of the current node (though techinally it will be the same anyway)
                { 
                    float height = getMeshHieght(centerPosition.x + x, centerPosition.z + z);
                    Vector3 nodesPosition = new Vector3(centerPosition.x + x, height, centerPosition.z + z);

                    if (nodesPosition != currentNode.parent.position) //so its parent is not readded as a neighbour
                    {
                        Neighbours[i].position = nodesPosition;   //loops through the neighbours adjusting the positions
                    }
                    Debug.Log(Neighbours[i].position);
                    i++;
                    
                }
            }
        }
        return Neighbours;
    }
    public List<Vector3> AStarSearch(node target)
    {
        //runs a AstarSearch algorithmn based off psudeocode from Sebestian Lague on youtube
        
        open = new List<node>(); //nodes to be checked
        closed = new List<node>();  //nodes already checked
        
        open.Add(this);
        bool found = false;
        node currentNode = this;
        node closestNode =  null;

        int lowestCost = int.MaxValue;
        while (!found)
        {
            if(Vector3.Distance(currentNode.position,target.position)<100) //as not working with whole numbers i cant check for when they are exactly equal
            {                                                              //so by checking when the distance is small is good enough as it means the enemy will be in range
                found = true;
            }
            foreach  (node openNode in open)    //finds node with lowest cost which will be next to explore
            {
                openNode.setCosts(target, currentNode);
                if(openNode.totalCost < lowestCost)
                {
                    lowestCost = openNode.totalCost;    //if it has the smallest cost so far set the cloestnode to the one currently being check (openNode)
                    closestNode = openNode;
                }
            }
            if(closestNode != null)
            {
                closestNode.parent = currentNode;
                currentNode = closestNode;
            }
            closed.Add(currentNode);   //moves node into closed as now explored
            open.Remove(currentNode);
            node[] neighbours = findNodeNeighbours(currentNode); //gets neighbours of current node
            foreach(node neighbour in neighbours)
            {
                neighbour.parent = currentNode;
                neighbour.setCosts(target, currentNode);
                open.Add(neighbour);
            }
           

        }
        if (found)
        {

            List<Vector3> path = new List<Vector3>();   //backtracks through the parent nodes to get the final path
            path.Add(currentNode.position);
            while(currentNode.parent != null)
            {
                path.Add(currentNode.parent.position);
            }
            path.Reverse();
            return path;

            
        }
        return null;
    }
    
}
