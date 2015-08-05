using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public GhostRunnerResources resources;
	public GameObject LoginIcon;
	SpriteRenderer loginiconrenderer;
	public GameObject LogoutIcon;
	// Use this for initialization
	void Start ()
	{
		loginiconrenderer = LoginIcon.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!resources.offlinemode) 
		{
			if(FB.IsInitialized)
			{
				if(FB.IsLoggedIn)
				{
					if(resources.login_done==true)
					{
						LogoutIcon.SetActive(true);
						LoginIcon.SetActive(false);
					}
					else
					{
						LogoutIcon.SetActive(false);
						LoginIcon.SetActive(true);
						loginiconrenderer.enabled = true;
					}
					loginiconrenderer.enabled = false;
				}
				else
				{
					LogoutIcon.SetActive(false);
					LoginIcon.SetActive(true);
					loginiconrenderer.enabled = true;
				}
			}
		} 
		else 
		{
			/*LoginIcon.SetActive(false);
			LogoutIcon.SetActive(false);*/
		}

		/*if (resources.isnotify)
			this.transform.Find ("Tittle").gameObject.SetActive (false);
		else
			this.transform.Find ("Tittle").gameObject.SetActive (true);*/
	}
}

