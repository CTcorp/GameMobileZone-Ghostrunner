using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public Animator coinAnimator;
	private Rigidbody2D rigi;

	// Use this for initialization
	void Start () {
		//gameObject.SetActive (false);
		rigi = GetComponent<Rigidbody2D>();
		coinAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.position.x < -19||transform.position.y < -8) 
		{
			Destroy(this.gameObject);
		}
	}
}
