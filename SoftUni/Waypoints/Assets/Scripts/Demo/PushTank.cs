using UnityEngine;
using UnityEngine.UI;
public class PushTank : MonoBehaviour
{

    public float acceleration;
    public float deceleration = 3.0f;
    public float Accuracy = 1.0f;
    public float RotationSpeed = 1.0f;
    float Speed = 0;
    float speedDelta;
    public GameObject speedDisplay;
    public Transform target;
    void Start()
    {
    }

    void LateUpdate()
    {
        //speedDelta = (10 - Abs(target.localRotation.y)) / 90;
        speedDelta = (30 - Vector3.Angle(transform.forward, target.forward)) / 90;
        if (speedDelta > 0)
        {
            Speed += acceleration * speedDelta * Time.deltaTime;
        }
        else if (speedDelta < 0)
        {
            Speed += deceleration * speedDelta * Time.deltaTime;
            //Debug.Log("speed has decreased!1");
        }
        Speed = Mathf.Clamp(Speed, 10, 100);
        Vector3 lookAtTarget = new Vector3(this.target.position.x,
            this.transform.position.y,
            this.target.position.z);

        Vector3 direction = lookAtTarget - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction),
            this.RotationSpeed * Time.deltaTime);

        this.transform.Translate(0, 0, this.Speed * Time.deltaTime);

        speedDisplay.GetComponent<Text>().text = Speed.ToString();
    }
    float Abs(float a)
    {
        return (a > 0) ? a : -a;
    }
}