using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGen
{

    public static float[,] Noise;
    public static float[,] generateNoise(int octaves, float persitance, float lacunarity, int xSize, int zSize){

        float[,] Noise = new float[xSize,zSize]; 
        float amplitude=1;
        float frequency=1;
        float y=0;

        for (int z=0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++) //loop for x and z values 
            {

                amplitude = 1;         
                frequency = 1;
                y = 0;

                for (int i = 0; i < octaves; i++)   // loops to create each octave
                {
                    float Xsample = x * frequency;
                    float Zsample = z * frequency;
     
                   
                    float Ysample = (Mathf.PerlinNoise(Xsample, Zsample)-0.5f) ;
                    Debug.Log(Ysample);
                    y += Ysample;
                    
                    amplitude *= persitance;
                    frequency *= lacunarity;
                }
                Noise[x, z] = y;
            } 
        }
        return Noise;


    }

}