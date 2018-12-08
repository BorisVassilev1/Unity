using UnityEngine;
using System.Collections;

public class raketa : MonoBehaviour {
    public float force;
	// Use this for initialization
	void Start () {
	
	}
    public Rigidbody korab;
	// Update is called once per frame
	void Update () {
       korab.AddForceAtPosition(new Vector3(0f,force,0f),transform.position);
        var x = Input.GetAxis("Horizontal") * 0.1f;
        var z = Input.GetAxis("Vertical") * 0.1f;
    }
}
