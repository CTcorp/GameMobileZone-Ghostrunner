using UnityEngine;
using System.Collections;

public class ChallengeIcon : MonoBehaviour
{
	public GhostRunnerResources resources;
	public GameState gamestate;
	public Notification notification;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDown(){
		gamestate.isfacebookclick = true;
	} 

	void OnMouseUp(){
		if(FB.IsLoggedIn)
		{
			if(resources.HasConnection())
			{
				showWaitingNotification();
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
		gamestate.isfacebookclick = false;
	} 


	void showWaitingNotification()
	{
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.SearchingOpponent;
		notification.isDone = false;
		resources.isnotify = true;
		if (gamestate.isfacebookclick == false) 
		{
			gamestate.isfacebookclick = true;
		}
		resources.SearchingOpponent ();
		gamestate.laststate = "MainMenu";
	}
	
	void showNoConnection()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NoConnection;
		notification.isDone = false;
		resources.isnotify = true;
	}


}

