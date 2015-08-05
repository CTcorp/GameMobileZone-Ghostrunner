using UnityEngine;
using System.Collections;

public class LeftRightShop : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDown(){
		if (this.gameObject.name == "MoveLeft")
			transform.parent.GetComponent<Test_MotherItem> ().isLeftIcon = true;
		if(this.gameObject.name == "MoveRight")
			transform.parent.GetComponent<Test_MotherItem> ().isRightIcon = true;
	} 
}

