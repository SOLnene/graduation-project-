using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth,int mapHeight,int seed, float scale,int octaves,float persistance,float lacunarity,Vector2 offset)
    {
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000)+offset.x;
            float offsetY = prng.Next(-100000, 100000)+offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float halfwidth = mapWidth / 2f;
        float halfheight = mapHeight / 2f;

        float[,] NoiseMap = new float[mapWidth, mapHeight];

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int y = 0; y < mapHeight; y++){
            for (int x = 0; x < mapWidth; x++){

                float amplitude = 1;
                float frequecy = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++){
                    float sampleX = (x-halfwidth) / scale *frequecy+octaveOffsets[i].x;
                    float sampleY = (y-halfheight) / scale * frequecy+octaveOffsets[i].y;
                    float perlinvalue = Mathf.PerlinNoise(sampleX, sampleY)*2-1;
                    noiseHeight += perlinvalue * amplitude;

                    amplitude *= persistance;
                    frequecy *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight){
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                NoiseMap[x, y] = noiseHeight;
            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                NoiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, NoiseMap[x, y]);
            }
        }
            return NoiseMap;
    }
}
