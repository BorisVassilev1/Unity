using UnityEngine;

[ExecuteInEditMode]
public class WaypointHelper : MonoBehaviour
{

    void Start()
    {
        if (this.transform.parent.gameObject.name != "Waypoint")
        {
            return;
        }
        this.RenameWaypoints(null);
    }

    private void RenameWaypoints(GameObject currentWaypoint)
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        int currentWaypointIndex = 1;

        foreach (GameObject waypoint in waypoints)
        {
            if (waypoint != currentWaypoint)
            {
                waypoint.name = "Waypoint" + string.Format("{0:000}", currentWaypointIndex++);
            }
        }
    }

    void Update()
    {
        this.GetComponent<TextMesh>().text = this.transform.parent.gameObject.name;
    }

    void OnDestroy()
    {
        this.RenameWaypoints(this.gameObject);
    }
}
