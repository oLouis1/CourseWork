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

    public static Mesh generateMesh(int Xsize, int Zsize, float lacunarity, float persistance, int octaves)
    {
        mesh = new Mesh();
        verticies = new Vector3[(Xsize) * (Zsize)];
        triangles = new int[(Xsize - 1) * (Zsize - 1) * 6];



        //--------------------Vertex Generation--------------------
        float[,] Noise = NoiseGen.generateNoise(octaves, persistance, lacunarity, Xsize, Zsize);
        int i = 0;
        for (int z = 0; z < Zsize; z++)
        {
            for (int x = 0; x < Xsize; x++)
            {
                verticies[i] = new Vector3(x, Noise[x, z], z); //creates each vertex for the mesh giving it the current x and z value and the random y noise height
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
