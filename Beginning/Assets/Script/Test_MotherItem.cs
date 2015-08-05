using UnityEngine;
using System.Collections;

public class Test_MotherItem : MonoBehaviour
{
	public int realscore;
	public GameState gamestate;
	Test_ListItem[] allChildren;
	int childidactive =0;
	int numchild = 0;
	public bool isLeftIcon=false;
	public bool isRightIcon=false;
	private Transform moneytf;
	public GhostRunnerResources resources;

	// Use this for initialization
	void Start ()
	{
		moneytf = transform.Find ("Money");
		moneytf.GetComponent<TextMesh> ().text = resources.gold.ToString();
		allChildren = GetComponentsInChildren<Test_ListItem>();
		numchild = allChildren.Length;
		Reset ();
		//this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gamestate.stategame == GameState.StateGame.Shopping) {
			if (isRightIcon) {
				if (childidactive < numchild - 1) {
					allChildren [childidactive + 1].gameObject.SetActive (true);
					allChildren [childidactive].fadeout = true;
					allChildren [childidactive].fadeoutPriceButton = true;
					allChildren [childidactive + 1].fadein = true;
					allChildren [childidactive + 1].fadeinPriceButton = true;
					childidactive += 1;
				} else {
					allChildren [0].gameObject.SetActive (true);
					allChildren [childidactive].fadeout = true;
					allChildren [childidactive].fadeoutPriceButton = true;
					allChildren [0].fadein = true;
					allChildren [0].fadeinPriceButton = true;
					childidactive = 0;
				}
				isRightIcon = false;
			}
			if (isLeftIcon) {
				if (childidactive <= 0) {
					allChildren [numchild - 1].gameObject.SetActive (true);
					allChildren [childidactive].fadeout = true;
					allChildren [childidactive].fadeoutPriceButton = true;
					allChildren [numchild - 1].fadein = true;
					allChildren [numchild - 1].fadeinPriceButton = true;
					childidactive = numchild - 1;
				} else {
					allChildren [childidactive - 1].gameObject.SetActive (true);
					allChildren [childidactive].fadeout = true;
					allChildren [childidactive].fadeoutPriceButton = true;
					allChildren [childidactive - 1].fadein = true;
					allChildren [childidactive - 1].fadeinPriceButton = true;
					childidactive -= 1;
				}
				isLeftIcon = false;
			}
		}

			moneytf.GetComponent<TextMesh> ().text = resources.gold.ToString ();
	
	}

	public void ChangeGold(int numgold)
	{
		resources.realgold -= numgold;
		//resources.SaveGame();
	}

	public void Reset()
	{
		foreach (Test_ListItem child in allChildren) {
			child.gameObject.SetActive(false);
		}
		allChildren[0].gameObject.SetActive(true);
	}


}

