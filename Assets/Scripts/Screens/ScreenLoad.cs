using UnityEngine;
using System.Collections;

public class ScreenLoad : MonoBehaviour {
	public float waitTime = 3.0f;

	void OnGUI() {
		GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 225, 200));
		GUI.Box(new Rect(0, 0, 225, 175), "How to Play!");
		
		GUI.Label(new Rect(10, 30, 175, 40), "Arrow Keys or WASD to Move");
		GUI.Label(new Rect(10, 60, 175, 70), "Spacebar or Mouse1 to Shoot");
		GUI.Label(new Rect(10, 90, 210, 70), "Q or Mouse2 for One-Time-Shield™");
		GUI.Label(new Rect(10, 120, 175, 100), "Esc to Quit the Game");
		
		GUI.EndGroup();
	}
	
	void Start() {
		InvokeRepeating("loadCounter", 1, 1);
	}
	
	void Update() {
		if (waitTime <= 0 || Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("Level_01");
			CancelInvoke("loadCounter");
		}
	}
	
	private void loadCounter() {
		waitTime--;
	}
}
