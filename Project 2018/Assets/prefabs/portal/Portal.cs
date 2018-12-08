using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Portal : MonoBehaviour {

    public GameObject winMenu;
    public GameObject enemies;
    public GameObject WinInfo;
    public GameObject InGameUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && other.isTrigger == false)
        {
            winMenu.SetActive(true);
            Destroy(enemies);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player.GetComponent<PlayerManager>());
            Destroy(player.GetComponent<RigidbodyFirstPersonController>());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            WinInfo.GetComponent<Text>().text = "YOU HAVE KILLED " + GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Manager>().KilledMonsters + " MONSTERS!";
            InGameUI.SetActive(false);
        }
    }

}
