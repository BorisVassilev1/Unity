using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGenerator : MonoBehaviour {

    public GameObject chunk;
    private voxelGenerator voxGen;
    private void Awake()
    {
        voxGen = new voxelGenerator();
        for(int x = 0; x < 4; x ++)
        {
            for(int z = 0; z < 4; z ++)
            {
                GameObject curr = Instantiate(chunk, new Vector3(x * (voxGen.chunkSize - 1), 0, z * (voxGen.chunkSize - 1)), Quaternion.identity,this.transform);
                curr.name = "chunk" + x + " " + z;
            }
        }
    }
}
