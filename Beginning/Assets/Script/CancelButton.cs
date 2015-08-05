using UnityEngine;
using System.Collections;

public class CancelButton : MonoBehaviour
{

	public GameState gamestate;
	public GhostRunnerResources resources;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDown(){

		/*switch (this.transform.parent.GetComponent<Notification> ().type) 
		{
			
		case Notification.NotificationType.QuitGame:
			gamestate.isfacebookclick = true;
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			//resources.offlinemode=true;
			break;
			
		}
		gamestate.isfacebookclick=false;*/
		
	}


	void OnMouseUp(){
		
		switch (this.transform.parent.GetComponent<Notification> ().type) 
		{

		case Notification.NotificationType.ChallengeRequest_1:
			gamestate.isfacebookclick = true;
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			resources.CallRejectRequest();
			break;

		case Notification.NotificationType.QuitGame:
			gamestate.isfacebookclick = true;
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			this.transform.parent.gameObject.SetActive (false);
			resources.isnotify = false;
			//resources.offlinemode=true;
			break;
			
			/*case Notification.NotificationType.Saving:
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			this.transform.parent.gameObject.SetActive (false);
			break;*/
			
		}
		gamestate.isfacebookclick=false;
		
	}
}

