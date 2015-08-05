using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class Demo : MonoBehaviour {
	public GameObject ScoreEntryPanel;
	public GameObject ScoreScrollList;
	String rstext = "";
	void Start () {
		//FB.Init(OnInitComplete, OnHideUnity);
		//StartCoroutine( GetScore());
	}


	private IEnumerator GetScore()
	{
		ParseCloud.CallFunctionAsync<string>("GetScore", new Dictionary<string, object>()).ContinueWith(t =>
		{
			rstext = t.Result;
			//QueryScore();
		});
		yield break;
	}
	
	void Update () {
	}

	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		//Check when login complete
		//Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		//Debug.Log("Is game showing? " + isGameShown);
	}

	// Login before use function
	public void CallFBLogin()
	{
		FB.Logout ();
		FB.Login("user_about_me, user_relationships, user_birthday, user_location", LoginCallback);
	}

	void LoginCallback(FBResult result)
	{
		if(FB.IsLoggedIn) {
			StartCoroutine("ParseLogin");
		} else {
			//Debug.Log ("FBLoginCallback: User canceled login");
		}
	}

	private IEnumerator ParseLogin() {
		if (FB.IsLoggedIn) {
			var loginTask = ParseFacebookUtils.LogInAsync(FB.UserId,FB.AccessToken, DateTime.Now);
			while (!loginTask.IsCompleted) yield return null;
			if (loginTask.IsFaulted || loginTask.IsCanceled) {
				// Handle error
			} else {
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
			userProfile["score"] = "333333";
			StartCoroutine("saveUserData", userProfile);
		}
	}

	private string getDataValueForKey(Dictionary<string, object> dict, string key) {
		object objectForKey;
		if (dict.TryGetValue(key, out objectForKey)) {
			return (string)objectForKey;
		} else {
			return "";
		}
	}

	private IEnumerator saveUserData(Dictionary<string, string> profile) {
		var user = ParseUser.CurrentUser;
		string temp;
		if (profile.TryGetValue ("score", out temp)) {
			user ["score"] = Int32.Parse(temp);
		}
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
		//}
	}

	/*Joe Amigcecjebaa Narayanansen+undefined+https://graph.facebook.com/106573173010526/picture?type=large&width=128&height=128&return_ssl_resources=1+5555*
	 Đoàn Minh Chính+Ho Chi Minh City, Vietnam+https://graph.facebook.com/822403307808270/picture?type=large&width=128&height=128&return_ssl_resources=1+5555*
	 Đoàn Minh Chính+undefined+https://graph.facebook.com/602686439834954/picture?type=large&width=128&height=128&return_ssl_resources=1+5555*
	*/
	public void QueryScore()
	{
		//StartCoroutine( PayPoints());
		//string tempstring = result;
		string[] results = rstext.Split('#');
		var topbestavatar = new Dictionary<string, string>();
		foreach (string word in results)
		{
			Debug.Log(word);
		}
		for(int i=0;i<results.Length;i++)
		{
			string[] result = results[i].Split('$');
			GameObject ScorePanel;
			ScorePanel = Instantiate (ScoreEntryPanel) as GameObject;
			ScorePanel.transform.parent = ScoreScrollList.transform;
			Transform ThisName = ScorePanel.transform.Find ("Name");
			Transform ThisScore = ScorePanel.transform.Find ("Score");
			Transform ThisAvatar = ScorePanel.transform.Find ("Avatar");
			Text ScoreName = ThisName.GetComponent<Text> ();
			Text ScoreScore = ThisScore.GetComponent<Text> ();
			Image ScoreAvatar = ThisAvatar.GetComponent<Image> ();
			ScoreName.text = result[1];
			ScoreScore.text = result[3];
			FB.API (Util.GetPictureURL(result[0],128,128),HttpMethod.GET,delegate(FBResult pictureResult)
			{
				if(pictureResult.Error!=null)
				{
					Debug.Log("Error!");
				}
				else
				{
					ScoreAvatar.sprite = Sprite.Create (pictureResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));
				}
			});
		}
	}
}
