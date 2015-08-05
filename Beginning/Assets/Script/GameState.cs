using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(AdMobPlugin))]

public class GameState : MonoBehaviour {
	
	public StateGame stategame;
	public GhostRunnerResources resources;
	public Notification notification;

	public GameObject Loading;
	public GameObject Ingame;
	public GameObject MainMenu;
	public GameObject EndGameScreen;
	public GameObject Shop;
	public GameObject HighScore;
	public Text LifeTime;
	public TextMesh timeText;
	public string laststate;
	public float beginTime = 5;
	public float deltaTime = 0;
	public bool isfacebookclick=false;
	public bool isAdsLoaded = false;
	public int numLoad = 0;

	private AdMobPlugin admob;
	
	public string AD_UNIT_ID_TOP = "";//"ca-app-pub-2051829079190923/5626698693";
	public string AD_UNIT_ID_BOT = "";//"ca-app-pub-2051829079190923/7103431897";
	public string INTERSTITIAL_ID = "";//"ca-app-pub-2051829079190923/8692315890";

	void Awake()
	{

	}

	public void CallTopBanner()
	{
		admob = GetComponent<AdMobPlugin>();
		admob.CreateBanner(adUnitId: AD_UNIT_ID_TOP,
		                   adSize: AdMobPlugin.AdSize.SMART_BANNER,
		                   isTopPosition: true,
		                   interstitialId: INTERSTITIAL_ID,
		                   isTestDevice: false);
		admob.RequestAd();
		admob.ShowBanner ();
	}
	public void CallBotBanner()
	{
		admob = GetComponent<AdMobPlugin>();
		admob.CreateBanner(adUnitId: AD_UNIT_ID_BOT,
		                   adSize: AdMobPlugin.AdSize.SMART_BANNER,
		                   isTopPosition: false,
		                   interstitialId: INTERSTITIAL_ID,
		                   isTestDevice: false);
		admob.RequestAd();
		admob.ShowBanner ();
	}
	
	void OnEnable() {
		AdMobPlugin.AdClosed += () => { Debug.Log ("AdClosed"); };
		AdMobPlugin.AdFailedToLoad += () => { Debug.Log ("AdFailedToLoad"); };
		AdMobPlugin.AdLeftApplication += () => { Debug.Log ("AdLeftApplication"); };
		AdMobPlugin.AdOpened += () => { Debug.Log ("AdOpened"); };
		
		AdMobPlugin.InterstitialClosed += () => { Debug.Log ("InterstitialClosed"); };
		AdMobPlugin.InterstitialFailedToLoad += () => { Debug.Log ("InterstitialFailedToLoad"); };
		AdMobPlugin.InterstitialLeftApplication += () => { Debug.Log ("InterstitialLeftApplication"); };
		AdMobPlugin.InterstitialOpened += () => { Debug.Log ("InterstitialOpened"); };
		
		AdMobPlugin.AdLoaded += HandleAdLoaded;
		AdMobPlugin.InterstitialLoaded += HandleInterstitialLoaded;
	}
	
	void OnDisable() {
		AdMobPlugin.AdLoaded -= HandleAdLoaded;
		AdMobPlugin.InterstitialLoaded -= HandleInterstitialLoaded;
	}
	
	void HandleAdLoaded() {
		#if !UNITY_EDITOR
		admob.ShowBanner();
		#endif
	}
	
	void HandleInterstitialLoaded() {
		#if !UNITY_EDITOR
		admob.ShowInterstitial();
		#endif
	}
	
	void CallInterstitial()
	{
		admob.RequestInterstitial ();
		admob.ShowInterstitial ();
	}
	
	
	
	public enum StateGame
	{
		Loading,
		MainMenu,
		InGame,
		Dying,
		EndScreen,
		Shopping,
		HighScore,
		Reborn,
		Notify,
		Restart
	}
	
	// Use this for initialization
	void Start () {
		ChangeState (StateGame.Loading);
		//stategame = StateGame.Loading;
		laststate = "EndGame";
		//CallTopBanner ();
		//CallBotBanner ();
	}
	
	// Update is called once per frame
	void Update () {

		if (resources.getadsdone) 
		{
			//CallBotBanner();
			resources.getadsdone=false;
		}
		if (resources.challengemode == true&&resources.ischallenging==false) 
		{
			string[] results = resources.challengelog.Split('*');
			for(int i=0;i<results.Length;i++)
			{
				if(results[i].Contains("Win")||results[i].Contains("Close"))
				{

					string[] tempresults = results[i].Split ('|');
					if (tempresults.Length >= 6) 
					{
						if (tempresults [1] == "undefined")
							resources.opponentname = "";
						else {
							resources.opponentname = tempresults [1];
						}
						if (tempresults [4] == "undefined")
							resources.opponentscore = "0.00";
						else {
							resources.mytime = tempresults [4];
						}
						if (tempresults [5] == "undefined")
							resources.opponenttime = "0.00";
						else {
							resources.opponenttime = tempresults [5];
						}
					}

					if(results[i].Contains("Win"))
					{
						resources.iswinChallenge=true;
					}
					else if(results[i].Contains("Close"))
					{
						resources.iswinChallenge=false;
					}
					resources.challengestringprocessing=results[i];
					showCheckWinCloseChallenge();
					resources.ischallenging=true;
					break;
				}

				if(results[i].Contains("Rejected"))
				{
					string[] tempresults = results[i].Split ('|');
					if (tempresults.Length >= 4) 
					{
						if (tempresults [1] == "undefined")
							resources.opponentname = "";
						else {
							resources.opponentname = tempresults [1];
						}
						if (tempresults [2] == "undefined")
							resources.opponentscore = "0.00";
						else {
							resources.opponentscore = tempresults [2];
						}
					}
					//resources.isopponent_done = false;
					resources.challengestringprocessing=results[i];
					resources.ischallenging=true;
					showChallengeDialog3();
				}

				if(results[i].Contains("Done")||results[i].Contains("New"))
				{


					if(results[i].Contains("Done"))
					{
						string[] tempresults = results[i].Split ('|');
						if (tempresults.Length >= 5) 
						{
							if (tempresults [1] == "undefined")
								resources.opponentname = "";
							else {
								resources.opponentname = tempresults [1];
							}
							if (tempresults [2] == "undefined")
								resources.opponentscore = "0.00";
							else {
								resources.opponentscore = tempresults [2];
							}
							if (tempresults [4] == "undefined")
								resources.opponenttime = "0.00";
							else {
								resources.opponenttime = tempresults [4];
							}
						}
						resources.isopponent_done = true;
						showChallengeDialog2();
					}

					else if(results[i].Contains("New"))
					{
						string[] tempresults = results[i].Split ('|');
						if (tempresults.Length >= 4) 
						{
							if (tempresults [1] == "undefined")
								resources.opponentname = "";
							else {
								resources.opponentname = tempresults [1];
							}
							if (tempresults [2] == "undefined")
								resources.opponentscore = "0.00";
							else {
								resources.opponentscore = tempresults [2];
							}
						}
						resources.isopponent_done = false;
						showChallengeDialog1();
					}
					resources.challengestringprocessing=results[i];
					resources.ischallenging=true;
					break;
				}
			}
		}

		/*switch (stategame) 
		{
		case StateGame.Loading:
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(true);
			break;
		case StateGame.MainMenu:
			MainMenu.gameObject.SetActive(true);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			break;
		case StateGame.InGame:
			isAdsLoaded = false;
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			deltaTime = Time.time - beginTime;
			
			break;
		case StateGame.EndScreen:
			if(!isAdsLoaded && numLoad%10==0)
				CallInterstitial();


			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(true);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			beginTime = Time.time;
			isAdsLoaded = true;
			numLoad++;
			break;
		case StateGame.Shopping:
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(true);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			//beginTime = Time.time;
			break;
		case StateGame.HighScore:
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(true);
			Loading.gameObject.SetActive(false);
			//beginTime = Time.time;
			break;
		case StateGame.Notify:
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			//beginTime = Time.time;
			break;
		}*/
		
		/*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began&& stategame != GameState.StateGame.EndScreen) 
		{
			if(stategame == GameState.StateGame.MainMenu&&!isfacebookclick)
			{
				stategame = GameState.StateGame.InGame;
				
				//beginTime = Time.realtimeSinceStartup;
				beginTime = Time.time;
				
			}
			
		}*/
		if (Input.GetMouseButtonDown (0)&& stategame != GameState.StateGame.EndScreen) 
		{
			if(stategame == GameState.StateGame.MainMenu&&!isfacebookclick&&!resources.isnotify)
			{
				ChangeState(StateGame.InGame);
				//stategame = GameState.StateGame.InGame;
				
				//beginTime = Time.realtimeSinceStartup;
				beginTime = Time.time;
				
				
			}
		}
		
		
		if (Input.GetKeyDown (KeyCode.Space)&& stategame != GameState.StateGame.EndScreen) 
		{	
			if(stategame == GameState.StateGame.MainMenu)
			{
				
				beginTime = Time.time;
				ChangeState(StateGame.InGame);
				//stategame = GameState.StateGame.InGame;
				
				//Debug.Log("press space");
				
				
				
			}
			
		}

		// Xử lý key
		if (Input.GetKeyDown(KeyCode.Escape))//Back Button
		{
			showQuitGameDialog();
			//Application.Quit(); 
		}



		LifeTime.text = Math.Truncate (deltaTime) + ":" +deltaTime.ToString ("0.00").Split ('.') [1];
		timeText.text = Math.Truncate (deltaTime) + ":" +deltaTime.ToString ("0.00").Split ('.') [1];
	}

	public void ChangeState(StateGame state)
	{
		this.stategame = state;

		switch (this.stategame) 
		{
		case StateGame.Loading:
			Ingame.gameObject.SetActive(false);
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(true);
			break;
		case StateGame.MainMenu:
			Ingame.gameObject.SetActive(true);
			MainMenu.gameObject.SetActive(true);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			break;
		case StateGame.InGame:
			isAdsLoaded = false;
			Ingame.gameObject.SetActive(true);
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			deltaTime = Time.time - beginTime;
			
			break;
		case StateGame.EndScreen:
			Ingame.gameObject.SetActive(true);
			if(!isAdsLoaded && numLoad%10==0)
				//CallInterstitial();
			
			
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(true);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			beginTime = Time.time;
			isAdsLoaded = true;
			numLoad++;
			break;
		case StateGame.Shopping:
			Ingame.gameObject.SetActive(true);
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(true);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			//beginTime = Time.time;
			break;
		case StateGame.HighScore:
			Ingame.gameObject.SetActive(true);
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(true);
			Loading.gameObject.SetActive(false);
			//beginTime = Time.time;
			break;
		case StateGame.Notify:
			//Ingame.gameObject.SetActive(true);
			MainMenu.gameObject.SetActive(false);
			EndGameScreen.gameObject.SetActive(false);
			Shop.gameObject.SetActive(false);
			HighScore.gameObject.SetActive(false);
			Loading.gameObject.SetActive(false);
			//beginTime = Time.time;
			break;
		}
	}

	void showQuitGameDialog()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.QuitGame;
		notification.isDone = false;
		resources.isnotify = true;
	}

	void showChallengeDialog1()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.ChallengeRequest_1;
		notification.isDone = false;
		resources.isnotify = true;
	}

	void showChallengeDialog2()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.ChallengeRequest_2;
		notification.isDone = false;
		resources.isnotify = true;
	}

	void showChallengeDialog3()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.ChallengeRequest_3;
		notification.isDone = false;
		resources.isnotify = true;
	}

	void showCheckWinCloseChallenge()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.CheckWinCloseChallenge;
		notification.isDone = false;
		resources.isnotify = true;
	}



}
