using UnityEngine;
using System.Collections;

public class ShopItem : MonoBehaviour
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
		if (transform.parent.name == "EndGameMenu")
			gamestate.laststate = "EndGame";
		if (transform.parent.name == "MainMenu")
			gamestate.laststate = "MainMenu";
		gamestate.ChangeState(GameState.StateGame.Shopping);//gamestate.stategame = GameState.StateGame.Shopping;
	} 
}

