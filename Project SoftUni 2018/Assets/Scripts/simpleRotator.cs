using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleRotator : MonoBehaviour {

    public Vector3 rotation;

	void Start () {
		
	}
	
	void Update () {
        this.transform.Rotate(rotation.x * Time.deltaTime, rotation.y * Time.deltaTime, rotation.z * Time.deltaTime);
	}
}
