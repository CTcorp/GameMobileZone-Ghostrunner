using UnityEngine;
using System.Collections;

public class RestartIcon : MonoBehaviour {

	public GameState gamestate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		gamestate.numLoad++;
		gamestate.isAdsLoaded = false;
		gamestate.ChangeState(GameState.StateGame.Restart);//gamestate.stategame = GameState.StateGame.Restart;
	}  
}
