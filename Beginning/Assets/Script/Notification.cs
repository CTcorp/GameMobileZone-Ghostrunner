using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour
{

	public GhostRunnerResources resource;
	public GameState gamestate;
	public GameObject Title;
	public GameObject OKButton;
	public GameObject OKButton_2;
	public GameObject CancelButton;
	public Sprite WaitTitleSprite;
	public Sprite DoneTitleSprite;
	public Sprite LogoutTitleSprite;
	public Sprite QuitGameTitleSprite;
	public TextMesh detail;
	public NotificationType type;
	public bool isDone = false;
	public string OpponentName ="Enemy";
	// Use this for initialization

	public enum NotificationType
	{
		Logging,
		Logout,
		Loading,
		Saving,
		SavingChallenge,
		SavingChallenge2,
		RequestServer,
		SearchingOpponent,
		NoConnection,
		ServerNotAvaiable,
		NotEnoughMoney,
		Notify,
		ChallengeRequest_1,
		ChallengeRequest_2,
		ChallengeRequest_3,
		CheckWinCloseChallenge,
		VideoAvaiable,
		VideoNotAvaiable,
		QuitGame
	}

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		switch (type) 
		{
			case NotificationType.Logging:
				if (!isDone) 
				{
					Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
					detail.text = "Please wait a few seconds \n to loading data...";
					if(resource.loaddata_ok)
					{
						isDone=true;
					}
					OKButton.SetActive(false);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
				else
				{
					Title.GetComponent<SpriteRenderer>().sprite = DoneTitleSprite;
					detail.text = "All success. Click OK to continue...";
					OKButton.SetActive(true);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
			break;

			case NotificationType.Logout:
				Title.GetComponent<SpriteRenderer>().sprite = LogoutTitleSprite;
				detail.text = "Do you want to logout? \n All data will be reset to 0...";
				OKButton.SetActive(true);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
			break;

			case NotificationType.Loading:

			break;

			case NotificationType.Saving:
				if (!isDone) 
				{
					Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
					detail.text = "Processing, please wait...";
					if(resource.loaddata_ok)
					{
						isDone=true;
					}
					OKButton.SetActive(false);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
				else if(resource.loaddata_ok)
				{
					/*Title.GetComponent<SpriteRenderer>().sprite = DoneTitleSprite;
					detail.text = "All success. Click OK to continue...";
					OKButton.SetActive(true);*/
					isDone = false;
					resource.isnotify = false;
					this.gameObject.SetActive (false);
					resource.loaddata_ok = false;
				}
			break;

			case NotificationType.SavingChallenge:
				if (!isDone) 
				{
					Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
					detail.text = "Processing, please wait...";
					if(resource.savechallenge)
					{
						isDone=true;
					}
					OKButton.SetActive(false);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
				else if(resource.savechallenge)
				{
					Title.GetComponent<SpriteRenderer>().sprite = DoneTitleSprite;
					detail.text = "You have been finished your challenge in \n "+gamestate.deltaTime.ToString()
						+" .The result will be sent to \n"+resource.opponentname+"\n to decide who is the winner";
					OKButton.SetActive(true);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
					

					//isDone = false;
					//resource.isnotify = false;
					//this.gameObject.SetActive (false);
					//resource.savechallenge = false;
				}
				break;

		case NotificationType.SavingChallenge2:
			if (!isDone) 
			{
				Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
				detail.text = "Processing, please wait...";
				if(resource.savechallenge)
				{
					isDone=true;
				}
				OKButton.SetActive(false);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
			}
			else if(resource.savechallenge)
			{
				isDone = false;
				resource.isnotify = false;
				this.gameObject.SetActive (false);
				resource.savechallenge = false;
			}
			break;

		case NotificationType.SearchingOpponent:
				if (!isDone) 
				{
					Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
					detail.text = "Searching opponent, please wait...";
					if(resource.requestserver_ok)
					{
						isDone=true;
					}
					OKButton.SetActive(false);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
				else if(resource.requestserver_ok)
				{
					Title.GetComponent<SpriteRenderer>().sprite = DoneTitleSprite;
					detail.text = "Your challenge request has been sent \nto "+resource.opponentname
					+" (Best score :"+resource.opponentscore+").\n"+resource.opponentname+" will confirm \nyour request when him/her online";
					OKButton.SetActive(true);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
					//isDone = false;
					//resource.isnotify = true;
					//this.gameObject.SetActive (true);
					//resource.requestserver_ok = false;
				}
				break;

			case NotificationType.NoConnection:
				Title.GetComponent<SpriteRenderer>().sprite = LogoutTitleSprite;
				detail.text = "No Connection!You'll play in " +
					"\noffline mode. All data will be reset " +
					"\nto 0. Please check your connection...";
				OKButton.SetActive(true);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
			break;

			case NotificationType.VideoAvaiable:
			Title.GetComponent<SpriteRenderer>().sprite = LogoutTitleSprite;
			detail.text = "Let's complete this video to receive 1000 gold";
			OKButton.SetActive(true);
			OKButton_2.SetActive(false);
			CancelButton.SetActive(false);
			break;

			case NotificationType.VideoNotAvaiable:
			Title.GetComponent<SpriteRenderer>().sprite = LogoutTitleSprite;
			detail.text = "There is no videos at the moment!";
			OKButton.SetActive(true);
			OKButton_2.SetActive(false);
			CancelButton.SetActive(false);
			break;

			case NotificationType.ServerNotAvaiable:
				Title.GetComponent<SpriteRenderer>().sprite = LogoutTitleSprite;
				detail.text = "Can't connect to Server! \nPlease check your connection and\nrestart the game...";
				OKButton.SetActive(true);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
			break;

			case NotificationType.NotEnoughMoney:
				Title.GetComponent<SpriteRenderer>().sprite = LogoutTitleSprite;
				detail.text = "Can't buy this item! \nYou don't have enough money...";
				OKButton.SetActive(true);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
			break;

			case NotificationType.ChallengeRequest_1:
				isDone=true;
				Title.GetComponent<SpriteRenderer>().sprite = QuitGameTitleSprite;
				detail.text = resource.opponentname+" (Best score:"+resource.opponentscore+")" + " \nsend you a challenge request! \nLet's defeat him/her now";
				OKButton.SetActive(false);
				OKButton_2.SetActive(true);
				CancelButton.SetActive(true);
				break;

			case NotificationType.ChallengeRequest_2:
				isDone=true;
				Title.GetComponent<SpriteRenderer>().sprite = QuitGameTitleSprite;
				detail.text = resource.opponentname+" has finished his/her turn in \n"+resource.opponenttime + " .Now is your turn! \nClick ok to begin the challenge";
				OKButton.SetActive(true);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
				break;

			case NotificationType.ChallengeRequest_3:
				isDone=true;
				Title.GetComponent<SpriteRenderer>().sprite = QuitGameTitleSprite;
				detail.text = resource.opponentname+" (Best score:"+resource.opponentscore+")"+" has rejected your request";
				OKButton.SetActive(true);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
				break;

			case NotificationType.CheckWinCloseChallenge:
				isDone=true;
				Title.GetComponent<SpriteRenderer>().sprite = QuitGameTitleSprite;
				if(resource.iswinChallenge==true)
					detail.text = "Congratulations, you have won the challenge .All challenge " +
						"\ngold will be add to your account. The result:" +
						"\n You : "+ resource.mytime+
						"\n"+resource.opponentname+ " : " + resource.opponenttime;
				else
					detail.text = "You closed the challenge. All challenge gold " +
						"\nwill be sent to your opponent. The result:" +
						"\n You : "+ resource.mytime+
						"\n"+resource.opponentname+ " : " + resource.opponenttime;

				OKButton.SetActive(false);
				OKButton_2.SetActive(true);
				CancelButton.SetActive(false);
				break;

			case NotificationType.QuitGame:
				Title.GetComponent<SpriteRenderer>().sprite = QuitGameTitleSprite;
				detail.text = "Do you want to quit the game?";
				OKButton.SetActive(false);
				OKButton_2.SetActive(true);
				CancelButton.SetActive(true);
			break;

			case NotificationType.Notify:

			break;

		}
	}

	public void ChangeGold(int numgold)
	{
		resource.realgold -= numgold;
		//resources.SaveGame();
	}
}

