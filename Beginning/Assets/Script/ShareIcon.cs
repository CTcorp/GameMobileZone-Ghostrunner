using UnityEngine;
using System.Collections;

public class ShareIcon : MonoBehaviour {


	public GameState gamestate;
	public GhostRunnerResources resources;
	public Notification notification;


	void Start () {
		//CallFBInit ();
		//if (FB.IsLoggedIn) 
		//{
		//	FB.Logout ();
		//}
	}
	
	
	
	// Update is called once per frame
	void Update () {
	}
	
	
	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	public void LogIn()
	{
		FB.Login("public_profile,email,user_friends", LoginCallback);
	}
	
	//Check Login
	void LoginCallback(FBResult result)
	{
		if (!FB.IsLoggedIn) {
			FB.Login ("public_profile,email,user_friends", LoginCallback);
		} else {
			FB.AppRequest(
				message: "Let's eat and be prosperous!",
				callback :InvitedCallback
				);
		}

	}
	void InvitedCallback(FBResult result)
	{
		Debug.Log (result.Text);
		//Screen.orientation = ScreenOrientation.LandscapeLeft;
		if(result.Error != null)
		{
			//Debug.LogError("OnActionShared: Error: " + result.Error);
		}
		
		if (result == null || result.Error != null)
		{
			//Do something request failed
			
		}
		else
		{
			var responseObject = Facebook.MiniJSON.Json.Deserialize(result.Text) as System.Collections.Generic.Dictionary<string, object>;
			object obj = 0;
			if (responseObject == null || responseObject.Count <= 0 || responseObject.TryGetValue("cancelled", out obj))
			{
				//Debug.LogWarning("Request cancelled");
				//Do something when user cancelled
			}
			else if (responseObject.TryGetValue("id", out obj) || responseObject.TryGetValue("post_id", out obj))
			{
				//Debug.LogWarning("Request Send");
				//Do something it is succeeded
			}
		}
		
	}
	void OnMouseDown(){
		//Screen.orientation = ScreenOrientation.Portrait;
		//FB.Logout();

		if (resources.HasConnection ()) 
		{
			if (!FB.IsLoggedIn) 
			{
				FB.Login ("public_profile,email,user_friends", LoginCallback);
			} else 
			{
				FB.AppRequest(
					message: "Let's eat and be prosperous!",
					callback :InvitedCallback
					);
			}

		} 
		else 
		{
			showNoConnection();
		}
	} 

	void Share()
	{
		//TakeScreenshot ();
		//Destroy (this.gameObject);
		/*FB.Feed(
			link: "http://s1.anh.im/2015/05/01/icon17e07.png",
			linkName: "Test thôi mà",
			linkCaption: "Test tí cho vui",
			linkDescription: "Test nào",
			picture: "http://s1.anh.im/2015/05/01/icon17e07.png"
			);*/
		/*if(FB.IsLoggedIn)
		{
			FB.AppRequest(
				message: "Let's eat and be prosperous!",
				title: "Let's eat and be prosperous!"
				);
		}*/

		

	}


	private IEnumerator TakeScreenshot() 
	{
		yield return new WaitForEndOfFrame();
		
		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] screenshot = tex.EncodeToPNG();
		
		var wwwForm = new WWWForm();
		wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
		
		FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
	}

	void Callback(FBResult result)
	{
		
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
