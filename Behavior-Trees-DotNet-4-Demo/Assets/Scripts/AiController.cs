using UnityEngine;
using UnityEngine.UI;

public class AiController : MonoBehaviour
{
    private AiBehaviour aiBehaviour;
    public Slider HealthBar;

    public void Start()
    {
        this.aiBehaviour = this.GetComponent<AiBehaviour>();

        InvokeRepeating(nameof(UpdateHealth), 5, 0.5f);
    }

    public void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        this.HealthBar.transform.position = screenPosition;

        this.HealthBar.value = this.aiBehaviour.Health;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shell")
        {
            this.aiBehaviour.Health -= 5;
        }
    }

    private void UpdateHealth()
    {
        if (this.aiBehaviour.Health < 100)
        {
            this.aiBehaviour.Health++;
        }
    }
}
