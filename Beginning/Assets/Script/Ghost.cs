using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

	private Rigidbody2D ghostRigibody;
	float move = -1;
	public bool facingRight;
	// Use this for initialization
	public GameObject ShieldItem;
	public GameObject JumpItem;
	public GameObject LifeItem;
	int randomNumber = 0;

	void Awake()
	{
		GameObject gameItem;
		randomNumber = Random.Range (0, 100);
		if (randomNumber > 98) {
			gameItem = (Instantiate (LifeItem) as GameObject);
			gameItem.transform.parent = this.transform;
			gameItem.transform.position = new Vector2 (this.transform.position.x, 0.85f);
		} else if (randomNumber > 95) {
			gameItem = (Instantiate (ShieldItem) as GameObject);
			gameItem.transform.parent = this.transform;
			gameItem.transform.position = new Vector2 (this.transform.position.x, 0.85f);
		} else if (randomNumber > 90) {
			gameItem = (Instantiate (JumpItem) as GameObject);
			gameItem.transform.parent = this.transform;
			gameItem.transform.position = new Vector2 (this.transform.position.x, 0.85f);
		}
	}
	void Start () {
		ghostRigibody = GetComponent <Rigidbody2D> ();
		facingRight = false;
		transform.rotation = Quaternion.Euler(transform.rotation.x,180,transform.rotation.z);

	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x < -19||transform.position.y < -8) 
		{
			Destroy(this.gameObject);
		}
		ghostRigibody.velocity = new Vector2 (move*Time.deltaTime*100, ghostRigibody.velocity.y);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.transform.tag == "Pumpkin" || col.transform.tag == "EnemySquare") {
			
			if (facingRight == true) {
				move *= -1;
				facingRight = false;
				transform.rotation = Quaternion.Euler (transform.rotation.x, 180, transform.rotation.z);
				ghostRigibody.velocity = new Vector2 (move * Time.deltaTime * 200, ghostRigibody.velocity.y);
			} else {
				move *= -1;
				facingRight = true;
				transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z);
				ghostRigibody.velocity = new Vector2 (move * Time.deltaTime * 200, ghostRigibody.velocity.y);
			}
		} else if (col.transform.tag == "Player") 
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		//pumpkin.transform = null;
	}
}
