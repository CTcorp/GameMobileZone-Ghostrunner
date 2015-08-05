using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	private Rigidbody2D zombieRigibody;
	float move = -1;
	public bool facingRight;
	// Use this for initialization
	void Start () {
		zombieRigibody = GetComponent <Rigidbody2D> ();
		facingRight = false;
	}
	
	// Update is called once per frame
	void Update () {
		zombieRigibody.velocity = new Vector2 (move*Time.deltaTime*100, zombieRigibody.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.transform.tag == "Pumpkin")
		{

			if (facingRight == true) 
			{
				move*=-1;
				facingRight = false;
				transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
				zombieRigibody.velocity = new Vector2 (move*Time.deltaTime*200, zombieRigibody.velocity.y);
			}
			else
			{
				move*=-1;
				facingRight = true;
				transform.rotation = Quaternion.Euler(transform.rotation.x,180,transform.rotation.z);
				zombieRigibody.velocity = new Vector2 (move*Time.deltaTime*200, zombieRigibody.velocity.y);
			}
		}
		if(col.transform.tag == "Player")
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		//pumpkin.transform = null;
	}
}
