using UnityEngine;
using System.Collections;

public class ScrollBG : MonoBehaviour {


	public GameState gamestate;
	public float ResetPosition;
	public float OffScreen;
	public float speed;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gamestate.stategame != GameState.StateGame.Dying && gamestate.stategame != GameState.StateGame.EndScreen
		    &&gamestate.stategame != GameState.StateGame.Shopping&&gamestate.stategame != GameState.StateGame.HighScore) {
			if (this.transform.position.x < OffScreen) {
				this.transform.position = new Vector3 ((ResetPosition), this.transform.position.y, 0);
			}
			this.transform.Translate (-Vector3.right * speed * Time.deltaTime);
		}

		
	}
}
