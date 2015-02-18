using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {		
		// Reset the brick count
		//Brick.breakableCount = 0;
		Application.LoadLevel(name);
	}

	public void LoadNextLevel()	{		
		// Reset the brick count
		//Brick.breakableCount = 0;		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	public void QuitRequest() {
		Application.Quit();
	}

	/*
	public void BrickDestroyed() {

		if(Brick.breakableCount <= 0) {
			LoadNextLevel();
		}		
	}
	*/	
}
