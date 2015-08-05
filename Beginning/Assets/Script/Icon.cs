using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour
{
	public GameState gamestate;
	Item[] allChildren;
	// Use this for initialization
	void Start ()
	{
		allChildren = GetComponentsInChildren<Item>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gamestate.stategame == GameState.StateGame.InGame || 
			gamestate.stategame == GameState.StateGame.Shopping) {
			foreach (Item child in allChildren) {
				child.gameObject.SetActive (true);
			}
		} else {
			foreach (Item child in allChildren) {
				child.gameObject.SetActive (false);
			}
		}
	}
}

