using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class NPC1_AI : MonoBehaviour {

    public int sightDistance = 10;
    public GameObject player;
    public GameObject cube;
    private Transform head;
    public float rotationSpeed;
    public float rotationAccuracy = 5;
    public float MovementSpeed = 20;
    Vector3 direction = Vector3.zero;
    Vector3 target = Vector3.zero;
    public GameObject world;
    private voxelGenerator vg = new voxelGenerator();
    public int maxHealth = 20;
    public int health;
    private bool shouldAttack = false;

    void Start() {
        head = this.transform.GetChild(0);
        health = maxHealth;
    }

    [Task]
    public bool IsPlayerNear()
    {
        return Vector3.Distance(player.transform.position, this.transform.position) < 10;
    }

    [Task]
    public bool HasHealth()
    {
        return health > 0;
    }

    [Task]
    public void SelectTarget()
    {
        direction = new Vector3Int((int)(Random.value * 3) - 1, 0, (int)(Random.value * 3) - 1);
        target = this.transform.position + direction;

        if (target.x < 0f || target.z < 0f || (direction.x == 0 && direction.z == 0))
        {
            Task.current.Fail();
        }
        else
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void LookAtTarget()
    {
        head.rotation = Quaternion.Slerp(
            head.rotation,
            Quaternion.LookRotation(direction),
            rotationSpeed * Time.deltaTime
        );

        float angle = Vector3.Angle(head.forward, direction);

        if(Task.isInspected)
        {
            Task.current.debugInfo = "angle = " + angle;
        }
        
        if (angle < this.rotationAccuracy)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void Walk()
    {
        transform.Translate( direction.normalized * Time.deltaTime * MovementSpeed * 0.1f);

        if (Vector3.Distance(transform.position, target) <= 0.1f)
        {
            transform.position = new Vector3(target.x, target.y, target.z);
            Task.current.Succeed();
        }
        else
        {
            Debug.DrawLine(transform.position, target);
        }
    }

    [Task]
    public void Move(float StoppingDistance)
    {
        //transform.Translate(direction / 2 * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target) <= StoppingDistance)
        {
            Task.current.Succeed();
        }
        else
        {
            transform.Translate(direction.normalized * Time.deltaTime * MovementSpeed * 0.1f);
            Debug.DrawLine(transform.position, target);
        }
    }

    int random(int a)
    {
        return (int)(Random.value * (a + 1));
    }

    //bool LoadCube(Vector3 vec)
    //{
    //    Vector3Int pos = new Vector3Int((int)transform.position.x + (int)vec.x, (int)transform.position.y + (int)vec.y, (int)transform.position.z + (int)vec.z);
    //    Vector2Int chunkPos = new Vector2Int(pos.x / 32, pos.z / 32);

    //    GameObject chunk = world.transform.GetChild(4 * chunkPos.x + chunkPos.y + 1).gameObject;
    //    Vector3Int posInChunk = new Vector3Int(pos.x % 32, pos.y, pos.z % 32);


    //    //if(chunk.GetComponent<meshGenerator>().terrain[posInChunk.x,posInChunk.y,posInChunk.z])
    //    //{
    //    //    Instantiate(cube, pos, Quaternion.identity);
    //    //}
        
    //    return chunk.GetComponent<meshGenerator>().terrain[posInChunk.x, posInChunk.y, posInChunk.z];
    //}

    [Task]
    public void TargetPlayer()
    {
        target = player.transform.position;
        direction = target - transform.position;
        Task.current.Succeed();
    }

    [Task]
    public void Die()
    {
        Destroy(this.gameObject);
    }

    [Task]
    public void Attack()
    {
        player.GetComponent<HealthInventoryManager>().health -= 1;
        Task.current.Succeed();
    }
}

