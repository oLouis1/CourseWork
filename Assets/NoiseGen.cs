using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGen
{

    public static float[,] Noise;
    public static float[,] generateNoise(int xSize, int zSize, float lacunarity, float persistance, int octaves, float zoom)
    {

        float[,] Noise = new float[xSize,zSize]; 
        float amplitude=1;
        float frequency=1;
        float y=0;


        float maxVal = float.MinValue;
        float minVal = float.MaxValue;  //for tracking the max and min values so noise can be normalised.

        for (int z=0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++) //loop for x and z values 
            {

                amplitude = 1;         
                frequency = 1;
                y = 0;

                for (int i = 0; i < octaves; i++)   // loops to create each octave
                {
                    
                    float Xsample = x/ zoom * frequency;
                    float Zsample = z/ zoom * frequency;
                    
                    
                    float Ysample = (Mathf.PerlinNoise(Xsample, Zsample)-0.5f);
                    y += Ysample * amplitude;
                    
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if(y > maxVal)      //whenver theres a value smaller or greater than the min and max value it sets either min or max to new value
                {
                    maxVal = y;
                }
                else if(y < minVal)
                {
                    minVal = y;
                }


                Noise[x, z] = y;
            } 
        }
        for (int z=0;z<zSize; z++)      //normalises the noise so all values are between the min a max
        {
            for (int x = 0; x < xSize; x++)
            {
                Noise[x, z] = Mathf.InverseLerp(minVal, maxVal, Noise[x,z]);
            }
        }

        return Noise;


    }

}