using UnityEngine;
using System.Collections;


public class LifeBarControl : MonoBehaviour {

	public float countTime;
	public Texture2D clockBG;
	public Texture2D clockFG;

	private float timeRemaining;
	private float percent;
	private float clockFGMaxWidth;
	private bool clockIsPaused = false;

	// Use this for initialization
	void Start () {
		clockFGMaxWidth = clockFG.width;
	}
	
	// Update is called once per frame
	void Update () {
		if (!clockIsPaused) {
			DOCountdown();
		}

	
	}

	void  OnGUI()
	{
		float newBarWidth = (percent/100) * clockFGMaxWidth/2; // this is the width that the foreground bar should be
		
		GUI.BeginGroup (new Rect(Screen.width/2 - clockBG.width/4, 0, clockBG.width/2, clockBG.height/2));
		GUI.DrawTexture (new Rect (0, 0, clockBG.width/2, clockBG.height/2), clockBG);
		GUI.BeginGroup (new Rect(0, 0, newBarWidth, clockFG.height/2));
		GUI.DrawTexture (new Rect (0, 0, clockFG.width/2, clockFG.height/2), clockFG);
		GUI.EndGroup ();
		GUI.EndGroup();
	}
	void DOCountdown()
	{
		timeRemaining = countTime - Time.time;
		
		percent = timeRemaining/countTime * 100;
		if (timeRemaining < 0)
		{
			timeRemaining = 0;
			clockIsPaused = true;
			//TimeIsUp();
			Debug.Log("Time is up!");
			Destroy(this);
		}
		//ShowTime();
	}


}
