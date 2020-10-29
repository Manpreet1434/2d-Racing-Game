using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

	public Text scoreText;
	int score;
	bool GameOver;
	public Button[] buttons;

	// Use this for initialization
	void Start () {
		GameOver = false;
		score = 0;	
		InvokeRepeating ("scoreUpdate",1.0f,0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + score;
	}

	void scoreUpdate(){
		if (GameOver == false) {
			score += 1;
		}
	}

	public void gameOver(){
		GameOver = true;
		foreach(Button button in buttons){
			button.gameObject.SetActive(true);
		}
	}

	public void Play(){       
		Application.LoadLevel ("Level1");
	}

	public void Pause(){

		if (Time.timeScale == 1) {

			Time.timeScale = 0;
		} 
		else if (Time.timeScale == 0) {

			Time.timeScale = 1;
		}
	}
		
	public void MainMenu(){
		Application.LoadLevel ("menu");
	}

	public void exit(){
		Application.Quit ();
	}
}
