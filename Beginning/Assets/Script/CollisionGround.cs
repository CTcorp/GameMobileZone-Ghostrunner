using UnityEngine;
using System.Collections;

public class CollisionGround : MonoBehaviour {

	private GameObject enemy;
	// Use this for initialization
	void Start () {
		enemy = gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.transform.tag == "Ground")
		{
			enemy.transform.parent = col.transform;
			//pumpkin.transform.position = col.transform.position;
			//Debug.Log("Yeah");
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		//pumpkin.transform = null;
	}
}
