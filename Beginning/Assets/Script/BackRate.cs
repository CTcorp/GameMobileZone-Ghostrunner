
using UnityEngine;
using System.Collections;

public class BackRate : MonoBehaviour
{
	public GameState gamestate;
	public GameObject ScoreScrollList;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDown(){
		foreach (Transform child in ScoreScrollList.transform) {
			Destroy(child.gameObject);
		}
		if(gamestate.laststate == "MainMenu")
			gamestate.ChangeState(GameState.StateGame.MainMenu);
			//gamestate.stategame = GameState.StateGame.MainMenu;
		else if(gamestate.laststate == "EndGame")
			gamestate.ChangeState(GameState.StateGame.EndScreen);
			//gamestate.stategame = GameState.StateGame.EndScreen;
	} 
}

