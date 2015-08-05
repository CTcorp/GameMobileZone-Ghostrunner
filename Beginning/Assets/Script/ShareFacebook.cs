using UnityEngine;
using System.Collections;

public class ShareFacebook : MonoBehaviour {
	
	// Your app’s unique identifier.
	public string AppID = "1399925576886522";
	
	// The link attached to this post.
	public string Link = "https://play.google.com/store/apps/developer?id=Shukerullah";
	
	// The URL of a picture attached to this post. The picture must be at least 200px by 200px.
	public string Picture = "http://imageshack.us/scaled/landing/843/gh4o.png";
	
	// The name of the link attachment.
	public string Name = "My New Score";
	
	// The caption of the link (appears beneath the link name).
	public string Caption = "I just got +99 score friends! Can you beat it?";
	
	// The description of the link (appears beneath the link caption). 
	public string Description = "Enjoy fun, free games! Challenge yourself or share with friends. Fun and easy-to-use game.";
	
	void OnGUI () {
		float x = (Screen.width/2)-50;
		float y = (Screen.height/2)-25;
		
		if(GUI.Button(new Rect( x, y, 100, 50), "Share Score")) {
			ShareScoreOnFB();
		}
	}
	
	
	void ShareScoreOnFB(){
		Application.OpenURL("https://www.facebook.com/dialog/feed?"+
		                    "app_id="+AppID+
		                    "&link="+Link+
		                    "&picture="+Picture+
		                    "&name="+ReplaceSpace(Name)+
		                    "&caption="+ReplaceSpace(Caption)+
		                    "&description="+ReplaceSpace(Description)+
		                    "&redirect_uri=https://facebook.com/");
	}
	
	string ReplaceSpace (string val) {
		return val.Replace(" ", "%20");    
	}
}