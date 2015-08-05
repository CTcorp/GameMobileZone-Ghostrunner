using UnityEngine;
using System.Collections;
using Parse;

public class LogoutIcon : MonoBehaviour
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

	void OnMouseDown()
	{
		gamestate.isfacebookclick=true;
		showNotification ();
		/*if (FB.IsInitialized && FB.IsLoggedIn) 
		{
			FB.Logout();
			if (ParseUser.CurrentUser != null)
			{
				ParseUser.LogOut();
			}
			resources.login_done=false;
		}
		resources.Reset ();*/
	}

	void showNotification()
	{
		gamestate.laststate = "MainMenu";
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.Logout;
		resources.isnotify = true;
	}
}

