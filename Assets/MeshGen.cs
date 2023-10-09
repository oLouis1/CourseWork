using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MeshGen
{
    public static Mesh mesh;
    public static int[] triangles;
    public static Vector3[] verticies;

  //  public static float lacunarity, persistance;
   // private static int X, Z, octaves;

    public static Mesh generateMesh(int Xsize, int Zsize, float[,] noise,float heightScale)
    {
        mesh = new Mesh();
        verticies = new Vector3[(Xsize) * (Zsize)];
        triangles = new int[(Xsize - 1) * (Zsize - 1) * 6];



        //--------------------Vertex Generation--------------------
        
        int i = 0;
        for (int z = 0; z < Zsize; z++)
        {
            for (int x = 0; x < Xsize; x++)
            {
                if(noise[x,z] <= 0.3f)//this is so everything below a certain hieght will be flat, to represent water.
                {
                    noise[x, z] = 0.3f;
                }
                else if (noise[x, z] <= 0.5f)
                {
                    noise[x, z] = 0.5f;
                }
                else if (noise[x, z] <= 0.65f)
                {
                    noise[x, z] = 0.65f;
                }
                else if (noise[x, z] < 0.75f)
                {
                    noise[x, z] = 0.7f;
                }
                if (noise[x,z] >= 0.75f)
                {
                    noise[x, z] = 0.75f;
                }
                verticies[i] = new Vector3(x, noise[x, z]*heightScale, z); //creates each vertex for the mesh giving it the current x and z value and the random y noise height
                i++;
            }
        }


        //--------------------Triangle Generation----------------
        int trianglePart = 0, vertex = 0;
        for (int z = 0; z < Zsize - 1; z++)         
        {
            for (int x = 0; x < Xsize - 1; x++)     //loops for making each triangle required for the mesh
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

        //-----------Creating Mesh-----------
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

   

    
}
