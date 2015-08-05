using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
	
		player = gameObject.GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Ground") {
			player.playerAnimator.SetBool ("jump", true);
			player.canJump = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "Ground") {
			player.playerAnimator.SetBool ("jump", false);
			player.canJump = false;
		}
	}
}
