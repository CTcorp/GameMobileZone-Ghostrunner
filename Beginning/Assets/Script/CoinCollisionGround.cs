using UnityEngine;
using System.Collections;

public class CoinCollisionGround : MonoBehaviour {

	public Animator coinAnimator;
	// Use this for initialization
	void Start () {
		coinAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.transform.tag == "Ground"||col.transform.tag == "Pumpkin")
		{
			transform.position = new Vector2(transform.position.x, transform.position.y +1f);
			transform.parent = col.transform;
			this.GetComponent<Rigidbody2D>().isKinematic = true;
			//Rigidbody2D rigi = GetComponent<Rigidbody2D>();
			//rigi.gravityScale =0;
			//rigi.mass = 0;
			//pumpkin.transform.position = col.transform.position;
			//Debug.Log("Yeah");
		}
		if(col.transform.tag == "Player"||col.transform.tag == "Ghost")
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		//pumpkin.transform = null;
	}
}
