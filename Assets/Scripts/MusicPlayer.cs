using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip loseClip;

	private AudioSource music;
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
			music = this.GetComponent<AudioSource>();

			music.clip = startClip;
			music.Play();
			music.loop = true;
		}
	}

	void OnLevelWasLoaded(int level){
		// Stop the current track
		music.Stop();

		// Select the apropriate track
		if(level == 0)
		{
			music.clip = startClip;
		} else if (level == 1) {
			music.clip = gameClip;
		} else if (level == 2) {
			music.clip = loseClip;
		}

		// Begin playing the music
		music.Play();

		// Allow the music to continue to loop
		music.loop = true;
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
