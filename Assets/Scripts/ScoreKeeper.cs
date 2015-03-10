using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	private int scoreInt;
	private Text scoreText;

	void Start(){
		scoreText = this.GetComponent<Text>();
		ResetScore();
	}

	public void AddScore(int pts){
		scoreInt += pts;
		UpdateText();
	}
	
	public void ResetScore(){
		scoreInt = 0;
		UpdateText();
	}
	
	private void UpdateText(){
		//scoreText.text = scoreInt.ToString("3D");
		scoreText.text = scoreInt.ToString().PadLeft(4, '0');
	}
}