using UnityEngine;
using System.Collections;

public class RotationEffect : MonoBehaviour {

	// Use this for initialization

	public float speed;
	public string rotation;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(rotation =="z")
		transform.Rotate(0, 0, -Time.deltaTime*speed);
		else if(rotation =="y")
			transform.Rotate(0, -Time.deltaTime*speed,0);
		else
			transform.Rotate(-Time.deltaTime*speed, 0,0);
	}
}
