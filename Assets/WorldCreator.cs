using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WorldCreator : MonoBehaviour
{


    // variables for landscape--------
    public MeshFilter LandscapeMeshFilter;
    public Mesh LandscapeMesh;
    public int Xsize; 
    public int Zsize;
    public float worldLacunarity = 1.5f;    //varaibles for perlin noise
    public float worldPersistance = 0.5f;   
    public int worldOctaves = 3;
    public float zoom;
    public float heightScale;
    private Color[] colourMap;
    public AnimationCurve smoothHeightCurve;
    private float seed;
    public Gradient gradient;
    //varaibles for prefabs
    public GameObject treePrefab;
    public GameObject orePrefab;
    //------------------variables for water
    public Transform water;
    private MeshFilter waterFilter;
    public AnimationCurve waterHeightCurve;


    public void Start()
    {
        World.worldCreator = this;
        seed = World.worldSeed;
        LandscapeMeshFilter = GetComponent<MeshFilter>();
        makeMap();
    }

    // Start is called before the first frame update
    
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
          
            Debug.Log("Test");  //creates new random world when r is pressed
            makeMap();
        }
    }

   private void makeTreeCluster(Vector3 startPoint) //for making a cluster of trees around a certain point
    {
        bool makeTrees = true;

        while (makeTrees)
        {
            Instantiate(treePrefab, new Vector3(startPoint.x + Random.Range(0,150), startPoint.y, startPoint.z + Random.Range(0,150)), Quaternion.identity);
            if (Random.Range(1,11) < 8)
            {
                makeTrees = false;
            }
            
        }
        
    }

    public void makeMap()
    {
        //making water
        Debug.Log(water);
        waterFilter = water.GetComponent<MeshFilter>();
        float[,] waterHeights = new float[Xsize, Zsize];
        for (int z = 0; z < Zsize; z++)         //creates a flat water plane
        {
            for (int x = 0; x < Xsize; x++)
            {
                waterHeights[x, z] = Random.Range(0, 5);
            }
        }
        waterFilter.mesh = MeshGen.generateMesh(Xsize, Xsize, waterHeights, 1, waterHeightCurve);

        //making land
        LandscapeMesh = new Mesh();
    
        float[,] noise = NoiseGen.generateNoise(Xsize, Zsize, worldLacunarity, worldPersistance, worldOctaves, zoom, seed);   //creates noise map
        
        LandscapeMesh = MeshGen.generateMesh(Xsize, Zsize, noise, heightScale, smoothHeightCurve);  // creates mesh for terrain

        
        colourMap = new Color[Xsize * Zsize];
        for (int i = 0,z=0; z < Zsize; z++) //for colouring landscape
        {
            for (int x = 0; x < Xsize; x++)
            {
                colourMap[i] = gradient.Evaluate(noise[x, z]);
                i++;
            }
        }
        //adding land details
        Vector3[] worldPoints = LandscapeMesh.vertices;
        bool[] isResourceOnPoint = new bool[worldPoints.Length]; //tracking if a tree/ore vein has already been made on a point
       
        for(int i = 0; i<worldPoints.Length; i += 1)
        {
            Vector3 currentPoint = worldPoints[i];
            Debug.Log(currentPoint);
            currentPoint.x *= 30;
            currentPoint.z *= 30;
            //Debug.Log(currentPoint);
            if (!isResourceOnPoint[i] && currentPoint.y>35) //if no resoruce on point create a tree and if above water
            {
                if (Random.Range(1, 1001) > 997) // 0.1% of spawning a tree cluster
                {
                    makeTreeCluster(currentPoint);   
                }
                isResourceOnPoint[i] = true;
            }
        }

        //assigning mesh values
        GetComponent<MeshCollider>().convex = false;
        LandscapeMesh.colors = colourMap;
        LandscapeMeshFilter.mesh = LandscapeMesh;
        GetComponent<MeshCollider>().sharedMesh = LandscapeMesh;
    }

}
