using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	//Biến lưu trữ Rigidbody của player
	private Rigidbody2D playerRigibody;
	
	//Biến lưu trữ Animater của player
	public Animator playerAnimator;
	
	//Nếu player đang va chạm thì canJump = true, nếu player đang trên không thì canJump = false
	public bool canJump = true;
	
	//private bool facingRight = true;

	//Chỉ xét tương tác với playerMask = Icon (Check touch vào icon)
	public LayerMask layerMask;

	//Hai điểm để xét va chạm của player
	public Text balance;
	public TextMesh coinText;
	public int score,realscore;
	public String rstext = "";

	
	public GameState gamestate;
	private float positionx;
	public GameObject ground1;
	public GameObject ground2;
	public Item shieldItem;
	public Item jumpItem;
	public Item lifeItem;

	AudioSource[] playerEffect;

	public PlayerForm playerform;
	public PlayerEffect playereffectObject;

	public GhostRunnerResources resources;
	public Notification notification;


	public enum PlayerForm
	{
		Normal,
		ShieldForm,
		LifeForm,
		JumpForm
	}
	
	// Use this for initialization

	void Start () 
	{
		playerEffect = GetComponents<AudioSource>();
		playerEffect [0].Play ();
		playerEffect [0].loop = true;
		gamestate.ChangeState(GameState.StateGame.MainMenu);//gamestate.stategame = GameState.StateGame.MainMenu;
		playerRigibody = GetComponent <Rigidbody2D> ();
		playerAnimator = GetComponent <Animator> ();
		positionx = transform.position.x;
		realscore = 0;
		score = 0;
		playerform = PlayerForm.Normal;

		//StartCoroutine (GetAdsID());
		
	}

	IEnumerator GetAdsID()
	{
		WWW www = new WWW("http://ctcoporation.zz.mu/unity.txt");

		yield return www;
		if(www.text.StartsWith("Ads"))
			balance.text = www.text.Substring(6);
		else
			balance.text = "Can't connect to server";

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (score < resources.realgold)
			score += 1;
		if (resources.realgold == 0)
			score = 0;
		//balance.text = "Balance : " + score;
		coinText.text=resources.gold.ToString();
		if (gamestate.stategame == GameState.StateGame.Reborn) 
		{
			Reborn();
		}
		if (gamestate.stategame == GameState.StateGame.Restart) 
		{
			Restart();
		}

		transform.position = new Vector3 (positionx, transform.position.y, 0);
		
		//Xử lý gameState
		switch (gamestate.stategame) 
		{


		/////////////////////////////Update MainMenu ////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////

		case GameState.StateGame.MainMenu:
			playerAnimator.SetFloat ("speed", 1f);
			break;




		//////////////////////////////Update InGame /////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		
		case GameState.StateGame.InGame:
			playerAnimator.SetFloat ("speed", 1f);

			// Xử lý Input mouse
			if (Input.GetMouseButtonDown(0)&&canJump==true) 
			{
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 touchPos = new Vector2(position.x, position.y);
				RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.up,0.001f,layerMask);
				if(hit)
				{
					if(hit.collider.tag=="Shield"&&resources.numShieldItem>0)
					{
						//resources.numShieldItem-=1;
						//shieldItem.StartCountdown();
						//playereffectObject.form=1;
						//jumpItem.PauseClock();
						SaveDataCloud("Shield");
					}
					else if(hit.collider.tag=="Jump"&&resources.numJumpItem>0)
					{
						//resources.numJumpItem-=1;
						//jumpItem.StartCountdown();
						//playereffectObject.form = 2;
						//shieldItem.PauseClock();
						SaveDataCloud("Jump");
					}
					else if(hit.collider.tag=="Life"&&resources.numLifeItem>0)
					{
						//resources.numLifeItem-=1;
						//playereffectObject.form = 3;
						SaveDataCloud("Life");
					}
					//resources.SaveGame();
					
				}
				else if(!resources.isnotify)
				{
					if(playerform==PlayerForm.JumpForm)
					{
						playerEffect[1].Play();
						playerRigibody.AddForce(new Vector2(0f, 1000f));
					}
					else
					{
						playerEffect[1].Play();
						playerRigibody.AddForce(new Vector2(0f, 500f));
					}
				}
			}


			//Xử lý touch
			/*if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began&&canJump==true) 
			{
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Vector2 touchPos = new Vector2(position.x, position.y);
				RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.up,0.001f,layerMask);
				if(hit)
				{
					if(hit.collider.tag=="Shield"&&resources.numShieldItem>0)
					{
						resources.numShieldItem-=1;
						shieldItem.StartCountdown();
						playereffectObject.form=1;
						jumpItem.PauseClock();
						SaveDataCloud("ShieldItem");
					}
					else if(hit.collider.tag=="Jump"&&resources.numJumpItem>0)
					{
						resources.numJumpItem-=1;
						jumpItem.StartCountdown();
						playereffectObject.form = 2;
						shieldItem.PauseClock();
					}
					else if(hit.collider.tag=="Life"&&resources.numLifeItem>0)
					{
						resources.numLifeItem-=1;
						playereffectObject.form = 3;
					}
					//resources.SaveGame();
					
				}
				else
				{
					if(playerform==PlayerForm.JumpForm)
					{
						playerEffect[1].Play();
						playerRigibody.AddForce(new Vector2(0f, 1000f));
					}
					else
					{
						playerEffect[1].Play();
						playerRigibody.AddForce(new Vector2(0f, 500f));
					}
				}	
			}*/



			if (Input.GetKeyDown (KeyCode.Space)&& canJump==true) 
			{	
				
				if(playerform==PlayerForm.JumpForm)
				{
					playerEffect[1].Play();
					playerRigibody.AddForce(new Vector2(0f, 1000f));
				}
				else
				{
					playerEffect[1].Play();
					playerRigibody.AddForce(new Vector2(0f, 500f));
				}
			}
			break;


		/////////////////////////////Update EndGame /////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		}


		switch (playereffectObject.form) 
		{
		case 0:
			playerform = PlayerForm.Normal;
			break;
		case 1:
			playerform = PlayerForm.ShieldForm;
			break;
		case 2:
			playerform = PlayerForm.JumpForm;
			break;
		case 3:
			playerform = PlayerForm.LifeForm;
			break;
		}

		/*
		//Xử lý key input di chuyển trái, phải
		float move = Input.GetAxisRaw ("Horizontal");
		//playerAnimator.SetFloat ("speed", Mathf.Abs(move));
		playerRigibody.velocity = new Vector2 (move*Time.deltaTime*200, playerRigibody.velocity.y);
		if (facingRight == true && move < 0) 
		{
			facingRight = false;
			transform.rotation = Quaternion.Euler(transform.rotation.x,180,transform.rotation.z);
		}
		if (facingRight == false && move > 0) 
		{
			facingRight = true;
			transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
		}*/
		
		if (transform.position.y < -10&&gamestate.stategame!=GameState.StateGame.Shopping&&gamestate.stategame!=GameState.StateGame.HighScore) 
		{
			//realscore = 0;
			gamestate.ChangeState(GameState.StateGame.EndScreen);//gamestate.stategame = GameState.StateGame.EndScreen;
			this.GetComponent<Rigidbody2D>().isKinematic = true;
			//resources.SaveGame();
		}

	}


	void SaveDataCloud(string itemname)
	{
		StartCoroutine(UseItem(itemname));
	}

	private IEnumerator UseItem(string type) 
	{
		if (FB.IsLoggedIn) {
			if (resources.HasConnection ()) {
				//var user = ParseUser.CurrentUser;
				switch (type) {
				case "Shield":
					shieldItem.StartCountdown ();
					playereffectObject.form = 1;
					jumpItem.PauseClock ();
					resources.UseItemCloud ("ShieldItem");
					//user ["ShieldItem"] = resources.numShieldItem-1;
					break;
				case "Jump":
					jumpItem.StartCountdown ();
					playereffectObject.form = 2;
					shieldItem.PauseClock ();
					resources.UseItemCloud ("JumpItem");
					//user ["JumpItem"] = resources.numJumpItem-1;
					break;
				case "Life":
					playereffectObject.form = 3;
					resources.UseItemCloud ("LifeItem");
					//user ["LifeItem"] = resources.numLifeItem-1;
					break;
				}
				resources.isuseditem_ok = false;
				//var saveTask = user.SaveAsync ();
				//while (!saveTask.IsCompleted)
				//	yield return null;
				//if (saveTask.IsCompleted) {
				//	resources.login_done = true;
				//	resources.RefeshDataFromServer ();
				//notification.isDone = true;
				//}
				//gamestate.stategame = GameState.StateGame.Reborn;
				//UpdateProfile();
				//}
			} else {
				showNoConnection ();
			}
		} 
		else 
		{
			switch (type) {
			case "Shield":
				resources.numShieldItem-=1;
				shieldItem.StartCountdown();
				playereffectObject.form=1;
				jumpItem.PauseClock();
				break;
			case "Jump":
				resources.numJumpItem-=1;
				jumpItem.StartCountdown();
				playereffectObject.form = 2;
				shieldItem.PauseClock();
				break;
			case "Life":
				resources.numLifeItem-=1;
				playereffectObject.form = 3;
				break;
			}
		}
		yield return null;
	}

	/*private IEnumerator UseItem()
	{
		IDictionary<string, object> testparam = new Dictionary<string, object>
		{
			{ "itemname", "ShieldItem"},
			{ "id", "822403307808270"}
		};
		ParseCloud.CallFunctionAsync<string>("UseItem", testparam).ContinueWith(t =>
		{
			rstext = t.Result;
			Debug.Log(rstext);
		});
		yield break;
	}*/
	
	void OnTriggerEnter2D(Collider2D col) {
		switch (col.tag) {
		case "EnemySquare":

			switch (playerform) {
			case (PlayerForm.Normal):
				playerEffect [2].Play ();
				gamestate.ChangeState(GameState.StateGame.Dying);//gamestate.stategame = GameState.StateGame.Dying;
				playerRigibody.AddForce (new Vector2 (0f, 600f));
				this.GetComponent<PolygonCollider2D> ().isTrigger = true;
				playerAnimator.SetBool ("die", true);
				Destroy (col.gameObject);
				break;

			case (PlayerForm.JumpForm):
				playerform = PlayerForm.Normal;
				playereffectObject.form = 0;
				jumpItem.PauseClock ();
				gamestate.ChangeState(GameState.StateGame.Restart);//gamestate.stategame = GameState.StateGame.Restart;
				break;

			case (PlayerForm.ShieldForm):
				Destroy (col.gameObject);
				break;

			case (PlayerForm.LifeForm):
				playerform = PlayerForm.Normal;
				playereffectObject.form = 0;
				gamestate.ChangeState(GameState.StateGame.Restart);//gamestate.stategame = GameState.StateGame.Restart;
				break;
			}
			break;
			
		case "Coin":
			playerEffect [3].Play ();
			resources.realgold += 1;
			break;
		case "ShieldItem":
			resources.numShieldItem += 1;
			if (FB.IsLoggedIn) {
				if (resources.HasConnection ()) 
				{
					resources.AddItemCloud ("ShieldItem", 0);
				}
				else
				{
					showNoConnection();
				}
			}
			Destroy (col.gameObject);
			break;
		case "JumpItem":
			resources.numJumpItem += 1;
			if (FB.IsLoggedIn) {
				if (resources.HasConnection ()) 
				{
					resources.AddItemCloud ("JumpItem", 0);
				}
				else
				{
					showNoConnection();
				}
			}
			Destroy (col.gameObject);
			break;
		case "LifeItem":
			resources.numLifeItem += 1;
			if (FB.IsLoggedIn) {
				if (resources.HasConnection ()) 
				{
					resources.AddItemCloud ("LifeItem", 0);
				}
				else
				{
					showNoConnection();
				}
			}
			Destroy (col.gameObject);
			break;

		}

	}
	
	void OnTriggerExit2D(Collider2D col) {
	}
	
	void Reborn()
	{
		StartCoroutine (BackToMainMenu ());
	}

	void showNoConnection()
	{
		gamestate.laststate = "Ingame";
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NoConnection;
		notification.isDone = false;
		resources.isnotify = true;
	}

	IEnumerator BackToMainMenu(){
		yield return new WaitForSeconds(0.05f);
		this.transform.position = new Vector2 (-13, -4);
		this.GetComponent<PolygonCollider2D>().isTrigger = false;
		this.GetComponent<Rigidbody2D>().isKinematic = false;
		playerAnimator.SetBool("die",false);
		gamestate.ChangeState(GameState.StateGame.MainMenu);//gamestate.stategame = GameState.StateGame.MainMenu;
		yield break;
	}

	void Restart()
	{
		this.transform.position = new Vector2 (-13, -4);
		this.GetComponent<PolygonCollider2D>().isTrigger = false;
		this.GetComponent<Rigidbody2D>().isKinematic = false;
		playerAnimator.SetBool("die",false);
		foreach (Transform child in ground1.transform) {
			Destroy (child.gameObject);
		}
		foreach (Transform child in ground2.transform) {
			Destroy (child.gameObject);
		}
		gamestate.ChangeState(GameState.StateGame.InGame);//gamestate.stategame = GameState.StateGame.InGame;
	}
	
}
