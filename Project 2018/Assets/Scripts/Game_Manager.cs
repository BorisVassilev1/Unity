using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {


    public int KilledMonsters = 0;
	
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Exit()
    {
        SceneManager.LoadScene("menu");
    }

    public void Play()
    {
        SceneManager.LoadScene("game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
