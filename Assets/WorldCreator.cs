using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WorldCreator : MonoBehaviour
{


    // variables for landscape--------
    Mesh LandscapeMesh;
    public int Xsize; 
    public int Zsize;
    public float worldLacunarity = 1.5f;
    public float worldPersistance = 0.5f;   
    public int worldOctaves = 3;
    public float zoom;
    public float heightScale;
    private Color[] colourMap;
    public AnimationCurve smoothHeightCurve;
    private float seed;
    public Gradient gradient;
    //------------------
    private Transform water;
    private MeshFilter waterFilter;
    public AnimationCurve waterHeightCurve;

    // Start is called before the first frame update
    void Start()
    {
        water = transform.Find("Water");
        waterFilter = water.GetComponent<MeshFilter>();
        float[,] waterHeights = new float[Xsize,Zsize];
        for (int z = 0; z < Zsize; z++)    
        {
            for (int x = 0; x < Xsize; x++)
            {
                waterHeights[x, z] = Random.Range(0,1);
            }
        }
        waterFilter.mesh = MeshGen.generateMesh(Xsize, Xsize, waterHeights, 1, waterHeightCurve);

        makeMap();
      


    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
          
            Debug.Log("Test");
            makeMap();
        }
    }

   

    void makeMap()
    {
        LandscapeMesh = new Mesh();
        seed = Random.Range(0,100000);
        float[,] noise = NoiseGen.generateNoise(Xsize, Zsize, worldLacunarity, worldPersistance, worldOctaves, zoom, seed);   //creates noise map
        
        LandscapeMesh = MeshGen.generateMesh(Xsize, Zsize, noise, heightScale, smoothHeightCurve);  // creates mesh for terrain

        
        colourMap = new Color[Xsize * Zsize];
        for (int i = 0,z=0; z < Zsize; z++) //for colouring mesh
        {
            for (int x = 0; x < Xsize; x++)
            {
                colourMap[i] = gradient.Evaluate(noise[x, z]);
                i++;
            }
        }
        
        

        GetComponent<MeshCollider>().convex = false;
        LandscapeMesh.colors = colourMap;

        GetComponent<MeshFilter>().mesh = LandscapeMesh;
        GetComponent<MeshCollider>().sharedMesh = LandscapeMesh;
    }

    

}
