using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public GameObject particleEffect2;
    public GameObject particleEffect1;
    private GameObject[] enemies;
    private Rigidbody rb;
    public float BasicAttackCD = 1;
    public float SpecialAttackCD = 5;
    private float BACDCounter;
    private float SACDCounter;

    public GameObject BATimer;
    public GameObject SATimer;
    private Text BTimer;
    private Text STimer;

    public int maxHealth = 20;
    private int health;
    public Slider HealthBar;

    public GameObject deathmenu;
    public GameObject InGameUI;

	void Start () {
        BACDCounter = 0;
        SACDCounter = 0;
        BTimer = BATimer.GetComponent<Text>();
        STimer = SATimer.GetComponent<Text>();
        health = maxHealth;
        HealthBar.maxValue = maxHealth;
    }
	
	void Update () {
        BACDCounter -= Time.deltaTime;
        SACDCounter -= Time.deltaTime;
        if(BACDCounter <= 0)
        {
            BACDCounter = 0;
        }
        if(SACDCounter <= 0)
        {
            SACDCounter = 0;
        }
        BTimer.text = Mathf.Round(BACDCounter * 10) / 10 + "";
        STimer.text = Mathf.Round(SACDCounter * 10) / 10 + "";
        


		if(Input.GetMouseButtonDown(1) && SACDCounter == 0)
        {
            SACDCounter = SpecialAttackCD;
            GameObject pe = Instantiate(particleEffect2,transform.position,particleEffect2.transform.rotation);
            Destroy(pe, 2);

            enemies = GameObject.FindGameObjectsWithTag("NMAgent");
            foreach (GameObject go in enemies)
            {
                if(Vector3.Distance(go.transform.position,transform.position) <= 4)
                {
                    go.GetComponent<NMAgent>().dealDamage(5);
                }
            }

            enemies = GameObject.FindGameObjectsWithTag("WaypointAgent");
            
            foreach (GameObject go in enemies)
            {
                if (Vector3.Distance(go.transform.position, transform.position) <= 4)
                {
                    go.GetComponent<WaypointAgent>().dealDamage(5);
                    rb = go.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(1000, transform.position + new Vector3(0,-2,0), 10);
                }
            }

            enemies = GameObject.FindGameObjectsWithTag("BTAgent");
            foreach (GameObject go in enemies)
            {
                if (Vector3.Distance(go.transform.position, transform.position) <= 4)
                {
                    go.GetComponent<BTAgent>().dealDamage(5);
                    rb = go.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(1000, transform.position + new Vector3(0, -2, 0), 10);
                }
            }

            enemies = GameObject.FindGameObjectsWithTag("FSMAgent");
            foreach (GameObject go in enemies)
            {
                if (Vector3.Distance(go.transform.position, transform.position) <= 4)
                {
                    go.GetComponent<FSMAgent>().dealDamage(5);
                    rb = go.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(1000, transform.position + new Vector3(0, -2, 0), 10);
                }
            }
        }
        if(Input.GetMouseButtonDown(0) && BACDCounter == 0)
        {
            BACDCounter = BasicAttackCD;
            GameObject pe = Instantiate(particleEffect1, transform.position, transform.rotation);
            pe.transform.Rotate(new Vector3(90, -70, 0));
            Destroy(pe, 2);
            enemies = GameObject.FindGameObjectsWithTag("NMAgent");
            foreach (GameObject go in enemies)
            {
                if(go.GetComponent<NMAgent>().willBeHit)
                {
                    go.GetComponent<NMAgent>().dealDamage(2);
                }
            }
            enemies = GameObject.FindGameObjectsWithTag("WaypointAgent");
            foreach (GameObject go in enemies)
            {
                if (go.GetComponent<WaypointAgent>().willBeHit)
                {
                    go.GetComponent<WaypointAgent>().dealDamage(2);
                }
            }
            enemies = GameObject.FindGameObjectsWithTag("BTAgent");
            foreach (GameObject go in enemies)
            {
                if (go.GetComponent<BTAgent>().willBeHit)
                {
                    go.GetComponent<BTAgent>().dealDamage(2);
                }
            }
            enemies = GameObject.FindGameObjectsWithTag("FSMAgent");
            foreach (GameObject go in enemies)
            {
                if (go.GetComponent<FSMAgent>().willBeHit)
                {
                    go.GetComponent<FSMAgent>().dealDamage(2);
                }
            }
        }

        if(health <= 0)
        {
            deathmenu.SetActive(true);
            InGameUI.SetActive(false);
            Destroy(GetComponent<RigidbodyFirstPersonController>());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(this);
        }

        HealthBar.value = health;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NMAgent")
        {
            other.gameObject.GetComponent<NMAgent>().willBeHit = true;
        }
        if (other.gameObject.tag == "WaypointAgent")
        {
            other.gameObject.GetComponent<WaypointAgent>().willBeHit = true;
        }
        if (other.gameObject.tag == "BTAgent")
        {
            other.gameObject.GetComponent<BTAgent>().willBeHit = true;
        }
        if (other.gameObject.tag == "FSMAgent")
        {
            other.gameObject.GetComponent<FSMAgent>().willBeHit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NMAgent")
        {
            other.gameObject.GetComponent<NMAgent>().willBeHit = false;
        }
        if (other.gameObject.tag == "WaypointAgent")
        {
            other.gameObject.GetComponent<WaypointAgent>().willBeHit = false;
        }
        if (other.gameObject.tag == "BTAgent")
        {
            other.gameObject.GetComponent<BTAgent>().willBeHit = false;
        }
        if (other.gameObject.tag == "FSMAgent")
        {
            other.gameObject.GetComponent<FSMAgent>().willBeHit = false;
        }
    }

    public void dealDamage(int dmg)
    {
        health -= dmg;
    }
}
