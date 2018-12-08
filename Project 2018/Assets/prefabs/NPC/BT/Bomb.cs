using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    private GameObject[] enemies;
    public GameObject particleEffect2;
    private Rigidbody rb;

    private void OnCollisionEnter()
    {
        GameObject pe = Instantiate(particleEffect2, transform.position, particleEffect2.transform.rotation);
        Destroy(pe, 2);

        enemies = GameObject.FindGameObjectsWithTag("NMAgent");
        foreach (GameObject go in enemies)
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 4)
            {
                go.GetComponent<NMAgent>().dealDamage(3);
            }
        }

        enemies = GameObject.FindGameObjectsWithTag("WaypointAgent");

        foreach (GameObject go in enemies)
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 4)
            {
                go.GetComponent<WaypointAgent>().dealDamage(3);
                rb = go.GetComponent<Rigidbody>();
                rb.AddExplosionForce(1000, transform.position + new Vector3(0, -2, 0), 10);
            }
        }

        //enemies = GameObject.FindGameObjectsWithTag("BTAgent");
        //foreach (GameObject go in enemies)
        //{
        //    if (Vector3.Distance(go.transform.position, transform.position) <= 4)
        //    {
        //        go.GetComponent<BTAgent>().dealDamage(3);
        //        rb = go.GetComponent<Rigidbody>();
        //        rb.AddExplosionForce(1000, transform.position + new Vector3(0, -2, 0), 10);
        //    }
        //}
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(player.transform.position, transform.position) <= 4)
        {
            player.GetComponent<PlayerManager>().dealDamage(3);

        }

        Destroy(gameObject);
        Destroy(this);
    }
}
