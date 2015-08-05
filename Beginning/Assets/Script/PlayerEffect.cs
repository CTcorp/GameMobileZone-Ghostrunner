using UnityEngine;
using System.Collections;

public class PlayerEffect : MonoBehaviour {


	public Animator playerEffectAnimator;
	public int form =0;
	// Use this for initialization
	void Start () {
		playerEffectAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerEffectAnimator.SetInteger ("Form", form);
	}
}
