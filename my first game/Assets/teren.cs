using UnityEngine;
using System.Collections;

public class teren : MonoBehaviour {
    public int xrazmer = 20;
    public int zrazmer = 20;
    public GameObject block;
    public float heightscale=20.0f;
    public float detailscale=25.0f;
    public Transform me;

    void combine(GameObject block)
    {
        MeshFilter[] meshfilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshfilters.Length];
        Destroy(this.gameObject.GetComponent<MeshCollider>());

        int i = 0;

        while (i<meshfilters.Length)
        {
            combine[i].mesh = meshfilters[i].sharedMesh;
            combine[i].transform = meshfilters[i].transform.localToWorldMatrix;
            meshfilters[i].gameObject.SetActive(false);
            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine,true);
        transform.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        transform.GetComponent<MeshFilter>().mesh.RecalculateNormals();
        transform.GetComponent<MeshFilter>().mesh.Optimize();

        this.gameObject.AddComponent<MeshCollider>();
        transform.gameObject.SetActive(true);

        Destroy(block);
    }

    // Use this for initialization
    void Start () {
        for(int x = 0; x < xrazmer; x++ )
        {
            for(int z = 0; z < zrazmer; z++ )
            {
                int y = (int) (Mathf.PerlinNoise(x/detailscale,z/detailscale)* Mathf.Pow( heightscale,1.0f));
                
                GameObject newblock = null;
                for (int i=y;i>=0;i--)
                {
                    Vector3 blockpos = new Vector3(x, y, z);
                    newblock = (GameObject) Instantiate(block, blockpos, Quaternion.identity);
                    newblock.transform.SetParent(me);
                }
                combine(newblock);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
