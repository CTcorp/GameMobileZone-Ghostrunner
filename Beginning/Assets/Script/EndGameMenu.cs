using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System;

public class EndGameMenu : MonoBehaviour
{

	public GhostRunnerResources resources;
	public GameState gamestate;
	public Notification notification;
	public GameObject Invite;
	public GameObject Share;
	public GameObject Rate;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnEnable()
	{
		//StartCoroutine(saveUserData(resources.realgold));
		if(FB.IsLoggedIn)
		{
			if(resources.HasConnection())
			{
				if(!resources.ischallenging)
					showSaveNotification();
				else 
				{
					showChallegeFinish();
				}
			}
			else
			{
				showNoConnection();
			}
		}
		else
		{
			//Don't has event
		}
		this.transform.Find ("YourTime").GetComponent<TextMesh> ().text = Math.Truncate (gamestate.deltaTime) + ":" +gamestate.deltaTime.ToString ("0.00").Split ('.') [1];
		this.transform.Find ("BestTime").GetComponent<TextMesh> ().text = Math.Truncate (resources.besttime) + ":" +resources.besttime.ToString ("0.00").Split ('.') [1];
	}

	void showSaveNotification()
	{
		//notification.gameObject.SetActive(true);
		//notification.type = Notification.NotificationType.Saving;
		//notification.isDone = false;
		//resources.isnotify = true;
		resources.SaveDataCloud();
		//StartCoroutine(saveUserData());
	}

	void showChallegeFinish()
	{
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.SavingChallenge;
		notification.isDone = false;
		resources.isnotify = true;
		resources.SaveChallenge ();
	}
	
	void showNoConnection()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NoConnection;
		notification.isDone = false;
		resources.isnotify = true;
	}


	private IEnumerator saveUserData() 
	{
		resources.SaveDataCloud();
			/*var user = ParseUser.CurrentUser;
			user ["time"] = gamestate.deltaTime;
			user ["score"] = score;
			var saveTask = user.SaveAsync ();
			while (!saveTask.IsCompleted)
				yield return null;
			if (saveTask.IsCompleted) {
				resources.login_done = true;
				resources.RefeshDataFromServer ();
				//notification.isDone = true;
			}*/
			//gamestate.stategame = GameState.StateGame.Reborn;
			//UpdateProfile();
			//}
		yield return null;
	}
}

