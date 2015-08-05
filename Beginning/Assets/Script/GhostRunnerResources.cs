using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Net;


public class GhostRunnerResources : MonoBehaviour
{

	public GameState gamestate;

	public int gold=0;
	public int realgold=0;
	public float besttime=0;
	public float time;
	public int numShieldItem=0;
	public int numJumpItem=0;
	public int numLifeItem=0;
	private Transform moneytf;
	private String rstext = "";
	public String datafromserver = "";
	public String idadmob="";
	public bool offlinemode = false;
	public bool login_done = false;
	public bool loaddata_ok=false;
	public bool isuseditem_ok = true;
	public bool isnotify = false;
	private int price=0;
	public bool requestserver_ok= false;
	public string challengelog="";
	public bool challengemode=false;
	public bool ischallenging = false;
	public string challengestringprocessing="";
	public string challengestringprocessing2="";
	public string challengestringprocessing3="";
	public bool savechallenge = false; 
	public bool isopponent_done=false;
	public bool iswinChallenge=false;
	public string opponentname="";
	public string opponentscore="";
	public string opponenttime="";
	public string mytime="";
	public bool getadsdone=false;

	FileInfo f; 

	void Awake()
	{
		//f = new FileInfo(Application.dataPath + "\\" + "savedata.txt");
		//f = new FileInfo(Application.persistentDataPath + "\\" + "savedata.txt");
		//if(f.Exists)
		//{
		//	LoadGame();
		//}
		FB.Init(OnInitComplete, OnHideUnity);
	}

	void Start ()
	{
		//f = new FileInfo(Application.dataPath + "\\" + "savedata.txt");
		//f = new FileInfo(Application.persistentDataPath + "\\" + "savedata.txt");
		//if(f.Exists)
		//{
		//	LoadGame();
		//}
		//realgold = gold;
		//SaveDataCloud ();
		getID ();
	}

	void Update ()
	{
		if (realgold < gold) {
			gold -= 10;
		} else {
			gold = realgold;
		}

		if (isnotify) {
			Time.timeScale = 0;
		} else 
		{
			Time.timeScale=1;
		}
	}

	void OnEnable() 
	{
		/*if (HasConnection ()) 
		{
			offlinemode=false;
		} 
		else 
		{
			offlinemode=true;
		}*/
	}

	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		if (FB.IsLoggedIn) {
			login_done = true;
			GetDataFromServer ();
		}
	}

	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}

	public void SearchingOpponent()
	{
		StartCoroutine (RequestOpponent ());
	}

	public void GetDataFromServer()
	{
		StartCoroutine (LoadData ());
	}

	public void RefeshDataFromServer()
	{
		StartCoroutine (RefeshData ());
	}

	public void SaveDataCloud()
	{
		StartCoroutine (Test ());
	}

	public void CheckWinClose()
	{
		StartCoroutine (CheckWinCloseChallenge ());
	}

	public void CallRejectRequest()
	{
		StartCoroutine (RejectRequest ());
	}

	public void CallRemoveRequest()
	{
		StartCoroutine (RemoveRequest ());
	}

	public void SaveChallenge()
	{
		StartCoroutine (SaveChallengeData ());
	}

	public void UseItemCloud(string itemname)
	{
		StartCoroutine (UseItem (itemname));
	}

	//public void CancelRequest

	public void AddItemCloud(string itemname,int gold)
	{
		price = gold;
		StartCoroutine (AddItem (itemname));
	}

	public void getID()
	{
		StartCoroutine (GetAdsID ());
	}
	
	private IEnumerator UseItem(string itemname)
	{
		var user = ParseUser.CurrentUser;
		if (user != null) 
		{
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "itemname", itemname},{ "id", FB.UserId.ToString()}
			};
			ParseCloud.CallFunctionAsync<string> ("UseItem", testparam).ContinueWith (t =>
			{
				datafromserver = t.Result;
				LoadItem();
				loaddata_ok=true;
				isuseditem_ok=true;
			});
		}
		yield break;
	}

	private IEnumerator RemoveRequest()
	{
		var user = ParseUser.CurrentUser;
		if (user != null) {
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()},{"stringtoremove",challengestringprocessing}
			};
			ParseCloud.CallFunctionAsync<string> ("RemoveRequest", testparam).ContinueWith (t =>
			                                                                                         {
				datafromserver = t.Result;
				savechallenge = true;
				LoadItem();
				loaddata_ok=true;
				ischallenging=false;
			});
		}
		yield break;
	}

	private IEnumerator CheckWinCloseChallenge()
	{
		var user = ParseUser.CurrentUser;
		if (user != null) {
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()},{"stringtoremove",challengestringprocessing}
			};
			ParseCloud.CallFunctionAsync<string> ("CheckWinCloseChallenge", testparam).ContinueWith (t =>
			{
				datafromserver = t.Result;
				savechallenge = true;
				LoadItem();
				loaddata_ok=true;
				ischallenging=false;
			});
		}
		yield break;
	}

	private IEnumerator RejectRequest()
	{
		string opponentfacebookid = "";
		string[] results = challengestringprocessing.Split ('|');
		if (results.Length >= 4) 
		{
			if (results [0] == "undefined")
				opponentfacebookid = "";
			else {
				challengestringprocessing2 = results [0];
				opponentfacebookid = results [0];
			}
		}

		var user = ParseUser.CurrentUser;
		if (user != null) {
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()},{ "opponentid", opponentfacebookid}
			};
			Debug.Log("I'm here");
			ParseCloud.CallFunctionAsync<string> ("RejectRequest", testparam).ContinueWith (t =>
			{
				datafromserver = t.Result;
				savechallenge = true;
				LoadItem();
				loaddata_ok=true;
				ischallenging=false;
			});
		}
		yield break;
	}

	private IEnumerator SaveChallengeData()
	{
		if (isopponent_done == true) //Neu doi thu da hoan thanh
		{
			string opponentfacebookid = "";
			float opponenttime = 0f;
			string[] results = challengestringprocessing.Split ('|');
			if (results.Length >= 5) 
			{
				if (results [0] == "undefined")
					opponentfacebookid = "";
				else {
					challengestringprocessing2 = results [0];
					opponentfacebookid = results [0];
				}
				if (results [4] == "undefined")
					opponenttime = 0f;
				else {
					challengestringprocessing3 = results [4];
					opponenttime = float.Parse (results [4]);
				}
			}


			var user = ParseUser.CurrentUser;
			if (user != null) {
				IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()},{ "time", gamestate.deltaTime.ToString()},{ "score", realgold.ToString()},
				{ "opponentid", opponentfacebookid},{ "opponenttime", opponenttime.ToString()}
			};
				ParseCloud.CallFunctionAsync<string> ("SaveChallengeData1", testparam).ContinueWith (t =>
				{
					datafromserver = t.Result;
					savechallenge = true;
					LoadItem();
					loaddata_ok=true;
					ischallenging=false;
					//datafromserver = t.Result;
					//LoadItem();
				
				});
			}
		}

		else if (isopponent_done == false) //Neu la loi de nghi moi
		{
			string opponentfacebookid = "";
			string[] results = challengestringprocessing.Split ('|');
			if (results.Length >= 4) 
			{
				if (results [0] == "undefined")
					opponentfacebookid = "";
				else {
					challengestringprocessing2 = results [0];
					opponentfacebookid = results [0];
				}
			}
			
			
			var user = ParseUser.CurrentUser;
			if (user != null) {
				IDictionary<string, object> testparam = new Dictionary<string, object>
				{
					{ "id", FB.UserId.ToString()},{ "time", gamestate.deltaTime.ToString()},{ "score", realgold.ToString()},
					{ "opponentid", opponentfacebookid}
				};
				ParseCloud.CallFunctionAsync<string> ("SaveChallengeData2", testparam).ContinueWith (t =>
				{
					datafromserver = t.Result;
					savechallenge = true;
					LoadItem();
					loaddata_ok=true;
					ischallenging=false;
					//datafromserver = t.Result;
					//LoadItem();
					
				});
			}
		}

		yield break;
	}

	private IEnumerator AddItem(string itemname)
	{
		var user = ParseUser.CurrentUser;
		if (user != null) 
		{
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "itemname", itemname},{ "id", FB.UserId.ToString()},{ "price", price}
			};
			ParseCloud.CallFunctionAsync<string> ("AddItem", testparam).ContinueWith (t =>
			{
				datafromserver = t.Result;
				LoadItemNotGold();
				loaddata_ok=true;
			});
		}
		yield break;
	}

	private IEnumerator RefeshData()
	{
		var user = ParseUser.CurrentUser;
		if (user != null) 
		{
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()}
			};
			ParseCloud.CallFunctionAsync<string> ("LoadData", testparam).ContinueWith (t =>
			{
				datafromserver = t.Result;
				LoadItem();
				loaddata_ok=true;
				isuseditem_ok=true;
			});
		}
		yield break;
	}

	private IEnumerator RequestOpponent()
	{
		var user = ParseUser.CurrentUser;
		if (user != null) 
		{
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()}
			};
			ParseCloud.CallFunctionAsync<string> ("SearchOpponent", testparam).ContinueWith (t =>
			{
				string datafromserver2 = t.Result;
				GetOpponentInformation(datafromserver2);
				gamestate.isfacebookclick = false;
				/*if(t.Result=="OK")
				{
					requestserver_ok=true;
					gamestate.isfacebookclick = false;
				}*/
				//LoadItem();
			});
		}
		yield break;
	}

	private IEnumerator LoadData()
	{
		var user = ParseUser.CurrentUser;
		if (user != null) 
		{
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()}
			};
			ParseCloud.CallFunctionAsync<string> ("LoadData", testparam).ContinueWith (t =>
			                                                                          {
				datafromserver = t.Result;
				isuseditem_ok=true;
				//LoadItem();
			});
		}
		yield break;
	}

	private IEnumerator Test()
	{
		var user = ParseUser.CurrentUser;
		if (user != null) 
		{
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()},{ "time", gamestate.deltaTime.ToString()},{ "score", realgold.ToString()}
			};
			ParseCloud.CallFunctionAsync<string> ("SaveData", testparam).ContinueWith (t =>
			{
				datafromserver = t.Result;
				LoadItem();
				isuseditem_ok=true;
				//loaddata_ok=true;
			});
		}
		yield break;
	}

	private IEnumerator SaveData()
	{
		var user = ParseUser.CurrentUser;
		if (user != null) 
		{
			IDictionary<string, object> testparam = new Dictionary<string, object>
			{
				{ "id", FB.UserId.ToString()},{ "time", gamestate.deltaTime.ToString()},{ "score", realgold.ToString()}
			};
			ParseCloud.CallFunctionAsync<string> ("SaveData", testparam).ContinueWith (t =>
			{
				datafromserver = t.Result;
				LoadItem();
				isuseditem_ok=true;

			});
		}
		yield break;
	}

	private IEnumerator GetAdsID()
	{
		ParseCloud.CallFunctionAsync<string> ("GetAdsID", new Dictionary<string, object>()).ContinueWith (t =>
		{
			idadmob = t.Result;

			string[] tempresults = idadmob.Split ('#');
			if (tempresults.Length >= 3) 
			{
				if (tempresults [0] == "undefined")
					gamestate.AD_UNIT_ID_TOP = "";
				else {
					gamestate.AD_UNIT_ID_TOP = tempresults [0];
				}
				if (tempresults [1] == "undefined")
					gamestate.AD_UNIT_ID_BOT = "";
				else {
					gamestate.AD_UNIT_ID_BOT = tempresults [1];
				}
				if (tempresults [2] == "undefined")
					gamestate.INTERSTITIAL_ID = "";
				else {
					gamestate.INTERSTITIAL_ID = tempresults [2];
				}
			}
			getadsdone=true;

		});
		yield break;
	}

	public void LoadItemNotGold()
	{
		string[] results = datafromserver.Split('#');
		if (results [1] == "undefined")
			numShieldItem = 0;
		else
			numShieldItem = Int32.Parse(results[1]);
		if (results [2] == "undefined")
			numJumpItem = 0;
		else
			numJumpItem = Int32.Parse(results[2]);
		if (results [3] == "undefined")
			numLifeItem = 0;
		else
			numLifeItem = Int32.Parse(results[3]);
		if (results [4] == "undefined")
			besttime = 0;
		else
			besttime = float.Parse(results[4]);
	}

	public void CheckChallengeLog()
	{

	}

	public void GetOpponentInformation(String serverresult)
	{
		string[] results = serverresult.Split('#');
		if (results [0] == "undefined") 
		{
			opponentname = "No Name";
		}
		else
		{
			opponentname = results [0].ToString();
		}
		if (results [1] == "undefined")
			opponentscore = "0.00";
		else
			opponentscore = results [1].ToString ();
		if (results [2] == "undefined") 
		{
			realgold=0;
		}
		else
		{
			realgold=Int32.Parse(results[2]);
		}
		requestserver_ok=true;
	}

	public void LoadItem()
	{
		isuseditem_ok=true;
		string[] results = datafromserver.Split('#');
		if (results [0] == "undefined") 
		{
			realgold=0;
		}
		else
		{
			realgold=Int32.Parse(results[0]);
			//gold = Int32.Parse(results[0]);
		}
		if (results [1] == "undefined")
			numShieldItem = 0;
		else
			numShieldItem = Int32.Parse(results[1]);
		if (results [2] == "undefined")
			numJumpItem = 0;
		else
			numJumpItem = Int32.Parse(results[2]);
		if (results [3] == "undefined")
			numLifeItem = 0;
		else
			numLifeItem = Int32.Parse(results[3]);
		if (results [4] == "undefined")
			besttime = 0;
		else
			besttime = float.Parse(results[4]);
		if (results [5] == "undefined")
			challengelog = "";
		else 
		{
			challengelog = results [5].ToString ();
			if(challengelog.Contains("Done")||challengelog.Contains("New")
			   ||challengelog.Contains("Win")||challengelog.Contains("Close")
			   ||challengelog.Contains("Rejected"))
			{
				challengemode = true;
			}
		}
	}

	public void Reset()
	{
		gold=0;
		realgold=0;
		numShieldItem=0;
		numJumpItem=0;
		numLifeItem=0;
	}

	public void ChangeMode(bool isOfflineMode)
	{
		offlinemode = isOfflineMode;
	}

	public bool HasConnection()
	{
		try
		{
			using (var client = new WebClient())
				using (var stream = new WebClient().OpenRead("http://www.google.com"))
			{
				return true;
			}
		}
		catch
		{
			return false;
		}
	}

	public void ChangeGold(int gold)
	{
		realgold -= gold;
	}
	
	/*public void SaveGame()
	{
		StreamWriter w;
		if(!f.Exists)
		{
			w = f.CreateText();    
		}
		else
		{
			f.Delete();
			w = f.CreateText();
		}
		w.WriteLine(realgold.ToString());
		w.WriteLine(realgold.ToString());
		w.WriteLine(numShieldItem.ToString());
		w.WriteLine(numJumpItem.ToString());
		w.WriteLine(numLifeItem.ToString());
		w.Close();
	}

	public void LoadGame()
	{
		//StreamReader r = File.OpenText(Application.dataPath + "\\" + "savedata.txt");
		StreamReader r = File.OpenText(Application.persistentDataPath + "\\" + "savedata.txt");
		realgold = IntParseFast (r.ReadLine ());
		gold = IntParseFast (r.ReadLine ());
		numShieldItem = IntParseFast (r.ReadLine ());
		numJumpItem = IntParseFast (r.ReadLine ());
		numLifeItem = IntParseFast (r.ReadLine ());
		//realgold = int.Parse(r.ReadLine ());
		//gold = int.Parse(r.ReadLine ());
		//numShieldItem = int.Parse(r.ReadLine ());
		//numJumpItem = int.Parse(r.ReadLine ());
		//numLifeItem = int.Parse(r.ReadLine ());
		r.Close();

	}

	public static int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}*/
	
}

