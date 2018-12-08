using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class WaypointAgent : MonoBehaviour {

    private Transform target;
    private Transform player;
    private PlayerManager PM;
    public float TurningSpeed = 4;
    public float Speed = 4;
    public int MaxHealth = 10;
    private int health;
    public bool willBeHit = false;
    public float sightDistance = 10;
    public GameObject deathEffect;

    public Material material;

	// Use this for initialization
	void Start () {
        target = transform.GetChild(0);
        GetComponent<WaypointProgressTracker>().circuit = GameObject.FindGameObjectWithTag("WaypointCircuit").GetComponent<WaypointCircuit>();
        health = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("Attack", 1.5f, 1.5f);
        PM = player.gameObject.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(transform.position, target.position) > 2 || target.position != player.position)
        {
        Vector3 lookAtTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 dir = lookAtTarget - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), TurningSpeed);
        transform.Translate(0, 0, Speed * Time.deltaTime * 0.5f);
        }

        if (Vector3.Distance(player.position, transform.position) <= sightDistance)
        {
            target = player;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
            GameObject go = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(go, 2);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Manager>().KilledMonsters += 1;
        }
    }

    public void dealDamage(int dmg)
    {
        health -= dmg;
    }
        

    private void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < 2.5)
            PM.dealDamage(1);
    }
}
