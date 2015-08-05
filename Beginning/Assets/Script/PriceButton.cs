using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System;

public class PriceButton : MonoBehaviour
{
	public int price;
	Color pricebutton_colorStart;
	float duration1 = 0.1f;
	float duration2 = 0.25f;
	float t =0f;
	public bool fadeout = false;
	public bool fadein = false;
	public bool clickable = false;
	public Notification notification;
	public GameState gamestate;
	public GhostRunnerResources resources;
	
	void Start () {
		pricebutton_colorStart = this.gameObject.GetComponent<Renderer> ().material.color;
		if(!clickable)
			this.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.clear,pricebutton_colorStart,0);
		else
			this.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.clear,pricebutton_colorStart,1);
	}
	
	void Update() {
		if(fadeout==true)
			FadeOut();
		if(fadein==true)
			FadeIn();
	}
	
	void FadeOut ()
	{
		clickable = false;
		if (t < duration1) {
			t += Time.deltaTime;
			this.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (pricebutton_colorStart, Color.clear, t / duration1);
			
			//destf.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (description_colorStart, Color.clear, t / duration1);
			
		} else {
			fadeout = false;
			t = 0f;
		}
		//yield return new WaitForSeconds(duration);
	}
	void FadeIn ()
	{
		if (t < duration2) {
			t += Time.deltaTime;
			this.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.clear,pricebutton_colorStart,  t / duration2);
			//destf.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.clear,description_colorStart, t / duration2);
			
		} else {
			fadein = false;
			t = 0f;
			clickable = true;
		}
		//yield return new WaitForSeconds(duration);
	}

	void OnMouseDown(){
		if (clickable) {
			if(resources.realgold>=price)
			{
				if(FB.IsLoggedIn)
				{
					if(resources.HasConnection())
					{
						showSaveNotification ();
					}
					else
					{
						showNoConnection();
					}
				}
				else
				{
					resources.ChangeGold(price);
					switch(price)
					{
					case 200:
						resources.numJumpItem+=1;
						break;
					case 500:
						resources.numLifeItem+=1;
						break;
					case 999:
						resources.numShieldItem+=1;
						break;
					}
				}
			}
			else
			{
				showNotMoneyNotification();
					//Show not enough money
			}
		}
	} 

	void showSaveNotification()
	{
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.Saving;
		notification.isDone = false;
		resources.isnotify = true;
		StartCoroutine(saveScore(resources.realgold));
	}

	void showNotMoneyNotification()
	{
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NotEnoughMoney;
		notification.isDone = false;
		resources.isnotify = true;
		//StartCoroutine(saveScore(resources.realgold));
	}

	void showNoConnection()
	{
		//gamestate.stategame = GameState.StateGame.Reborn;
		notification.gameObject.SetActive(true);
		notification.type = Notification.NotificationType.NoConnection;
		notification.isDone = false;
		resources.isnotify = true;
	}
	
	private IEnumerator saveScore(int score) 
	{
		if (FB.IsLoggedIn) 
		{
			//var user = ParseUser.CurrentUser;
			//user ["score"] = score-price;
			switch(price)
			{
			case 200:
				resources.AddItemCloud("JumpItem",price);
				//user ["JumpItem"] =resources.numJumpItem+1;
				break;
			case 500:
				resources.AddItemCloud("LifeItem",price);
				//user ["LifeItem"] =resources.numLifeItem+1;
				break;
			case 999:
				resources.AddItemCloud("ShieldItem",price);
				//user ["ShieldItem"] =resources.numShieldItem+1;
				break;
			}
			//user ["ShieldItem"] = resources.numShieldItem;
			//user ["JumpItem"] = score;
			//user ["LifeItem"] = score;
			/*var saveTask = user.SaveAsync ();
			while (!saveTask.IsCompleted)
				yield return null;
			if (saveTask.IsCompleted) {
				resources.login_done = true;
				resources.RefeshDataFromServer ();
				//notification.isDone = true;
			}*/
			//gamestate.stategame = GameState.StateGame.Reborn;
			//UpdateProfile();
			//}
		}
		yield return null;
	}
}

