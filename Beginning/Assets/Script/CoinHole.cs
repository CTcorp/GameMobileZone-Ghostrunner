using UnityEngine;
using System.Collections;

public class CoinHole : MonoBehaviour {

	public GameState gamestate;
	public GameObject spawnObject;
	public Vector3 startPosition;
	//public float delay = 0.002f;
	//int cancreateobj = 0;
	private float delay = 0.2f;
	private float timeForNextEvent=0f;
	//int randomNumber = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (gamestate.stategame == GameState.StateGame.InGame) {
			if (timeForNextEvent == 0f) {
				timeForNextEvent = Time.time + delay;
			} else if (timeForNextEvent < Time.time) {
				Object objectToSpawn = Instantiate (spawnObject, transform.position, transform.rotation);
				timeForNextEvent = 0f;
			}
		}
		//if (cancreateobj <10 && gamestate.stategame == GameState.StateGame.InGame) {
		//	cancreateobj+=1;
		//} else if (cancreateobj >=10) {
			//randomNumber = Random.Range(0,1000);
			
		//	if(randomNumber>0)
		//	{

			//	Object objectToSpawn = Instantiate (spawnObject, transform.position, transform.rotation);
				//Transform t = Instantiate(spawnObject, transform.position,transform.rotation) as Transform;
				//GameObject go = t.gameObject;

		//	}
			//cancreateobj=0;
			//timeForNextEvent = 0f;
		//}
		
		 //(SpawnChallenge());

	
	}
}
