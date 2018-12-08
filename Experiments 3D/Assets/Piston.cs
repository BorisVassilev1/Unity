using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour {

    public GameObject connection_1;
    public GameObject connection_2;

    private Transform part_1;
    private Transform part_2;

    // Use this for initialization
    void Start () {
        part_1 = transform.GetChild(0);
        part_2 = transform.GetChild(1);
	}
	
	// Update is called once per frame
	void Update () {
        part_1.position = connection_1.transform.position;
        part_1.LookAt(connection_2.transform);

        part_2.position = connection_2.transform.position;
        part_2.LookAt(connection_1.transform);

        Vector3 scale = part_2.localScale;
        scale.z = Vector3.Distance(connection_1.transform.position, connection_2.transform.position) - part_1.localScale.z;
        part_2.localScale = scale;

    }
}
