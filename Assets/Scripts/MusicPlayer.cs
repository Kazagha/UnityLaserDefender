using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

/*
	static GameObject musicPlayerObject = null;

	// Use this for initialization
	void Start () {
		if(musicPlayerObject == null)
		{
			Debug.Log("Music Player is NULL, creating static variable");			
			musicPlayerObject = this.gameObject;
			GameObject.DontDestroyOnLoad(musicPlayerObject);
		} else {
			Debug.Log ("Music Player Exists");
			Destroy (gameObject);
			gameObject.audio.Stop();
			Debug.Log ("Duplicate music player has been destroyed");
		}
	}	
*/

	static MusicPlayer playerInstance = null;
	
	void Awake() {
		// Debug.Log("Music Player Awake: " + GetInstanceID());
		
		
		if(playerInstance != null)
		{					
			Destroy (gameObject);	
			Debug.Log ("Duplicate music player has been destroyed in 'Awake' method");			
		} else {
			Debug.Log("Music Player is NULL, creating static variable");			
			playerInstance = this;
			GameObject.DontDestroyOnLoad(playerInstance);
		}
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
