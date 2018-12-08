using UnityEngine;

public class SlerpPush : MonoBehaviour
{

    public float Speed = 1.0f;
    public float RotationSpeed = 1.0f;
    public Transform Goal;

    void LateUpdate()
    {
        Vector3 destination = new Vector3(
            this.Goal.position.x,
            this.transform.position.y,
            this.Goal.position.z);

        Vector3 direction = destination - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * this.RotationSpeed);
    }
}
