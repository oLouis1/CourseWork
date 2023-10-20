using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WorldCreator : MonoBehaviour
{
    Mesh mesh;


    public int Xsize = 10;   //width and length for the mesh
    public int Zsize = 10;
    public float worldLacunarity = 1.5f;
    public float worldPersistance = 0.5f;
    public int worldOctaves = 3;
    public float zoom;
    public float heightScale;
    public Color[] colourMap;
    public AnimationCurve smoothHeightCurve;
    public float seed;
    public Gradient gradient;



    // Start is called before the first frame update
    void Start()
    {

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
        mesh = new Mesh();
        seed = Random.Range(0,100000);
        float[,] noise = NoiseGen.generateNoise(Xsize, Zsize, worldLacunarity, worldPersistance, worldOctaves, zoom, seed);   //creates noise map
        

        mesh = MeshGen.generateMesh(Xsize, Zsize, noise, heightScale, smoothHeightCurve);  // creates mesh for terrain
        
       // transform.localScale = new Vector3(Xsize, 1, Zsize);
        
        


       
        
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
        mesh.colors = colourMap;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    

}
