using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject camera;
    public GameObject bullet;
    public Transform bulletTransform;
    public int minimapSize = 256;
    private RaycastHit hit;
    public Vector3 offset;
    private Vector3 goalPos;
    private Vector3 camPos;

    public RectTransform minimapTr;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        camera = Camera.main.gameObject;
        camPos = camera.transform.position;
        
        minimapTr.anchorMax = new Vector2(1,1);
        minimapTr.anchorMin = new Vector2((Screen.width - minimapSize) / (float)Screen.width, (Screen.height - minimapSize) / (float)Screen.height);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.mousePosition.x < Screen.width - minimapSize || Input.mousePosition.y < Screen.height - minimapSize)
            {
                Debug.Log("raycasting!");
                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                    bulletTransform.LookAt(new Vector3(hit.point.x, 1, hit.point.z));
                }
            }
            else
            {
                Debug.Log("minimaping!!");
                Vector2 pos = new Vector2(Input.mousePosition.x - (Screen.width - minimapSize), Input.mousePosition.y - (Screen.height - minimapSize));
                Debug.Log(pos);
                Debug.Log(Input.mousePosition.y);
                agent.SetDestination(new Vector3(pos.x / (minimapSize / 2) * 25, 0, pos.y / (minimapSize / 2) * 25));
            }
        }
        goalPos = this.transform.position + offset;
        camPos.x += (- camPos.x + goalPos.x) / 10;
        camPos.y += (- camPos.y + goalPos.y) / 10;
        camPos.z += (- camPos.z + goalPos.z) / 10;
        camera.transform.position = camPos;

        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                bulletTransform.LookAt(new Vector3(hit.point.x, 1, hit.point.z));
            }

            Debug.Log("henlo");
            GameObject clone = Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
            clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * 10;
        }
    }
}
