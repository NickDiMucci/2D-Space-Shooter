using UnityEngine;
using System.Collections;

public class ScreenLose : MonoBehaviour {
	public string loseQuote = "Game Over!";
	
	void OnStart() {
	}

	void OnGUI() {
		GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 220));
		GUI.Box(new Rect(0, 0, 200, 210), loseQuote);
		
		int score = PlayerPrefs.GetInt("score");
		GUI.Label(new Rect(10, 20, 200, 200), "Score: " + score);
		
		if (GUI.Button(new Rect(10, 150, 180, 40), "Back to Main Menu")) {
			Application.LoadLevel("ScreenMainMenu");
		}
		
		GUI.EndGroup();
	}
}
