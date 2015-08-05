using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreItem : MonoBehaviour {

	public GameState gamestate;
	public GhostRunnerResources resources;
	public Notification notification;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		if (resources.HasConnection ()) 
		{
			if (transform.parent.name == "EndGameMenu")
				gamestate.laststate = "EndGame";
			if (transform.parent.name == "MainMenu")
				gamestate.laststate = "MainMenu";
			gamestate.ChangeState(GameState.StateGame.HighScore);
			//gamestate.stategame = GameState.StateGame.HighScore;
		} else 
		{
			showNoConnection();
		}

	}

	void showNoConnection()
	{
		gamestate.laststate = "EndGame";
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NoConnection;
		notification.isDone = false;
		resources.isnotify = true;
	}
}
