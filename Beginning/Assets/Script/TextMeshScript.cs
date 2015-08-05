using UnityEngine;
using System.Collections;

public class TextMeshScript : MonoBehaviour {

	// Use this for initialization
	public string SortingLayerName = "Foreground";
	public int SortingOrder = 500;
	public TextMesh tm;


	void Awake ()
	{
		gameObject.GetComponent<MeshRenderer> ().sortingLayerName = SortingLayerName;
		gameObject.GetComponent<MeshRenderer> ().sortingOrder = SortingOrder;
		tm = GetComponent<TextMesh> ();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
