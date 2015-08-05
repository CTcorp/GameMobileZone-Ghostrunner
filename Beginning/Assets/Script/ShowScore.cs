using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
	public GameObject ScoreEntryPanel;
	public GameObject ScoreScrollList;
	String rstext = "";
	public String rstext2 = "";
	public bool ready = false;
	// Use this for initialization
	void Start ()
	{
		//StartCoroutine( GetScore());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ready == true) {
			QueryScore();
			ready=false;
		}
	}
	void OnEnable()
	{
		StartCoroutine( GetHighScore());
	}



	private IEnumerator GetHighScore()
	{
		/*IDictionary<string, object> testparam = new Dictionary<string, object>
		{
			{ "id", "822403307808270" }
		};
		ParseCloud.CallFunctionAsync<string>("GetScore", testparam).ContinueWith(t =>
		{
			rstext2 = t.Result;
			Debug.Log(rstext2);
		});*/

		ParseCloud.CallFunctionAsync<string>("GetHighScore", new Dictionary<string, object>()).ContinueWith(t =>
		{
			rstext = t.Result;
			Debug.Log(rstext);
			ready=true;
		});
		yield break;
	}
	
	public void QueryScore()
	{
		string[] results = rstext.Split('#');
		var topbestavatar = new Dictionary<string, string>();
		foreach (string word in results)
		{
			Debug.Log(word);
		}
		foreach (Transform child in ScoreScrollList.transform) {
			Destroy(child.gameObject);
		}
		for(int i=0;i<results.Length;i++)
		{
			string[] result = results[i].Split('$');
			GameObject ScorePanel;
			ScorePanel = Instantiate (ScoreEntryPanel) as GameObject;
			ScorePanel.transform.SetParent(ScoreScrollList.transform);
			Transform ThisName = ScorePanel.transform.Find ("Name");
			Transform ThisScore = ScorePanel.transform.Find ("Score");
			Transform ThisCountry = ScorePanel.transform.Find ("Country");
			Transform ThisAvatar = ScorePanel.transform.Find ("Avatar");
			Text ScoreName = ThisName.GetComponent<Text> ();
			Text ScoreCountry = ThisCountry.GetComponent<Text> ();
			Text ScoreScore = ThisScore.GetComponent<Text> ();
			Image ScoreAvatar = ThisAvatar.GetComponent<Image> ();
			ScoreName.text = result[1];
			ScoreCountry.text = result[2];
			ScoreScore.text = Math.Truncate (Convert.ToDouble(result[3])) + ":" +Convert.ToDouble(result[3]).ToString ("0.00").Split ('.') [1];
			FB.API (Util.GetPictureURL(result[0],128,128),HttpMethod.GET,delegate(FBResult pictureResult)
			        {
				if(pictureResult.Error!=null)
				{
					Debug.Log("Error!");
				}
				else
				{
					if(ScoreAvatar!=null)
					ScoreAvatar.sprite = Sprite.Create (pictureResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));
					else
					{
						Debug.Log ("ImageDestroyed");
					}
				}
			});
		}
	}
}

