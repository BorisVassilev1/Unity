using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class BTAgent : MonoBehaviour {

    public int MaxHealth = 7;
    private int health;
    public bool willBeHit = false;

    private  GameObject player;
    private PlayerManager PM;

    public float sightDistance = 10;
    public float ShootingDistance = 4;

    public GameObject DeathEffect;

    private Vector3 Target;
    private Vector3 Direction;
    public float RatationSpeed = 4;
    public float MovementSpeed = 4;

    public GameObject bomb;

	void Start () {
        health = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        PM = player.GetComponent<PlayerManager>();
	}

    public void dealDamage(int dmg)
    {
        health -= dmg;
    }

    [Task]
    public void Attack()
    {
        GameObject go = Instantiate(bomb, transform.position + new Vector3(0,2,0), Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        Task.current.Succeed();
    }

    [Task]
    public void SelectTarget()
    {
        Target = new Vector3(Random.value * 48 - 24, transform.position.y, Random.value * 48 - 24);
        Direction = Target - transform.position;
        Direction = Direction.normalized * 3;
        Target = transform.position + Direction;
        Task.current.Succeed();
    }

    [Task]
    public void TargetPlayer()
    {
        Target = player.transform.position;
        Task.current.Succeed();
    }

    [Task]
    public void LookAtTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), RatationSpeed);
        if(Quaternion.Angle(transform.rotation,Quaternion.LookRotation(Direction)) < 2)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void MoveToTarget(float stoppingDistance)
    {
        Direction = Target - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), RatationSpeed);
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
        Debug.DrawLine(transform.position, Target);
        float dist = Vector3.Distance(transform.position, Target);
        if (Task.isInspected)
        {
            Task.current.debugInfo = "dist = " + dist;
        }

        if(dist < stoppingDistance)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public bool IsPlayerNear()
    {   if (player)
        {
            return Vector3.Distance(transform.position, player.transform.position) < sightDistance;
        }
        else
        {
            return false;
        }
    }

    [Task]
    public bool CanShootPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position) < ShootingDistance;
    }

    [Task]
    public bool IsHealthLessThan(int value)
    {
        return health < value;
    }

    [Task]
    public void Die()
    {
        GameObject de = Instantiate(DeathEffect, transform.position, transform.rotation);
        Destroy(de, 2);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Manager>().KilledMonsters += 1;
        Destroy(gameObject);
    }
}
