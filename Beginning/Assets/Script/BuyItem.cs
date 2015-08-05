using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System;

public class BuyItem : MonoBehaviour
{
	// Use this for initialization
	public int price;
	public Notification notification;
	public GhostRunnerResources resources;

	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnMouseDown(){
		transform.parent.parent.GetComponent<Test_MotherItem> ().ChangeGold (price);
		showNotification ();

	} 

	void showNotification()
	{
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.Saving;
		notification.isDone = false;
		resources.isnotify = true;
		StartCoroutine(saveUserData(resources.realgold));
	}

	private IEnumerator saveUserData(int score) 
	{
		if (FB.IsLoggedIn) 
		{
			var user = ParseUser.CurrentUser;
			user ["score"] = score;
			var saveTask = user.SaveAsync ();
			while (!saveTask.IsCompleted)
				yield return null;
			if (saveTask.IsCompleted) {
				resources.login_done = true;
				resources.RefeshDataFromServer ();
				//notification.isDone = true;
			}
			//gamestate.stategame = GameState.StateGame.Reborn;
			//UpdateProfile();
			//}
		}
	}
}

