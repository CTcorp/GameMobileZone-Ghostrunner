//Need recode Update

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	// Use this for initialization
	private Animator itemAnimator;
	public TextMesh tm;
	public GameObject showquantity;
	public LayerMask layerMask;


	public float countTime;
	public Texture2D clockBG;
	public Texture2D clockFG;
	
	private float timeRemaining;
	private float timetoRun=0;
	private float percent;
	private float clockFGMaxWidth;
	public bool clockIsPaused = true;
	public PlayerEffect playereffectObject;
	public GhostRunnerResources resources;


	void Start () {
		itemAnimator = GetComponent <Animator> ();
		clockFGMaxWidth = clockFG.width;
	}
	
	// Update is called once per frame
	void Update () {

		if (!clockIsPaused) {
			DOCountdown ();
		}

		if (transform.position.x < -19||transform.position.y < -8) 
		{
			Destroy(this.gameObject);
		}

		switch(this.tag)
		{
		case "Shield":
			if(resources.numShieldItem>0&&resources.isuseditem_ok)
			{
				itemAnimator.SetBool ("Shielded", true);
				tm.text=resources.numShieldItem.ToString();
				showquantity.SetActive(true);
			}
			else
			{
				itemAnimator.SetBool ("Shielded", false);
				tm.text="";
				showquantity.SetActive(false);
			}
			break;
		case "Jump":
			if(resources.numJumpItem>0&&resources.isuseditem_ok)
			{
				itemAnimator.SetBool ("JumpState", true);
				tm.text=resources.numJumpItem.ToString();
				showquantity.SetActive(true);
			}
			else
			{
				itemAnimator.SetBool ("JumpState", false);
				tm.text="";
				showquantity.SetActive(false);
			}
			break;
		case "Life":
			if(resources.numLifeItem>0&&resources.isuseditem_ok)
			{
				itemAnimator.SetBool ("LifeState", true);
				tm.text=resources.numLifeItem.ToString();
				showquantity.SetActive(true);
			}
			else
			{
				itemAnimator.SetBool ("LifeState", false);
				tm.text="";
				showquantity.SetActive(false);
			}
			break;
		}

	}

	void  OnGUI()
	{
		if (clockIsPaused==false) {
			float newBarWidth = (percent / 100) * clockFGMaxWidth / 2; // this is the width that the foreground bar should be
		
			GUI.BeginGroup (new Rect (Screen.width / 2 - clockBG.width / 4, 0, clockBG.width / 2, clockBG.height / 2));
				GUI.DrawTexture (new Rect (0, 0, clockBG.width / 2, clockBG.height / 2), clockBG);
				GUI.BeginGroup (new Rect (0, 0, newBarWidth, clockFG.height / 2));
					GUI.DrawTexture (new Rect (0, 0, clockFG.width / 2, clockFG.height / 2), clockFG);
				GUI.EndGroup ();
			GUI.EndGroup ();
		}
	}

	public void StartCountdown()
	{
		timeRemaining = countTime;
		clockIsPaused = false;
		timetoRun = Time.time + countTime;
	}
	void DOCountdown()
	{
		timeRemaining = timetoRun - Time.time;
		
		percent = timeRemaining/countTime * 100;
		if (timeRemaining <=0)
		{
			clockIsPaused = true;
			//TimeIsUp();
			//Debug.Log("Time is up!");

			switch(this.tag)
			{
			case "Shield":
				if(playereffectObject.form==1)
				{
					playereffectObject.form=0;
				}
				break;
			case "Jump":
				if(playereffectObject.form==2)
				{
					playereffectObject.form=0;
				}
				break;
			case "Life":
				if(playereffectObject.form==3)
				{
					playereffectObject.form=0;
				}
				break;
			}
		}
		//ShowTime();
	}

	public void PauseClock()
	{
		clockIsPaused = true;
	}

	void OnMouseDown(){
		/*if (quantity > 0)
			quantity -= 1;*/
	}  
	
}
