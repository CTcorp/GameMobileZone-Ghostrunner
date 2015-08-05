using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net;

public class LoginIcon : MonoBehaviour {
	
	
	public GameState gamestate;
	public GhostRunnerResources resources;
	public Notification notification;
	// Use this for initialization
	void Start () {
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

	void SharedCallback(FBResult result)
	{
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
	
	private IEnumerator ParseLogin() 
	{
		if (FB.IsLoggedIn) {
			var loginTask = ParseFacebookUtils.LogInAsync(FB.UserId,FB.AccessToken, DateTime.Now);
			while (!loginTask.IsCompleted) yield return null;
			if (loginTask.IsFaulted || loginTask.IsCanceled) {
				// Handle error
			} else {
				Debug.Log("Yeah");
				FB.API("/me", HttpMethod.GET, FBAPICallback);
			}
		}
	}
	
	private void FBAPICallback(FBResult result)
	{
		if (!String.IsNullOrEmpty(result.Error)) {
			// Handle error
		} else {
			// Got user profile info
			var resultObject = Json.Deserialize(result.Text) as Dictionary<string, object>;
			var userProfile = new Dictionary<string, string>();
			
			userProfile["facebookId"] = getDataValueForKey(resultObject, "id");
			userProfile["name"] = getDataValueForKey(resultObject, "name");
			object location;
			if (resultObject.TryGetValue("location", out location)) {
				userProfile["location"] = (string)(((Dictionary<string, object>)location)["name"]);
			}
			userProfile["locale"] = getDataValueForKey(resultObject, "locale");
			userProfile["gender"] = getDataValueForKey(resultObject, "gender");
			//userProfile["birthday"] = getDataValueForKey(resultObject, "birthday");
			//userProfile["relationship"] = getDataValueForKey(resultObject, "relationship_status");
			if (userProfile["facebookId"] != "") {
				userProfile["pictureURL"] = 
					"https://graph.facebook.com/" + userProfile["facebookId"] + "/picture?type=large&width=128&height=128&return_ssl_resources=1";
			}
			//userProfile["score"] = "555555";
			StartCoroutine("saveUserData", userProfile);
		}
	}
	
	private IEnumerator saveUserData(Dictionary<string, string> profile) {
		var user = ParseUser.CurrentUser;
		string temp;
		//if (profile.TryGetValue ("score", out temp)) {
		//	user ["score"] = Int32.Parse(temp);
		//}
		if (profile.TryGetValue ("facebookId", out temp)) {
			user ["facebookid"] = temp;
		}
		if (profile.TryGetValue ("location", out temp)) {
			user ["location"] = temp;
		}
		if (profile.TryGetValue ("name", out temp)) {
			user ["name"] = temp;
		}
		if (profile.TryGetValue ("gender", out temp)) {
			user ["gender"] = temp;
		}
		if (profile.TryGetValue ("pictureURL", out temp)) {
			user ["pictureURL"] = temp;
		}
		// Save if there have been any updates
		//if (user.IsKeyDirty("facebookid")) {
		var saveTask = user.SaveAsync();
		while (!saveTask.IsCompleted) yield return null;
		if (saveTask.IsCompleted) {
			resources.login_done = true;
			resources.RefeshDataFromServer();
			//notification.isDone = true;
		}
		//gamestate.stategame = GameState.StateGame.Reborn;
		//UpdateProfile();
		//}
	}
	
	private string getDataValueForKey(Dictionary<string, object> dict, string key) {
		object objectForKey;
		if (dict.TryGetValue(key, out objectForKey)) {
			return (string)objectForKey;
		} else {
			return "";
		}
	}
	
	void OnMouseDown(){
		//Screen.orientation = ScreenOrientation.Portrait;
		//FB.Logout();
		if (resources.HasConnection ()) {
			if (gamestate.isfacebookclick == false) {
				if (!FB.IsLoggedIn) {
					gamestate.isfacebookclick = true;
					FB.Login ("public_profile,email,user_friends", LoginCallback);
					//gamestate.stategame = GameState.StateGame.Reborn;
				} else {
					//gamestate.stategame = GameState.StateGame.Reborn;
					gamestate.isfacebookclick = true;
					StartCoroutine ("ParseLogin");
					/*FB.Feed(
				link: "http://s1.anh.im/2015/05/01/icon17e07.png",
				linkName: "Test thôi mà",
				linkCaption: "Test tí cho vui",
				linkDescription: "Test nào",
				picture: "http://s1.anh.im/2015/05/01/icon17e07.png",
				callback :SharedCallback
				);*/
				}
			}
		} else 
		{
			showNoConnection();
			//resources.offlinemode=true;
		}
		// this object was clicked - do something
		//Destroy (this.gameObject);
	}  

	//Check Login
	void LoginCallback(FBResult result)
	{
		if (!FB.IsLoggedIn)
		{
			gamestate.isfacebookclick = false;
			//FB.Login ("public_profile,email,user_friends", LoginCallback);
			//Screen.orientation = ScreenOrientation.LandscapeRight;
			//gamestate.stategame = GameState.StateGame.Reborn;
		}
		else
		{
			//gamestate.stategame = GameState.StateGame.Reborn;
			showNotification();
			StartCoroutine("ParseLogin");
			//Screen.orientation = ScreenOrientation.LandscapeRight;
			/*FB.Feed(
				link: "http://s1.anh.im/2015/05/01/icon17e07.png",
				linkName: "Test thôi mà",
				linkCaption: "Test tí cho vui",
				linkDescription: "Test nào",
				picture: "http://s1.anh.im/2015/05/01/icon17e07.png",
				callback :SharedCallback
				);*/
		}
	}
	void showNotification()
	{
		gamestate.laststate = "MainMenu";
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.Logging;
		notification.isDone = false;
		resources.isnotify = true;
	}

	void showNoConnection()
	{
		gamestate.isfacebookclick = true;
		gamestate.laststate = "MainMenu";
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NoConnection;
		notification.isDone = false;
		resources.isnotify = true;
	}

}
