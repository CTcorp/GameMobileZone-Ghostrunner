using UnityEngine;
using System.Collections;

public class BackIcon : MonoBehaviour
{
	public GameState gamestate;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDown(){
		if(gamestate.laststate == "MainMenu")
			gamestate.ChangeState(GameState.StateGame.Reborn);
			//gamestate.stategame = GameState.StateGame.Reborn;
		else if(gamestate.laststate == "EndGame")
			gamestate.ChangeState(GameState.StateGame.EndScreen);
			//gamestate.stategame = GameState.StateGame.EndScreen;

	} 
}

