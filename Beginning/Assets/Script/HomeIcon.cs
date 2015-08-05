using UnityEngine;
using System.Collections;

public class HomeIcon : MonoBehaviour {


	public GameState gamestate;
	public GhostRunnerResources resource;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){

		gamestate.ChangeState(GameState.StateGame.Reborn);
		//gamestate.stategame = GameState.StateGame.Reborn;
		//resource.realgold = 100000;
		//resource.numShieldItem = 100;
		//resource.numJumpItem = 100;
		//resource.numLifeItem = 100;
		//resource.SaveGame ();
	}  
}
