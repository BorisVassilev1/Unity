using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class generator : MonoBehaviour {
    private Vector3[] vertices;
    public int xSize=0, ySize=0;
    private Mesh mesh;
    public float detailscale,heightscale=0;
    private float seed;
    public ArrayList terrain = new ArrayList();

    private void Awake()
    {
        seed = Random.Range(0f, 1000f);
        Generate();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, (Mathf.PerlinNoise(x / detailscale + seed, y / detailscale + seed)+ 0.25f*Mathf.PerlinNoise(x / detailscale*4 + seed, y / detailscale*4 + seed) + 0.0625f * Mathf.PerlinNoise(x / detailscale * 16 + seed, y / detailscale * 16 + seed)) * heightscale, y);
                

                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
