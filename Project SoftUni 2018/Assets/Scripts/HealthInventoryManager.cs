using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthInventoryManager : MonoBehaviour {

    public int maxHealth = 30;
    public int health;

	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 4))
            {
                GameObject obj = hit.collider.gameObject;
                if(obj.CompareTag("NPC_1"))
                {
                    obj.GetComponent<NPC1_AI>().health -= 1;
                    Debug.Log("hello");
                }
            }
        }
	}
}
