using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voxelGenerator {

    public bool[,,] noiseMap;
    public int chunkSize = 33;
    public int heightLimit = 33;
    
    public bool[,,] Generate(float noiseScaleX, float noiseScaleY, float noiseScaleZ, float treshhold, int offsetX, int offsetZ)
    {
        noiseMap = new bool[chunkSize, heightLimit, chunkSize];
        bool[,,] terrain = new bool[chunkSize, heightLimit, chunkSize];
        //generating the noise map
        for (int x = 0; x < chunkSize; x++)
        {
            for(int y = 0; y < heightLimit; y ++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    noiseMap[x, y, z] = (noiseGenerator.perlin((x + offsetX) * noiseScaleX, y * noiseScaleY, (z + offsetZ) * noiseScaleZ) * y / 6
                        ) > treshhold;
                }
            }
        }
        //removing the not-seen voxels
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < heightLimit; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    
                    if (noiseMap[x, y, z] && isSeen(x, y, z))
                    {
                        terrain[x, y, z] = true;
                    }
                    else
                    {
                        terrain[x, y, z] = false;
                    }
                }
            }
        }
        return terrain;
    }
    private bool isSeen(int x, int y, int z)
    {
        bool isSeen = false;
        if (x + 1 != chunkSize)
        {
            if (!noiseMap[x + 1, y, z])
            {
                isSeen = true;
            }
        }
        if (x - 1 != -1)
        {
            if (!noiseMap[x - 1, y, z])
            {
                isSeen = true;
            }
        }
        if (y + 1 != heightLimit)
        {
            if (!noiseMap[x, y + 1, z])
            {
                isSeen = true;
            }
        }
        if (y - 1 != -1)
        {
            if (!noiseMap[x, y - 1, z])
            {
                isSeen = true;
            }
        }
        if (z + 1 != chunkSize)
        {
            if (!noiseMap[x, y, z + 1])
            {
                isSeen = true;
            }
        }
        if (z - 1 != -1)
        {
            if (!noiseMap[x, y, z - 1])
            {
                isSeen = true;
            }
        }
        return isSeen;
    }
}
