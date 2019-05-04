using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsMap : MonoBehaviour
{
    public GameObject DebugCube;
    public List<Wall> walls;
    public List<Vector3> GraphVertices;

    public List<int>[] graph;

    public class Wall
    {
        public Vector3 v1;
        public Vector3 v2;
        public Wall(Vector3 _v1, Vector3 _v2)
        {
            this.v1 = _v1;
            this.v2 = _v2;
        }

        public bool intersects(Wall other)
        {
            Vector3 a = this.v1, b = this.v2, c = other.v1, d = other.v2;
            float o1 = orientedFace(a, b, c),
            o2 = orientedFace(a, b, d),
            o3 = orientedFace(c, d, a),
            o4 = orientedFace(c, d, b);
            if (o1 * o2 < 0 && o3 * o4 < 0) return true;
            if (o1 == 0 && (c.x - a.x) * (c.x - b.x) <= 0 && (c.z - a.z) * (c.z - b.z) <= 0) return true;
            if (o2 == 0 && (d.x - a.x) * (d.x - b.x) <= 0 && (d.z - a.z) * (d.z - b.z) <= 0) return true;
            if (o3 == 0 && (a.x - c.x) * (a.x - d.x) <= 0 && (a.z - c.z) * (a.z - d.z) <= 0) return true;
            if (o3 == 0 && (b.x - c.x) * (b.x - d.x) <= 0 && (b.z - c.z) * (b.z - d.z) <= 0) return true;
            return false;
        }

        public Vector3 getVector()
        {
            return (this.v2 - this.v1).normalized;
        }

        private float orientedFace(Vector3 a, Vector3 b, Vector3 c)
        {
            return a.x * b.z + a.z * c.x + b.x * c.z - b.z * c.x - a.x * c.z - a.z * b.x;
        }
    }

    void Awake()
    {
        walls = new List<Wall>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            if (child.CompareTag("Wall"))
            {
                Wall w = new Wall(child.GetChild(0).transform.position, child.GetChild(1).transform.position);
                walls.Add(w);
            }
        }
        GraphVertices = new List<Vector3>();
        foreach (Wall w in walls)
        {
            GameObject currCube;
            Vector3 pos;

            currCube = Instantiate(DebugCube);
            pos = (w.v1 - w.getVector());
            GraphVertices.Add(pos);
            currCube.transform.position = pos;

            currCube = Instantiate(DebugCube);
            pos = (w.v2 + w.getVector());
            GraphVertices.Add(pos);
            currCube.transform.position = pos;
        }

        graph = new List<int>[GraphVertices.Count];
        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = new List<int>();
        }
        for (int i = 0; i < GraphVertices.Count; i++)
        {
            for (int j = 0; j < GraphVertices.Count; j++)
            {
                if (i != j)
                {
                    Wall currentEdge = new Wall(GraphVertices[i], GraphVertices[j]);
                    foreach (Wall w in walls)
                    {
                        if (!w.intersects(currentEdge))
                        {
                            graph[i].Add(j);
                        }
                    }
                }
            }
        }

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
}
