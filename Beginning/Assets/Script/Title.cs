using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {


	private Animator titleAnimator;

	// Use this for initialization
	void Start () {
	
		titleAnimator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.SetActive (false);
		//if(titleAnimator.GetBool("test")==false)
		//	gameObject.SetActive (false);
		//else
		//	gameObject.SetActive (true);
	}
}
