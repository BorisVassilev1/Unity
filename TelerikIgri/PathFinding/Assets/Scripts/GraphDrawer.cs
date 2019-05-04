using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDrawer : MonoBehaviour
{
    public WallsMap map;

    public void OnPostRender()
    {
        renderLines();
    }
    public void OnDrawGizmos()
    {
        if(Application.isPlaying)
        renderLines();
    }

    void renderLines()
    {
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        for (int i = 0; i < map.graph.Length; i++)
        {
            for (int j = 0; j < map.graph[i].Count; j++)
            {
                GL.Vertex(map.GraphVertices[i]);
                GL.Vertex(map.GraphVertices[map.graph[i][j]]);
            }
        }
        GL.End();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
