using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	public static int score = 0;
	public static short lives = 3;
	
	private float gameTime = 60.0f;
	
	public float labelRight = 100.0f;

	// Use this for initialization
	void Start () {
		InvokeRepeating("countDownTimer", 1, 1.0f);
		resetPlayerPrefs();
	}
	
	// Update is called once per frame
	void Update () {
		levelEndLogic ();
	}

	void levelEndLogic () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("ScreenMainMenu");
		}
		if (lives <= 0) {
			Application.LoadLevel("ScreenLose");
			saveScoreToPlayerPrefs();
			resetLives();
			resetScore();
		}
		if (gameTime <= 0) {
			Application.LoadLevel("ScreenWin");
			saveScoreToPlayerPrefs();
		}
	}
	
	void OnGUI() {		
		GUI.Label(new Rect(6, 6, 100, 20), "Score: " + score.ToString());
		GUI.Label(new Rect(6, 20, 100, 20), "Player Lives: " + lives);
		GUI.Label(new Rect(Screen.width - labelRight, 10, 100, 50), "Timer: " + gameTime);
	}
	
	public void AddScore() {
		++score;
	}
	
	public void SetLives(short playerLives) {
		lives = playerLives;
	}
	
	public int GetScore() {
		return score;
	}
	
	private void countDownTimer() {
		gameTime--;
		if (gameTime == 0) {
			CancelInvoke("countDownTimer");
		}
	}
	
	private void resetLives() {
		lives = 3;
	}
	
	private void resetScore() {
		score = 0;
	}
	
	private void saveScoreToPlayerPrefs() {
		PlayerPrefs.SetInt("score", score);
	}
	
	private void resetPlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}
}
