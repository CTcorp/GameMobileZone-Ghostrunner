using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FreeGold : MonoBehaviour
{

	public GhostRunnerResources resources;
	public GameState gamestate;
	public Notification notification;
	public GUITexture videoZoneState = null;

	private int currencyAmount = 0;
	
	private string appVersion = "1.0";
	private string appId = "app5b2ac6e21037417ab5";
	private string zoneId = "vz983159b4a456435f9b";
	public Text mess;

	public void Initialize()
	{
		// Assign any AdColony Delegates before calling Configure
		AdColony.OnVideoFinished = this.OnVideoFinished;
		AdColony.OnV4VCResult = this.OnV4VCResult;
		
		// If you wish to use a the customID feature, you should call  that now.
		// Then, configure AdColony:
		AdColony.Configure
			(
				"version:1.0,store:google", // Arbitrary app version and Android app store declaration.
				appId,   // ADC App ID from adcolony.com
				zoneId // A zone ID from adcolony.com
				//"vzf8fb4670a60e4a139d01b5", // Any number of additional Zone IDS
				//"vz1fd5a8b2bf6841a0a4b826"
				);
		mess.text = "initializing";
	}

	private void OnVideoFinished(bool ad_was_shown)
	{
		mess.text=("On Video Finished");
		resources.realgold += 100;
		// Resume your app here.
		// Add notify here

	}
	
	// The V4VCResult Delegate assigned in Initialize -- AdColony calls this after confirming V4VC transactions with your server
	// success - true: transaction completed, virtual currency awarded by your server - false: transaction failed, no virtual currency awarded
	// name - The name of your virtual currency, defined in your AdColony account
	// amount - The amount of virtual currency awarded for watching the video, defined in your AdColony account
	private void OnV4VCResult(bool success, string name, int amount)
	{
		if(success)
		{
			mess.text=("V4VC SUCCESS: name = " + name + ", amount = " + amount);
			resources.realgold += amount;
		}
		else
		{
			mess.text=("V4VC FAILED!");
		}
	}

	public void PlayAVideo( string zoneID )
	{
		// Check to see if a video is available in the zone.
		if(AdColony.IsVideoAvailable(zoneID))
		{
			mess.text=("Play AdColony Video");
			// Call AdColony.ShowVideoAd with that zone to play an interstitial video.
			// Note that you should also pause your game here (audio, etc.) AdColony will not
			// pause your app for you.
			AdColony.ShowVideoAd(zoneID); 
		}
		else
		{
			mess.text=("Video Not Available");
			// Add Notify here 

		}
	}

	public void PlayV4VCAd( string zoneID, bool prePopup, bool postPopup )
	{
		// Check to see if a video for V4VC is available in the zone.
		if(AdColony.IsV4VCAvailable(zoneID))
		{
			mess.text=("Play AdColony V4VC Ad");
			// The AdColony class exposes two methods for showing V4VC Ads.
			// ---------------------------------------
			// The first `ShowV4VC`, plays a V4VC Ad and, optionally, displays
			// a popup when the video is finished.
			// ---------------------------------------
			// The second is `OfferV4VC`, which popups a confirmation before
			// playing the ad and, optionally, displays popup when the video 
			// is finished.
			
			// Call one of the V4VC Video methods:
			// Note that you should also pause your game here (audio, etc.) AdColony will not
			// pause your app for you.
			if (prePopup)
			{
				AdColony.OfferV4VC( postPopup, zoneID );
			}
			else
			{
				AdColony.ShowV4VC( postPopup, zoneID );
			}
		}
		else
		{
			mess.text=("V4VC Ad Not Available");
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		Initialize ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	
	void OnMouseDown(){
		gamestate.isfacebookclick = true;
	} 
	
	void OnMouseUp(){
		if(resources.HasConnection())
		{
			Debug.Log("Do something here ( Adcolony,...)");
			//Do Something here Tu Kun
			//PlayV4VCAd(zoneId,true,true);
			PlayAVideo(zoneId);
		}
		else
		{
			showNoConnection();
		}
		gamestate.isfacebookclick = false;
	} 

	void showNoConnection()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NoConnection;
		notification.isDone = false;
		resources.isnotify = true;
	}
}

