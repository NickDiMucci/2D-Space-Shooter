using UnityEngine;
using System.Collections;

public class ScreenCredit : MonoBehaviour {
	
	void OnGUI() {
		GUI.BeginGroup(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 130, 350, 300));
		GUI.Box(new Rect(0, 0, 330, 300), "Credits");
		
		GUI.Label(new Rect(10, 30, 280, 40), "Developed by Nicholas DiMucci");
		GUI.Label(new Rect(10, 60, 320, 40), "Based on the tutorial created by Walker Boys Studio");
		if (GUI.Button(new Rect(70, 90, 175, 30), "walkerboystudio.com")) {
			Application.OpenURL("http://www.walkerboystudio.com/");
		}
		GUI.Label(new Rect(10, 130, 300, 40), "Music: All Systems Go (Metroid) by bLiNd");
		if (GUI.Button(new Rect(80, 160, 150, 30), "gamechops.com")) {
			Application.OpenURL("http://music.gamechops.com/album/nesteryears");
		}
			
		if (GUI.Button(new Rect(63, 240, 180, 40), "Back to Main Menu")) {
			Application.LoadLevel("ScreenMainMenu");
		}
		
		GUI.EndGroup();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
