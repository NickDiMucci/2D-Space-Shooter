using UnityEngine;
using System.Collections;

public class ScreenMainMenu : MonoBehaviour {
	void OnGUI() {
		GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 200));
		GUI.Label(new Rect(90, 0, 250, 200), "Pew Pew!");
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 200));
		GUI.Box(new Rect(0, 0, 250, 200), "Main Menu");
		if (GUI.Button(new Rect(85, 30, 80, 30), "Start Game")) {
			Application.LoadLevel("ScreenLoad");
		}
		if (GUI.Button(new Rect(85, 70, 80, 30), "Credits")) {
			Application.LoadLevel("ScreenCredit");
		}
		if (GUI.Button(new Rect(85, 110, 80, 30), "Exit")) {
			Application.Quit();
		}
		if (GUI.Button(new Rect(20, 150, 210, 30), "Thanks to Walker Boys Studios!")) {
			Application.OpenURL("http://www.walkerboystudio.com/");
		}
		GUI.EndGroup();
	}
}
