using UnityEngine;
using System.Collections;

public class EnemyHole : MonoBehaviour {

	public GameState gamestate;
	public GameObject pumpkin;
	public GameObject ghost;
	public float offset;
	private float delay = 1f;
	private float timeForNextEvent=0f;
	int randomNumber = 1;
	void Start () {
		
		
	}
	
	
	
	
	// Update is called once per frame
	void Update () {
		
		if (gamestate.stategame == GameState.StateGame.InGame) {
			if (timeForNextEvent == 0f) {
				timeForNextEvent = Time.time + delay;
			} else if (timeForNextEvent < Time.time) {
				randomNumber = Random.Range (0, 1000);
			
				if (randomNumber % 2 == 0) {
					Object objectToSpawn = Instantiate (pumpkin, transform.position, transform.rotation);
					/*if(randomNumber <=800)
					{
						Object objectToSpawn1 = Instantiate (coin, transform.position, transform.rotation);
					}
					else
					{
						Object objectToSpawn2 = Instantiate (coin, transform.position, transform.rotation);
						Object objectToSpawn3 = Instantiate (coin, transform.position, transform.rotation);
						Object objectToSpawn4 = Instantiate (coin, transform.position, transform.rotation);
					}*/
				}
				else if (randomNumber % 7 == 0)
				{
					Object objectToSpawn = Instantiate (ghost, transform.position, transform.rotation);
				}
			
				timeForNextEvent = 0f;
			}
		}
	}
}
