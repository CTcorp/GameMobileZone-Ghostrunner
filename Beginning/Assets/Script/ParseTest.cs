using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ParseTest : MonoBehaviour {

	// Use this for initialization
	//TextMesh tm;
	string playerName;
	ParseObject gameScore;
	int i=0;
	float time =0;
	float delay =1;
	void Start () {
		//tm = GetComponent<TextMesh> ();
		/*gameScore = new ParseObject("GameScore");
		gameScore["playerid"] = "15698";
		gameScore["playerName"] = "Sean Plott";
		gameScore["score"] = 1337;
		Task saveTask = gameScore.SaveAsync();*/

		/*ParseUser user = new ParseUser()
		{
			Username = "myname2",
			Password = "mypass",
			Email = "email2@example.com"
		};*/
		
		// other fields can be set just like with ParseObject
		//user["phone"] = "415-392-0202";
		
		/*try
		{
			user.SignUpAsync().ContinueWith(t =>
			                                     {
				if (t.IsFaulted || t.IsCanceled)
				{
					// The login failed. Check t.Exception to see why.
					Debug.Log("Parse Registration failed!");
					playerName = "Parse Registration failed!";
				}
				else
				{
					// Login was successful.
					Debug.Log("Parse Registration was successful!");
					playerName = "Parse Registration was successful!";
				}
			});
		}
		catch (System.Exception e)
		{
			Debug.Log("Failed to sign up Parse User. Reason: " + e.Message);
			playerName = "Failed to sign up Parse User. Reason: " + e.Message;
			throw;
		}*/

		/*ParseQuery<ParseObject> query = ParseObject.GetQuery("GameScore").WhereEqualTo("playerid","15697");
		query.FirstAsync().ContinueWith(t =>
		{
			gameScore = t.Result;
			//int score = gameScore.Get<int>("score");
			//playerName = gameScore.Get<string>("playerName");
			//bool cheatMode = gameScore.Get<bool>("cheatMode");
		});*/

	}
	
	// Update is called once per frame
	void Update () {
		/*gameScore.FetchAsync().ContinueWith(t=>
		{
			playerName = t.Result.Get<string>("playerName");
			gameScore ["playerName"] = "Mr Boo " + i;
			gameScore.SaveAsync ();

		});*/

		/*ParseQuery<ParseObject> query = ParseObject.GetQuery("GameScore").WhereEqualTo("playerid","15697");
		query.FirstAsync().ContinueWith(t =>
		                                {
			playerName = t.Result.Get<string>("playerName");


			//int score = gameScore.Get<int>("score");
			//playerName = gameScore.Get<string>("playerName");
			//bool cheatMode = gameScore.Get<bool>("cheatMode");
		});*/
		//tm.text = playerName;
	}
}
