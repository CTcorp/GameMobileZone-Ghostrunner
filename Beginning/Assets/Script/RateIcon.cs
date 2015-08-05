using UnityEngine;
using System.Collections;

public class RateIcon : MonoBehaviour {


	public GameState gamestate;
	public GhostRunnerResources resources;
	public Notification notification;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (resources.HasConnection ()) 
		{
			Application.OpenURL("market://details?id=com.ketchapp.stickhero");
		} else 
		{
			showNoConnection();
		}
		//Destroy (this.gameObject);

	}  

	void showNoConnection()
	{
		gamestate.laststate = "EndGame";
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NoConnection;
		notification.isDone = false;
		resources.isnotify = true;
	}
}
