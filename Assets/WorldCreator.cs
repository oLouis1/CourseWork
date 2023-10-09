using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WorldCreator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices; //a vector for each point along the mesh, describes its x and z location and its height/ y location
    int[] triangles;    //the triangles needed for the mesh

    public int Xsize = 10;   //width and length for the mesh
    public int Zsize = 10;
    public float worldLacunarity = 1.5f;
    public float worldPersistance = 0.5f;
    public int worldOctaves = 3;
    public float zoom;
    public float heightScale;
    public Renderer textureRenderer;

    // Start is called before the first frame update
    void Start()
    {

        makeMap();
        drawNoise(NoiseGen.generateNoise(Xsize, Zsize, worldLacunarity, worldPersistance, worldOctaves, zoom));


    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            //drawNoise(NoiseGen.generateNoise(Xsize, Zsize, worldLacunarity, worldPersistance, worldOctaves, zoom));
            Debug.Log("Test");
            makeMap();
        }
    }

    void drawNoise(float[,] Noise)
    {
        int Xsize = Noise.GetLength(0);
        int Zsize = Noise.GetLength(1);

        Texture2D texture = new Texture2D(Xsize, Zsize);

        Color[] colourMap = new Color[Xsize * Zsize];
        for (int z = 0; z < Zsize; z++)
        {
            for (int x = 0; x < Xsize; x++)
            {
                colourMap[z * Xsize + x] = Color.Lerp(Color.black, Color.white, Noise[x, z]);
            }
        }
        texture.SetPixels(colourMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(Xsize, 1, Zsize);

    }
    void makeMap()
    {
        mesh = new Mesh();
        float[,] noise = NoiseGen.generateNoise(Xsize, Zsize, worldLacunarity, worldPersistance, worldOctaves, zoom);
        mesh = MeshGen.generateMesh(Xsize, Zsize, noise, heightScale);
        transform.localScale = new Vector3(Xsize/10, 1, Zsize/10);

        Texture2D texture = new Texture2D(Xsize, Zsize);

        Color[] colourMap = new Color[Xsize * Zsize];
        for (int z = 0; z < Zsize; z++)
        {
            for (int x = 0; x < Xsize; x++)
            {
                if (noise[x, z] <= 0.3f)//this is so everything below a certain hieght will be flat, to represent water.
                {
                    colourMap[z * Xsize + x] = Color.blue;
                }
                else if (0.3f < noise[x,z] && noise[x, z] <= 0.4f)
                {
                    colourMap[z * Xsize + x] = Color.yellow;
                }
                else if (0.4f < noise[x,z] && noise[x, z] <= 0.65f)
                {
                    colourMap[z * Xsize + x] = Color.green;
                }
            }
        }
        texture.SetPixels(colourMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(Xsize/10, 1, Zsize/10);

        GetComponent<MeshCollider>().convex = false;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    

}
