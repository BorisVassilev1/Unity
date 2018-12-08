using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NMAgent : MonoBehaviour {

    private GameObject player;
    private bool isChasing = false;
    private bool needsToRenewTarget = false;
    private float secondCounter = 0f;
    public float sightDistance = 5f;
    private NavMeshAgent NMA;
    public GameObject deathEffect;
    private PlayerManager PM;

    public bool willBeHit = false;

    public int maxHealth = 10;
    private int health;

	// Use this for initialization
	void Start () {
        NMA = this.GetComponent<NavMeshAgent>();
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        PM = player.GetComponent<PlayerManager>();
        InvokeRepeating("Attack", 1.5f, 1.5f);
    }
	
	// Update is called once per frame
	void Update () {
        secondCounter += Time.deltaTime;

        if(secondCounter >= 1f)
        {
            secondCounter = 0f;
            needsToRenewTarget = true;
        }

        if (needsToRenewTarget && secondCounter == 0f && isChasing)
        {
            NMA.SetDestination(player.transform.position);
            needsToRenewTarget = false;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= sightDistance)
        {
            if(!isChasing)
            {
                isChasing = true;
                NMA.SetDestination(player.transform.position);
                secondCounter = 0f;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject de = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
            Destroy(de, 2);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Manager>().KilledMonsters += 1;
        }
	}

    public void dealDamage(int dmg)
    {
        health -= dmg;
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2)
            PM.dealDamage(1);
    }
}
