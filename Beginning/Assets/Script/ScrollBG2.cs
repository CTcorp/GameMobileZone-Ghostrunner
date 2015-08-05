using UnityEngine;
using System.Collections;

public class ScrollBG2 : MonoBehaviour {
	
	public float ResetPosition;
	public float OffScreen;
	public float speed;
	public  Renderer render;
	public GameObject gameobj;
	private float spriteWidth;
	public GameState gamestate;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(gamestate.stategame == GameState.StateGame.Reborn||gamestate.stategame == GameState.StateGame.MainMenu||
		   gamestate.stategame == GameState.StateGame.Restart)
		{
			foreach(Transform child in transform){
				Destroy(child.gameObject);
			}
		}
		if (gamestate.stategame != GameState.StateGame.Dying&& gamestate.stategame != GameState.StateGame.EndScreen
		    &&gamestate.stategame != GameState.StateGame.Shopping&&gamestate.stategame != GameState.StateGame.HighScore) {
			spriteWidth = render.bounds.size.x;
			if (this.transform.position.x < OffScreen) {
				//this.transform.position= new Vector3((ResetPosition),this.transform.position.y,0);
				this.transform.position = new Vector3 (this.transform.position.x + spriteWidth,
			                                     this.transform.position.y, 0);
				foreach(Transform child in transform)
				{
					child.transform.position = new Vector3 (child.transform.position.x - spriteWidth,
					                                       child.transform.position.y, 0);
				}
			}
			this.transform.Translate (-Vector3.right * speed * Time.deltaTime);
		}
		
	}
}
