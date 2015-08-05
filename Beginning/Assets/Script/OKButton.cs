using UnityEngine;
using System.Collections;
using Parse;

public class OKButton : MonoBehaviour
{

	public GameState gamestate;
	public GhostRunnerResources resources;
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
		switch (this.transform.parent.GetComponent<Notification> ().type) 
		{

		case Notification.NotificationType.Logging:
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			resources.loaddata_ok = false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(GameState.StateGame.EndScreen);
				//gamestate.stategame = GameState.StateGame.EndScreen;
			this.transform.parent.gameObject.SetActive (false);
			break;

		case Notification.NotificationType.Logout:

			if (FB.IsInitialized && FB.IsLoggedIn) 
			{
				FB.Logout();
				if (ParseUser.CurrentUser != null)
				{
					ParseUser.LogOut();
				}
				resources.login_done=false;
			}
			resources.Reset ();
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(GameState.StateGame.EndScreen);
				//gamestate.stategame = GameState.StateGame.EndScreen;
			this.transform.parent.gameObject.SetActive (false);
			break;

		case Notification.NotificationType.NoConnection:
			
			if (FB.IsInitialized && FB.IsLoggedIn) 
			{
				FB.Logout();
				if (ParseUser.CurrentUser != null)
				{
					ParseUser.LogOut();
				}
				resources.login_done=false;
			}
			resources.Reset ();
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(GameState.StateGame.EndScreen);
				//gamestate.stategame = GameState.StateGame.EndScreen;
			else if(gamestate.laststate == "InGame")
				gamestate.ChangeState(GameState.StateGame.InGame);
				//gamestate.stategame = GameState.StateGame.InGame;
			this.transform.parent.gameObject.SetActive (false);
			//resources.offlinemode=true;
			break;

		case Notification.NotificationType.ServerNotAvaiable:
			
			if (FB.IsInitialized && FB.IsLoggedIn) 
			{
				FB.Logout();
				if (ParseUser.CurrentUser != null)
				{
					ParseUser.LogOut();
				}
				resources.login_done=false;
			}
			resources.Reset ();
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(GameState.StateGame.EndScreen);
				//gamestate.stategame = GameState.StateGame.EndScreen;
			else if(gamestate.laststate == "Shopping")
				gamestate.ChangeState(GameState.StateGame.Shopping);
				//gamestate.stategame = GameState.StateGame.Shopping;
			this.transform.parent.gameObject.SetActive (false);
			//resources.offlinemode=true;
			break;

		case Notification.NotificationType.NotEnoughMoney:

			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			this.transform.parent.gameObject.SetActive (false);
			//resources.offlinemode=true;
			break;

		case Notification.NotificationType.SearchingOpponent:
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			gamestate.isfacebookclick=false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(GameState.StateGame.EndScreen);
				//gamestate.stategame = GameState.StateGame.EndScreen;
			else if(gamestate.laststate == "Shopping")
				gamestate.ChangeState(GameState.StateGame.Shopping);
				//gamestate.stategame = GameState.StateGame.Shopping;
			resources.requestserver_ok = false;
			break;

		case Notification.NotificationType.SavingChallenge:
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			gamestate.isfacebookclick=false;
			resources.savechallenge = false;
			break;
		
		case Notification.NotificationType.ChallengeRequest_1:
			//resources.ischallenging=true;
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			gamestate.isfacebookclick=false;
			gamestate.beginTime = Time.time;
			gamestate.ChangeState(GameState.StateGame.Restart);
			//gamestate.stategame= GameState.StateGame.Restart;
			break;

		case Notification.NotificationType.ChallengeRequest_2:
			//resources.ischallenging=true;
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			gamestate.isfacebookclick=false;
			gamestate.beginTime = Time.time;
			gamestate.ChangeState(GameState.StateGame.Restart);
			//gamestate.stategame= GameState.StateGame.Restart;
			break;

		case Notification.NotificationType.ChallengeRequest_3:
			//resources.ischallenging=true;
			//resources.CallRemoveRequest();
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			gamestate.isfacebookclick=false;
			showChallegeCheck2();
			break;

		case Notification.NotificationType.CheckWinCloseChallenge:
			//resources.ischallenging=true;
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			gamestate.isfacebookclick=false;
			showChallegeCheck();
			break;

		case Notification.NotificationType.QuitGame:
			gamestate.isfacebookclick = true;
			Application.Quit();
			break;

		/*case Notification.NotificationType.Saving:
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			this.transform.parent.gameObject.SetActive (false);
			break;*/

		}
		gamestate.isfacebookclick=false;

	} 
	void showChallegeCheck2()
	{
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.SavingChallenge2;
		notification.isDone = false;
		resources.isnotify = true;
		resources.CallRemoveRequest ();
	}

	void showChallegeCheck()
	{
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.SavingChallenge2;
		notification.isDone = false;
		resources.isnotify = true;
		resources.CheckWinClose ();
	}
}

