using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour

{
    Mesh mesh;
    Vector3[] vertices; //a vector for each point along the mesh, describes its x and z location and its height/ y location
    int[] triangles;    //the triangles needed for the mesh

    public int Xsize=20;   //width and length for the mesh
    public int Zsize=20;

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
        clearMesh();
    }

    void makeMesh()
    {
        vertices = new Vector3[(Xsize+1) * (Zsize+1)];
        triangles = new int[((Xsize) * (Zsize) * 6)];

        for (int i = 0,z=0; z < Zsize; z++)     //loops through z and x, creating the vector for each vertex
        {
            for (int x = 0; x < Xsize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;  //create the psudo random height for each vertex
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
        
        for (int tris = 0, vertex= 0, z= 0; z < Zsize; z++)
        {
            for (int x = 0; x < Xsize; x++)
            {
                triangles[tris + 0] = vertex + 0;       //creates the triangles looping through x and z again
                triangles[tris + 1] = vertex + Xsize+1;
                triangles[tris + 2] = vertex + 1;
                triangles[tris + 3] = vertex + 1;
                triangles[tris + 4] = vertex + Xsize + 1;
                triangles[tris + 5] = vertex + Xsize + 2;
                tris += 6;
                vertex++;
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
    // Update is called once per frame
    
}
