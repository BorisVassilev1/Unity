using Panda;
using UnityEngine;
using UnityEngine.AI;

public class AiBehaviour : MonoBehaviour
{
    private Vector3 Destination;
    private Vector3 Target;
    private NavMeshAgent navMeshAgent;
    private float hearingRange = 80f;
    private float shootRange = 50f;

    public int Health = 100;
    public float RotationSpeed = 5f;
    public float RotationAccuracy = 5f;

    public Transform FirstTurret;
    public Transform SecondTurret;
    public GameObject ShellPrefab;
    public Transform Player;

    public void Start()
    {
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.navMeshAgent.stoppingDistance = this.shootRange - 5;
    }

    [Task]
    public void PickDestination()
    {
        this.Destination = new Vector3(Random.Range(-15, 120), 77.7f, Random.Range(-10, 111));
        this.navMeshAgent.SetDestination(this.Destination);

        Task.current.Succeed();
    }

    [Task]
    public void MoveToDestination()
    {
        if (Task.isInspected)
        {
            Task.current.debugInfo = string.Format("t={0:0.00}", Time.time);
        }

        if (this.navMeshAgent.remainingDistance <= this.navMeshAgent.stoppingDistance && !this.navMeshAgent.pathPending)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void TargetPlayer()
    {
        this.Target = this.Player.transform.position;
        Task.current.Succeed();
    }

    [Task]
    public void LookAtTarget()
    {
        Vector3 direction = this.Target - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * this.RotationSpeed);
        
        float angle = Vector3.Angle(this.transform.forward, direction);

        if (Task.isInspected)
        {
            Task.current.debugInfo = $"angle = {angle}";
        }

        if(angle < this.RotationAccuracy)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void Fire()
    {
        GameObject bullet = Instantiate(this.ShellPrefab,
                this.FirstTurret.transform.position,
                this.FirstTurret.transform.rotation);

        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 2500);

        GameObject secondBullet = Instantiate(this.ShellPrefab,
            this.SecondTurret.transform.position,
            this.SecondTurret.transform.rotation);

        secondBullet.GetComponent<Rigidbody>().AddForce(secondBullet.transform.forward * 2500);

        Task.current.Succeed();
    }

    [Task]
    public bool IsPlayerNear()
    {
        Vector3 direction = this.Player.transform.position - this.transform.position;

        bool seeObstacle = false;

        Debug.DrawRay(this.transform.position, direction, Color.red);

        RaycastHit raycastHit;
        if (Physics.Raycast(this.transform.position, direction, out raycastHit))
        {
            if(raycastHit.collider.gameObject.tag == "Obstacle")
            {
                seeObstacle = true;
            }
        }

        if(direction.magnitude < this.hearingRange && !seeObstacle)
        {
            return true;
        }

        return false;
    }

    [Task]
    public bool Turn(float angle)
    {
        this.Target = this.transform.position + Quaternion.AngleAxis(angle, Vector3.up) * this.transform.forward;
        return true;
    }

    [Task]
    public bool IsHealthLessThan(float health)
    {
        return this.Health < health;
    }

    [Task]
    public bool IsInDanger(float minimumDistance)
    {
        Vector3 distance = this.Player.transform.position - this.transform.position;
        return distance.magnitude <= minimumDistance;
    }

    [Task]
    public void Run()
    {
        //Vector3 oppositeToPlayer = this.transform.position - this.Player.transform.position;
        //Vector3 destination = this.transform.position + oppositeToPlayer * 2;
        //this.navMeshAgent.SetDestination(destination);
        this.Destination = new Vector3(Random.Range(-15, 120), 77.7f, Random.Range(-10, 111));
        this.navMeshAgent.SetDestination(this.Destination);

        Task.current.Succeed();
    }

    [Task]
    public bool IsAlive()
    {
        if (Task.isInspected)
        {
            Task.current.debugInfo = $"Health = {this.Health}";
        }

        return this.Health > 0;
    }

    [Task]
    public void Die()
    {
        Destroy(this.gameObject);
        Destroy(this.GetComponent<AiController>().HealthBar.gameObject);

        Task.current.Succeed();
    }

    [Task]
    public void SetTargetDestination()
    {
        this.navMeshAgent.SetDestination(this.Target);
        Task.current.Succeed();
    }

    [Task]
    public bool IsAbleToShoot()
    {
        Vector3 distance = this.Target - this.transform.position;

        if(distance.magnitude < this.shootRange && Vector3.Angle(this.transform.forward, distance) < 1.0f)
        {
            return true;
        }
        return false;
    }
}
