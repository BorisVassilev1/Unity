using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject NMAgent;
    public GameObject NMAgentFat;
    public GameObject WaypointAgent;
    public GameObject BTAgent;
    public GameObject FSMAgent;

	void Start () {
        GameObject current;
		for(int i = 0; i < 6; i ++)
        {
            current = Instantiate(NMAgent, transform);
            current.transform.position = new Vector3(Random.value * 50 - 25, 2, Random.value * 50 - 25);
        }

        for (int i = 0; i < 6; i++)
        {
            current = Instantiate(NMAgentFat, transform);
            current.transform.position = new Vector3(Random.value * 50 - 25, 2, Random.value * 50 - 25);
        }

        for (int i = 0; i < 6; i++)
        {
            current = Instantiate(WaypointAgent, transform);
            current.transform.position = new Vector3(Random.value * 50 - 25, 2, Random.value * 50 - 25);
        }

        for (int i = 0; i < 6; i++)
        {
            current = Instantiate(BTAgent, transform);
            current.transform.position = new Vector3(Random.value * 50 - 25, 1, Random.value * 50 - 25);
        }

        for (int i = 0; i < 4; i++)
        {
            current = Instantiate(FSMAgent, transform);
            current.transform.position = new Vector3(Random.value * 50 - 25, 2, Random.value * 50 - 25);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
