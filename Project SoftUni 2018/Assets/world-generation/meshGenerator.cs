using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class meshGenerator : MonoBehaviour
{

    public int xSize, ySize;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uvs;
    private Vector3[] normals;
    private voxelGenerator voxGen;
    public GameObject cube;
    private List<Vector3> triangles;
    public bool[,,] terrain;
    //private List<Vector3> filteredTriangles;

    private void Awake()
    {
        voxGen = new voxelGenerator();
        terrain = voxGen.Generate(0.09f, 0.09f, 0.09f, 0.5f, (int)(this.transform.position.x), (int)(this.transform.position.z));
        triangles = new List<Vector3>();
        vertices = new Vector3[voxGen.chunkSize * voxGen.heightLimit * voxGen.chunkSize];
        uvs = new Vector2[voxGen.chunkSize * voxGen.heightLimit * voxGen.chunkSize];
        normals = new Vector3[voxGen.chunkSize * voxGen.heightLimit * voxGen.chunkSize];
        calcMesh();
    }


    private List<Vector3[]> calcVoxelTris(int x, int y, int z)
    {
        List<Vector3> PList = new List<Vector3>();
          if (x + 1 != voxGen.chunkSize  ) { if (terrain[x + 1, y    , z    ]) { line(x, y, z, x + 1, y    , z    ); PList.Add(new Vector3(x + 1, y    , z    )); } }
        //if (x - 1 != -1                ) { if (voxGen.terrain[x - 1, y    , z    ]) { line(x, y, z, x - 1, y    , z    ); PList.Add(new Vector3(x - 1, y    , z    )); } }
          if (y + 1 != voxGen.heightLimit) { if (terrain[x    , y + 1, z    ]) { line(x, y, z, x    , y + 1, z    ); PList.Add(new Vector3(x    , y + 1, z    )); } }
        //if (y - 1 != -1                ) { if (voxGen.terrain[x    , y - 1, z    ]) { line(x, y, z, x    , y - 1, z    ); PList.Add(new Vector3(x    , y - 1, z    )); } }
          if (z + 1 != voxGen.chunkSize  ) { if (terrain[x    , y    , z + 1]) { line(x, y, z, x    , y    , z + 1); PList.Add(new Vector3(x    , y    , z + 1)); } }
        //if (z - 1 != -1                ) { if (voxGen.terrain[x    , y    , z - 1]) { line(x, y, z, x    , y    , z - 1); PList.Add(new Vector3(x    , y    , z - 1)); } }

          if (x + 1 != voxGen.chunkSize   && y + 1 != voxGen.heightLimit) { if (terrain[x + 1, y + 1, z    ]) { line(x, y, z, x + 1, y + 1, z    ); PList.Add(new Vector3(x + 1, y + 1, z    )); } }
        //if (x + 1 != voxGen.chunkSize   && y - 1 != -1                ) { if (voxGen.terrain[x + 1, y - 1, z    ]) { line(x, y, z, x + 1, y - 1, z    ); PList.Add(new Vector3(x + 1, y - 1, z    )); } }
          if (x - 1 != -1                 && y + 1 != voxGen.heightLimit) { if (terrain[x - 1, y + 1, z    ]) { line(x, y, z, x - 1, y + 1, z    ); PList.Add(new Vector3(x - 1, y + 1, z    )); } }
        //if (x - 1 != -1                 && y - 1 != -1                ) { if (voxGen.terrain[x - 1, y - 1, z    ]) { line(x, y, z, x - 1, y - 1, z    ); PList.Add(new Vector3(x - 1, y - 1, z    )); } }
          if (x + 1 != voxGen.chunkSize   && z + 1 != voxGen.chunkSize  ) { if (terrain[x + 1, y    , z + 1]) { line(x, y, z, x + 1, y    , z + 1); PList.Add(new Vector3(x + 1, y    , z + 1)); } }
        //if (x + 1 != voxGen.chunkSize   && z - 1 != -1                ) { if (voxGen.terrain[x + 1, y    , z - 1]) { line(x, y, z, x + 1, y    , z - 1); PList.Add(new Vector3(x + 1, y    , z - 1)); } }
          if (x - 1 != -1                 && z + 1 != voxGen.chunkSize  ) { if (terrain[x - 1, y    , z + 1]) { line(x, y, z, x - 1, y    , z + 1); PList.Add(new Vector3(x - 1, y    , z + 1)); } }
        //if (x - 1 != -1                 && z - 1 != -1                ) { if (voxGen.terrain[x - 1, y    , z - 1]) { line(x, y, z, x - 1, y    , z - 1); PList.Add(new Vector3(x - 1, y    , z - 1)); } }
          if (y + 1 != voxGen.heightLimit && z + 1 != voxGen.chunkSize  ) { if (terrain[x    , y + 1, z + 1]) { line(x, y, z, x    , y + 1, z + 1); PList.Add(new Vector3(x    , y + 1, z + 1)); } }
        //if (y + 1 != voxGen.heightLimit && z - 1 != -1                ) { if (voxGen.terrain[x    , y + 1, z - 1]) { line(x, y, z, x    , y + 1, z - 1); PList.Add(new Vector3(x    , y + 1, z - 1)); } }
          if (y - 1 != -1                 && z + 1 != voxGen.chunkSize  ) { if (terrain[x    , y - 1, z + 1]) { line(x, y, z, x    , y - 1, z + 1); PList.Add(new Vector3(x    , y - 1, z + 1)); } }
        //if (y - 1 != -1                 && z - 1 != -1                ) { if (voxGen.terrain[x    , y - 1, z - 1]) { line(x, y, z, x    , y - 1, z - 1); PList.Add(new Vector3(x    , y - 1, z - 1)); } }

          if (x + 1 != voxGen.chunkSize && y + 1 != voxGen.heightLimit && z + 1 != voxGen.chunkSize) { if (terrain[x + 1, y + 1, z + 1]) { line(x, y, z, x + 1, y + 1, z + 1); PList.Add(new Vector3(x + 1, y + 1, z + 1)); } }
        //if (x + 1 != voxGen.chunkSize && y + 1 != voxGen.heightLimit && z - 1 != -1              ) { if (voxGen.terrain[x + 1, y + 1, z - 1]) { line(x, y, z, x + 1, y + 1, z - 1); PList.Add(new Vector3(x + 1, y + 1, z - 1)); } }
          if (x + 1 != voxGen.chunkSize && y - 1 != -1                 && z + 1 != voxGen.chunkSize) { if (terrain[x + 1, y - 1, z + 1]) { line(x, y, z, x + 1, y - 1, z + 1); PList.Add(new Vector3(x + 1, y - 1, z + 1)); } }
        //if (x + 1 != voxGen.chunkSize && y - 1 != -1                 && z - 1 != -1              ) { if (voxGen.terrain[x + 1, y - 1, z - 1]) { line(x, y, z, x + 1, y - 1, z - 1); PList.Add(new Vector3(x + 1, y - 1, z - 1)); } }
          if (x - 1 != -1               && y + 1 != voxGen.heightLimit && z + 1 != voxGen.chunkSize) { if (terrain[x - 1, y + 1, z + 1]) { line(x, y, z, x - 1, y + 1, z + 1); PList.Add(new Vector3(x - 1, y + 1, z + 1)); } }
        //if (x - 1 != -1               && y + 1 != voxGen.heightLimit && z - 1 != -1              ) { if (voxGen.terrain[x - 1, y + 1, z - 1]) { line(x, y, z, x - 1, y + 1, z - 1); PList.Add(new Vector3(x - 1, y + 1, z - 1)); } }
          if (x - 1 != -1               && y - 1 != -1                 && z + 1 != voxGen.chunkSize) { if (terrain[x - 1, y - 1, z + 1]) { line(x, y, z, x - 1, y - 1, z + 1); PList.Add(new Vector3(x - 1, y - 1, z + 1)); } }
        //if (x - 1 != -1               && y - 1 != -1                 && z - 1 != -1              ) { if (voxGen.terrain[x - 1, y - 1, z - 1]) { line(x, y, z, x - 1, y - 1, z - 1); PList.Add(new Vector3(x - 1, y - 1, z - 1)); } }

        List<Vector3[]> list = new List<Vector3[]>();

        for (int i = 0; i < PList.Count; i++)
        {
            for (int j = 0; j < PList.Count; j++)
            {
                if (areAdjacent(PList[i], PList[j]))
                {
                    list.Add(new Vector3[3]
                    {
                        new Vector3(x,y,z),
                        PList[i],
                        PList[j]
                    });
                }
            }
        }

        return list;
    }

    private void calcMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Chunk " + this.transform.position.x / voxGen.chunkSize + " " + this.transform.position.z / voxGen.chunkSize;

        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int x = 0; x < voxGen.chunkSize; x++)
        {
            for (int y = 0; y < voxGen.heightLimit; y++)
            {
                for (int z = 0; z < voxGen.chunkSize; z++)
                {
                    vertices[coordsToID(new Vector3(x, y, z))] = new Vector3(x, y, z);
                    uvs[coordsToID(new Vector3(x, y, z))] = new Vector2(x + y , z + y);
                    if (terrain[x, y, z])
                    {
                        tangents[coordsToID(new Vector3(x, y, z))] = tangent;
                        List<Vector3[]> currTris = calcVoxelTris(x, y, z);
                        normals[coordsToID(new Vector3(x, y, z))] = Vector3.up;
                        foreach (Vector3[] t in currTris)
                        {
                            triangles.Add(new Vector3(coordsToID(t[0]), coordsToID(t[1]), coordsToID(t[2])));
                        }

                    }
                }
            }
        }
        {
        //    filteredTriangles = new List<Vector3>(triangles.Count);

        //    for (int i = 0; i < triangles.Count; i++)
        //    {
        //        if (!(filteredTriangles.Contains(new Vector3(triangles[i].x, triangles[i].y, triangles[i].z)) ||
        //             filteredTriangles.Contains(new Vector3(triangles[i].x, triangles[i].z, triangles[i].y)) ||
        //             filteredTriangles.Contains(new Vector3(triangles[i].y, triangles[i].x, triangles[i].z)) ||
        //             filteredTriangles.Contains(new Vector3(triangles[i].y, triangles[i].z, triangles[i].x)) ||
        //             filteredTriangles.Contains(new Vector3(triangles[i].z, triangles[i].x, triangles[i].y)) ||
        //             filteredTriangles.Contains(new Vector3(triangles[i].z, triangles[i].y, triangles[i].x))))
        //        {
        //            filteredTriangles.Add(triangles[i]);
        //        }
        //    }
        }

        int[] tri = new int[triangles.Count * 3];
        for (int i = 0; i < triangles.Count; i++)
        {
            tri[i * 3] = (int)(triangles[i].x);
            tri[i * 3 + 1] = (int)(triangles[i].y);
            tri[i * 3 + 2] = (int)(triangles[i].z);
        }

        mesh.vertices = vertices;
        //mesh.tangents = tangents;
        mesh.uv = uvs;
        mesh.triangles = tri;
        mesh.normals = normals;
        //mesh.RecalculateNormals();

        //foreach (var item in mesh.normals)
        //{
        //    Debug.Log(item);
        //}

        //mesh.RecalculateTangents();

        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void line(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        Debug.DrawRay(new Vector3(x1, y1, z1), new Vector3(x2 - x1, y2 - y1, z2 - z1));
    }

    private bool areAdjacent(Vector3 v1, Vector3 v2)
    {
        return (v1.x == v2.x + 1 || v1.x == v2.x - 1 || v1.x == v2.x) && (v1.y == v2.y + 1 || v1.y == v2.y - 1 || v1.y == v2.y) && (v1.z == v2.z + 1 || v1.z == v2.z - 1 || v1.z == v2.z) &&
            !(v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
    }

    private int coordsToID(Vector3 a)
    {
        return (int)(a.y) * voxGen.chunkSize + (int)(a.x) + (int)(a.z) * voxGen.chunkSize * voxGen.heightLimit;
    }
}