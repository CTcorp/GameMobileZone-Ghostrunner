using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.UI;
public class AdmobObj : MonoBehaviour {


	private BannerView bannerView;
	private InterstitialAd interstitial;

	public Text log;

	
	public string AD_UNIT_ID_TOP = "ca-app-pub-2051829079190923/5626698693";
	public string AD_UNIT_ID_BOT = "ca-app-pub-2051829079190923/7103431897";
	public string INTERSTITIAL_ID = "";//"ca-app-pub-2051829079190923/8692315890";
	public GameState gamestate;



	// Use this for initialization
	void Start () {

		RequestBanner ();
		RequestInterstitial ();
	
	}

	private void RequestInterstitial()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = INTERSTITIAL_ID;
		#elif UNITY_IPHONE
		string adUnitId = INTERSTITIAL_ID;
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create an interstitial.
		interstitial = new InterstitialAd(INTERSTITIAL_ID);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;

		// Load an interstitial ad.
		AdRequest request = new AdRequest.Builder ().Build();
		interstitial.LoadAd(request);
	}

	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword("game")
				.SetGender(Gender.Male)
				.SetBirthday(new DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
		
	}


	private void RequestBanner()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = AD_UNIT_ID_BOT;
		#elif UNITY_IPHONE
		string adUnitId = AD_UNIT_ID_BOT;
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(AD_UNIT_ID_BOT, AdSize.SmartBanner, AdPosition.Bottom);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		AdRequest request = new AdRequest.Builder ().Build();
		bannerView.LoadAd(request);
	}


	
	// Update is called once per frame
	void Update () {

		switch(gamestate.stategame) {
		case GameState.StateGame.EndScreen:
			if(!gamestate.isAdsLoaded && gamestate.numLoad%10==0)
			{
				ShowInterstitial();
				gamestate.isAdsLoaded = true;
			}
			break;
		}
	}

	private void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			print("Interstitial is not ready yet.");
			log.text = "Interstitial is not ready yet.";
		}
	}
	
	#region Banner callback handlers
	
	public void HandleAdLoaded(object sender, EventArgs args)
	{
		print("HandleAdLoaded event received.");
		log.text = "HandleAdLoaded event received.";
	}
	
	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
		log.text = "HandleFailedToReceiveAd event received with message: " + args.Message;
	}
	
	public void HandleAdOpened(object sender, EventArgs args)
	{
		print("HandleAdOpened event received");
		log.text = ("HandleAdOpened event received");
	}
	
	void HandleAdClosing(object sender, EventArgs args)
	{
		print("HandleAdClosing event received");
		log.text = ("HandleAdClosing event received");
	}
	
	public void HandleAdClosed(object sender, EventArgs args)
	{
		print("HandleAdClosed event received");
		log.text = ("HandleAdClosed event received");
	}
	
	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		print("HandleAdLeftApplication event received");
		log.text = ("HandleAdLeftApplication event received");
	}
	
	#endregion
	
	#region Interstitial callback handlers
	
	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		print("HandleInterstitialLoaded event received.");
		log.text = ("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
		log.text = ("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}
	
	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		print("HandleInterstitialOpened event received");
		log.text = ("HandleInterstitialOpened event received");
	}
	
	void HandleInterstitialClosing(object sender, EventArgs args)
	{
		print("HandleInterstitialClosing event received");
		log.text = ("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		print("HandleInterstitialClosed event received");
		log.text = ("HandleInterstitialClosed event received");
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		print("HandleInterstitialLeftApplication event received");
		log.text = ("HandleInterstitialLeftApplication event received");
	}
	
	#endregion
}
