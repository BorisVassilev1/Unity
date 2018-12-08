using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float speed = 20.0F;
    private float rotationSpeed = 120.0F;

    public GameObject BulletPrefab;
    public Transform FirstTurret;
    public Transform SecondTurret;

    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * this.speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * this.rotationSpeed * Time.deltaTime;

        Quaternion turnAngle = Quaternion.Euler(0f, rotation, 0f);

        this.rigidBody.MovePosition(this.rigidBody.position + this.transform.forward * translation);
        this.rigidBody.MoveRotation(this.rigidBody.rotation * turnAngle);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(this.BulletPrefab,
                this.FirstTurret.transform.position,
                this.FirstTurret.transform.rotation);

            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 2500);

            GameObject secondBullet = Instantiate(this.BulletPrefab,
                this.SecondTurret.transform.position,
                this.SecondTurret.transform.rotation);

            secondBullet.GetComponent<Rigidbody>().AddForce(secondBullet.transform.forward * 2500);
        }
    }
}
