using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour

{
    Mesh mesh;
    Vector3[] vertices; //a vector for each point along the mesh, describes its x and z location and its height/ y location
    int[] triangles;    //the triangles needed for the mesh

    public int Xsize=10;   //width and length for the mesh
    public int Zsize=10;
    public float lacunarity=1.5f;
    public float persistance=0.5f;
    public int octaves=3;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();


        GetComponent<MeshCollider>().convex = false;
        makeMesh();
        clearMesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        

    }

    void makeMesh()
    {
        vertices = new Vector3[(Xsize) * (Zsize)];
        triangles = new int[(Xsize - 1) * (Zsize - 1) * 6];

        float[,] NoiseMap = NoiseGen.generateNoise(octaves, persistance, lacunarity, Xsize, Zsize); 

        for (int i = 0, z = 0; z < Zsize; z++)     //loops through z and x, creating the vector for each vertex
        {
            for (int x = 0; x < Xsize; x++)
            {
                //create the psudo random height for each vertex
                
                vertices[i] = new Vector3(x, NoiseMap[x,z], z);
                i++;
            }
        }
        int trianglePart = 0, vertex = 0;
        for (int z = 0; z < Zsize - 1; z++)         //loops for making each triangle required for the mesh
        {
            for (int x = 0; x < Xsize - 1; x++)
            {
                triangles[trianglePart + 0] = vertex;   
                triangles[trianglePart + 1] = vertex + Xsize;
                triangles[trianglePart + 2] = vertex + 1;
                triangles[trianglePart + 3] = vertex + 1;
                triangles[trianglePart + 4] = vertex + Xsize;
                triangles[trianglePart + 5] = vertex + Xsize + 1;
                vertex++;
                trianglePart += 6;
            }
            vertex++;
        }
    }


    void clearMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
    
}
