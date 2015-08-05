using UnityEngine;
using System.Collections;

public class Pumpkin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -19||transform.position.y < -8) 
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.transform.tag == "Player") {
			Destroy (this.gameObject);
		}
	}
}
