using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Targeting : MonoBehaviour {
    // Use this for initialization

    public GameObject camera;
    Vector3 camPos;
    public Vector3 offset;
    Vector3 GoalPos;
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }
        }
        GoalPos = transform.position - offset;
        camPos = camera.transform.position;
        camPos.x += (GoalPos.x - camPos.x) / 10;
        camPos.y += (GoalPos.y - camPos.y) / 10;
        camPos.z += (GoalPos.z - camPos.z) / 10;
        camera.transform.position = camPos;
    }
}
