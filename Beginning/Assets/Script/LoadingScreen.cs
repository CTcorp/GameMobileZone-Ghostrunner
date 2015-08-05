using UnityEngine;
using System.Collections;
using Facebook;

public class LoadingScreen : MonoBehaviour
{

	public GhostRunnerResources resources; //Use to check loading
	public GameState gamestate;
	public int percent =0;
	private bool checkingadsid_ok = false;
	private bool checkingloginfacebook_ok = false;
	private bool loadingdata_ok = false;
	public TextMesh percentText;
	public bool thereIsConnection = false;
	public bool checkinginternetisdone=false;
	public bool gamestarted = false;
	private float delay = 30f;
	private float timeForNextEvent=0f;
	// Use this for initialization
	void Start ()
	{
		percentText.text = "Checking internet connection....";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timeForNextEvent == 0f) 
		{
			if(resources.HasConnection())
				timeForNextEvent = Time.time + delay;
			else
			{
				timeForNextEvent = Time.time - delay;// To start offline game
			}
		}
		if (timeForNextEvent >= Time.time) 
		{
			if (resources.idadmob != "") 
			{
				thereIsConnection = true;
				checkinginternetisdone = true;
			}
		} 
		else if(timeForNextEvent < Time.time &&!checkinginternetisdone)
		{
			StartCoroutine (StartOfflineGame ());
			gamestarted = true;
			checkinginternetisdone = true;
			resources.ChangeMode(true);
		}
		if (!gamestarted) 
		{
			if (checkinginternetisdone && thereIsConnection) 
			{
				if (!checkingadsid_ok) 
				{
					if (resources.idadmob != "") 
					{
						checkingadsid_ok = true;
						percent += 23;
						percentText.text = "Checking internet connection....OK";
					}
				} 
				else 
				{
					if (!loadingdata_ok) 
					{
						percentText.text = "Checking account logged in....";
						if (FB.IsInitialized) 
						{
							if (FB.IsLoggedIn) 
							{
								percentText.text = "Checking account logged in....OK";
								percentText.text = "Loading data....";
								if (!checkingloginfacebook_ok) 
								{
									checkingloginfacebook_ok = true;
									percent += 35;
								}
								//Load Data
								if (resources.datafromserver != "") 
								{
									resources.LoadItem();
									percentText.text = "Loading data....OK";
									loadingdata_ok = true;
									percent += 42;
									StartCoroutine (StartGame ());
									resources.ChangeMode(false);
									gamestarted = true;
								}
							} 
							else 
							{
								percentText.text = "You're not logged in yet. Please login to save your data...";
								loadingdata_ok = true;
								percent += 77;
								StartCoroutine (StartGame ());
								resources.ChangeMode(false);
								gamestarted = true;
							}
						}
					}
				}
			}
		}

		//percentText.text = percent.ToString () + "%";
	}

	IEnumerator StartGame(){
		yield return new WaitForSeconds(1);
		percentText.text = "Starting game....";
		yield return new WaitForSeconds(2);
		resources.ChangeMode (false);
		gamestate.ChangeState(GameState.StateGame.MainMenu);
		//gamestate.stategame = GameState.StateGame.MainMenu;
		yield break;
	}

	IEnumerator StartOfflineGame(){
		gamestarted = true;
		percentText.text = "Failed connect to the server. Please check your connection...";
		yield return new WaitForSeconds(2);
		percentText.text = "Starting game in offline mode....";
		yield return new WaitForSeconds(2);
		resources.ChangeMode (true);
		gamestate.ChangeState(GameState.StateGame.MainMenu);
		//gamestate.stategame = GameState.StateGame.MainMenu;
		yield break;
	}

	IEnumerator TestConnection()
	{
		if (timeForNextEvent == 0f)
			timeForNextEvent = Time.time + delay;
		while (true) 
		{
			if (timeForNextEvent >= Time.time) 
			{
				if (resources.idadmob != "") 
				{
					thereIsConnection = true;
					checkinginternetisdone = true;
					yield return null;
				}
			} 
			else
			{
				StartCoroutine (StartOfflineGame ());
				gamestarted = true;
				yield return null;
			}
		}

		/*var www = new WWW ("www.google.com");
		yield return www;

		if (www.error != null&&www.isDone&&www.bytesDownloaded==0)
		{

			//StartCoroutine (StartOfflineGame ());
			//gamestarted = true;
		}
		else
		{
			percentText.text = "I'm here";
			thereIsConnection = true;
			checkinginternetisdone = true;
		}*/


		/*float timeTaken = 0.0F;
		float maxTime = 2.0F;
		bool tested = false;
		Ping testPing = new Ping( "8.8.8.8" );// ping google.com
		while(!tested)
		{
			timeTaken = 0.0F;
			while ( !testPing.isDone )
			{
				
				timeTaken += Time.deltaTime;
				
				
				if ( timeTaken > maxTime )
				{
					// if time has exceeded the max
					// time, break out and return false
					thereIsConnection = false;
					checkinginternetisdone = true;
					tested = true;
					break;
				}
				
				yield return null;
			}
			if ( timeTaken <= maxTime )
			{
				thereIsConnection = true;
				checkinginternetisdone = true;
				tested = true;
			}
			yield return null;
		}*/
	}
}

