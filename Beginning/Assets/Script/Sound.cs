using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	//public AudioClip audio;
	AudioSource[] backgroundaudio;
	// Use this for initialization
	void Start () {
		backgroundaudio = GetComponents<AudioSource>();
		backgroundaudio[0].Play ();
		backgroundaudio[0].loop = true;
		backgroundaudio[2].Play ();
		backgroundaudio[2].loop = true;
		backgroundaudio[3].Play ();
		backgroundaudio[3].loop = true;
		//backgroundaudio[4].Play ();
		//backgroundaudio[4].loop = true;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
