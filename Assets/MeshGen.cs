using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour

{
    Mesh mesh;
    Vector3[] vertices; //a vector for each point along the mesh, describes its x and z location and its height/ y location
    int[] triangles;    //the triangles needed for the mesh

    public int Xsize=3;   //width and length for the mesh
    public int Zsize=3;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        makeMesh();
        clearMesh();

    }
    void Update()
    {
       // clearMesh();
    }

    void makeMesh()
    {
        vertices = new Vector3[(Xsize) * (Zsize)];
        triangles = new int[(Xsize-1) * (Zsize-1) * 6];

        for (int i = 0,z=0; z < Zsize; z++)     //loops through z and x, creating the vector for each vertex
        {
            for (int x = 0; x < Xsize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;  //create the psudo random height for each vertex
                vertices[i] = new Vector3(x, 0, z);
                Debug.Log(z);
                i++;
            }
        }
        int trianglePart = 0, vertex =0;
        for (int z=0; z<Zsize-1; z++)
        {
            for (int x = 0; x < Xsize-1; x++)
            {
                triangles[trianglePart + 0] = vertex;           //loops for making triangles for the mesh
                triangles[trianglePart + 1] = vertex + Xsize+1;
                triangles[trianglePart + 2] = vertex + 1;
                triangles[trianglePart + 3] = vertex + 1;
                triangles[trianglePart + 4] = vertex + Xsize+1;
                triangles[trianglePart + 5] = vertex + Xsize+2;
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
