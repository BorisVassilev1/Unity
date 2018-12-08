using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pathTracker : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject agentGO;
    void Start()
    {
        agent = agentGO.GetComponent<NavMeshAgent>();
    }
	
	void Update () {

	}
    public void OnPostRender()
    {
        if (agent.hasPath)
        {
            /*Debug.Log(
            agent.path.corners.Length
            );*/
            GL.Begin(GL.LINE_STRIP);
            GL.Color(Color.white);
            GL.Vertex(agent.path.corners[0]);
            for (int i = 1; i < agent.path.corners.Length; i++)
            {
                //Debug.DrawLine(agent.path.corners[i - 1], agent.path.corners[i]);
                GL.Vertex(agent.path.corners[i]);
            }
            GL.End();
        }
    }

    /*public void OnDrawGizmos()
    {
        if (agent.hasPath)
        {
            
            GL.Begin(GL.LINE_STRIP);
            GL.Color(Color.white);
            GL.Vertex(agent.path.corners[0]);
            for (int i = 1; i < agent.path.corners.Length; i++)
            {
                //Debug.DrawLine(agent.path.corners[i - 1], agent.path.corners[i]);
                GL.Vertex(agent.path.corners[i]);
            }
            GL.End();
        }
    }*/
}
