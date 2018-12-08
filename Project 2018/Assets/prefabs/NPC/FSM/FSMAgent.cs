using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMAgent : MonoBehaviour {

    private GameObject player;
    private Animator animator;

    public int MaxHealth = 10;
    private int health;

    public GameObject projectile;

    public bool willBeHit = false;

    public GameObject DeathEffect;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
        if (health <= 0)
        {
            GameObject de = Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(de, 2);
            Destroy(gameObject);
        }
    }

    public void dealDamage(int dmg)
    {
        health -= dmg;
    }

    public void Fire()
    {
        GameObject go = Instantiate(projectile, transform.position + transform.forward * 2.1f, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
    }
}
